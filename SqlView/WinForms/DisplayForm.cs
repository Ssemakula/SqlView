using Czemi.GridExcel;
using Czemi.LogicMethods;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using SqlView.Types;
using SqlView.Utilities;
using SqlView.WinForms;
using System.Data;

namespace SqlView
{
    public partial class DisplayForm : Form
    {
        private ConnectionConfiguration? SelectedConnection { get; set; }

        private DataTable _dataTable;
        private BindingSource _bindingSource;
        private GridSort? _sortController;

        public DisplayForm()
        {
            InitializeComponent();

            _dataTable = new DataTable();
            _bindingSource = new BindingSource { DataSource = _dataTable };
            resultDataGridView.DataSource = _bindingSource;
            _sortController = new GridSort(resultDataGridView, _bindingSource);
            resultDataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            SetGridDefaults();
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            RefreshConnectionList();
            if (connectionToolStripComboBox.Items.Count == 0)
            {
                refreshToolStripButton.Enabled = false;
            }
            else
            {
                if (connectionToolStripComboBox.SelectedItem is ConnectionConfiguration config)
                {
                    SelectedConnection = config;
                    refreshToolStripButton.Enabled = true;
                }
            }
        }

        private void RefreshConnectionList()
        {
            connectionToolStripComboBox.Items.Clear();
            var connections = LocalConfigDatabase.GetAllConnections();

            foreach (var conn in connections)
            {
                connectionToolStripComboBox.Items.Add(conn);
            }

            if (connectionToolStripComboBox.Items.Count > 0)
            {
                connectionToolStripComboBox.SelectedIndex = 0;
            }
        }

        private void ConnectToolStripButton_Click(object sender, EventArgs e)
        {
            using var editor = new ConnectionEditor();
            if (editor.ShowDialog() == DialogResult.OK)
            {
                RefreshConnectionList();
                if (connectionToolStripComboBox.Items.Count > 0)
                {
                    refreshToolStripButton.Enabled = true;
                }
            }
        }

        private void ConnectionToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connectionToolStripComboBox.SelectedItem is ConnectionConfiguration config)
            {
                SelectedConnection = config;
            }
        }
        private void SetGridDefaults()
        {
            // Set default properties for DataGridView
            resultDataGridView.EnableHeadersVisualStyles = false; // Disable default header styles
            // Set custom header styles for the DataGridView
            resultDataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(4, 52, 55);
            resultDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            resultDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(225, 255, 250); // Set alternating row color
            resultDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resultDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            resultDataGridView.AllowUserToAddRows = false; // Disable adding rows by the user
            resultDataGridView.AllowUserToDeleteRows = false; // Disable deleting rows by the user
            resultDataGridView.AllowUserToResizeRows = false; // Disable resizing rows by the user
            resultDataGridView.AllowUserToOrderColumns = true; // Allow column reordering
            resultDataGridView.AllowUserToResizeColumns = true; // Allow column resizing
            resultDataGridView.MultiSelect = true; // Enable multi-select
            resultDataGridView.ReadOnly = true; // Make the grid read-only
        }

        private async void RefreshToolStripButton_Click(object sender, EventArgs e)
        {
            // This executes the SqlQuery in the queryTextBox and displays the results in the resultDataGridView
            if (SelectedConnection == null)
            {
                MessageBox.Show("Please select a connection before refreshing.", "No Connection Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await ReadSqlData();
        }

        private async Task ReadSqlData()
        {
            if (SelectedConnection == null)
            {
                MessageBox.Show("Please select a connection.", "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = sqlTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(sql))
            {
                MessageBox.Show("Please enter a SQL query.", "No Query", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var connectionString = ConnectionMethods.GetConnectionString(SelectedConnection);

                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(sql, connection);
                command.CommandTimeout = 300; // 5 minutes timeout

                using var reader = await command.ExecuteReaderAsync();

                // Clear existing data
                _dataTable.Clear();
                _dataTable.Columns.Clear();

                // Handle multiple result sets - load the last one
                DataTable? lastResultSet = null;
                int resultSetCount = 0;

                do
                {
                    resultSetCount++;
                    var tempTable = new DataTable();
                    tempTable.Load(reader);

                    if (tempTable.Rows.Count > 0 || tempTable.Columns.Count > 0)
                    {
                        lastResultSet = tempTable;
                    }
                }
                while (!reader.IsClosed && reader.NextResult());

                // Load the last result set into our DataTable
                if (lastResultSet != null)
                {
                    // Track which columns are timestamp/byte[] columns
                    var timestampColumns = new List<int>();

                    foreach (DataColumn col in lastResultSet.Columns)
                    {
                        if (col.DataType == typeof(byte[]))
                        {
                            // Convert timestamp columns to string type
                            _dataTable.Columns.Add(col.ColumnName, typeof(string));
                            timestampColumns.Add(col.Ordinal);
                        }
                        else
                        {
                            _dataTable.Columns.Add(col.ColumnName, col.DataType);
                        }
                    }

                    foreach (DataRow sourceRow in lastResultSet.Rows)
                    {
                        var newRow = _dataTable.NewRow();

                        for (int i = 0; i < lastResultSet.Columns.Count; i++)
                        {
                            if (timestampColumns.Contains(i))
                            {
                                // Replace timestamp binary data with placeholder text
                                newRow[i] = "<timestamp>";
                            }
                            else
                            {
                                newRow[i] = sourceRow[i];
                            }
                        }

                        _dataTable.Rows.Add(newRow);
                    }

                    if (resultSetCount > 1)
                    {
                        MessageBox.Show($"Query returned {resultSetCount} result sets. Displaying the last one.",
                            "Multiple Result Sets", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Query executed successfully but returned no results.",
                        "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _bindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing query: {ex.Message}", "Query Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private static bool Export2Excel(DataGridView dgv, string filenameIn = "")
        {
            string filename = string.Empty;
            bool mresult = false;

            DialogResult result = MessageBox.Show("Save to file?", "Save File", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                SaveFileDialog select = new SaveFileDialog
                {
                    Filter = "Excel Files |*.xls;*.xlsx",
                    DefaultExt = "xlsx",
                    FileName = filenameIn,
                    Title = "Select Excel File to Save",
                    OverwritePrompt = true,
                    RestoreDirectory = true
                };

                if (select.ShowDialog() == DialogResult.OK)
                {
                    filename = select.FileName;
                }

                if (!filename.IsTrue())
                {
                    return mresult;
                }
            }
            else if (result == DialogResult.Cancel)
                return mresult;

            try
            {
                dgv.SaveToExcel(filename);
                mresult = true;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show($"Error: {ex} {Environment.NewLine} Please try again later",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else

                MessageBox.Show($"Error exporting to Excel: {ex.Message} {Environment.NewLine} Please try again later",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
            return mresult;
        }

        private static async Task<string> ReadFileContentsSafeAsync(string filename)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filename) || !File.Exists(filename))
                {
                    return string.Empty;
                }

                string content = await File.ReadAllTextAsync(filename, System.Text.Encoding.UTF8);

                // Normalize line endings to Windows format (\r\n)
                // This handles Unix (\n), Mac (\r), and mixed line endings
                content = content.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);

                return content;
            }
            catch
            {
                return string.Empty;
            }
        }

        private void ExitToolStripButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExcelToolStripButton_Click(object sender, EventArgs e)
        {
            // This saves to excel the data in the resultDataGridView
            Export2Excel(resultDataGridView, $"SqlView_Export_{DateTime.Now:yyyyMMdd_HHmmss}");
        }

        private async void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            string filename = string.Empty;


            OpenFileDialog select = new OpenFileDialog
            {
                Filter = "SQL Files |*.sql",
                DefaultExt = "sql",
                FileName = "",
                Title = "Select SQL File to Open",
                RestoreDirectory = true
            };

            if (select.ShowDialog() == DialogResult.OK)
            {
                filename = select.FileName;
            }

            if (!filename.IsTrue())
            {
                return;
            }

            try
            {
                sqlTextBox.Text = await ReadFileContentsSafeAsync(filename);
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show($"Error: {ex} {Environment.NewLine} Please try again later",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else

                MessageBox.Show($"Error reading SQL file: {ex.Message} {Environment.NewLine} Please try again later",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }
    }
}
