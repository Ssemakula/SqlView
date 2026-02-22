using SqlView.Types;
using SqlView.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SqlView.WinForms
{
    public partial class ConnectionEditor : Form
    {
        private readonly ConnectionConfiguration? _existingConnection;
        private readonly bool _isEditMode;

        public ConnectionEditor(ConnectionConfiguration? connection = null)
        {
            InitializeComponent();

            _existingConnection = connection;
            _isEditMode = connection != null;

            if (_isEditMode && _existingConnection != null)
            {
                nameTextBox.Text = _existingConnection.ConnectionName;
                serverTextBox.Text = _existingConnection.DataSource;
                databaseTextBox.Text = _existingConnection.Database;
                userIdTextBox.Text = _existingConnection.UserID;
                passwordTextBox.Text = _existingConnection.Password;

                Text = "Edit Connection";
            }
            else
            {
                Text = "Add Connection";
            }
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Connection name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(serverTextBox.Text))
            {
                MessageBox.Show("Server is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                serverTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(databaseTextBox.Text))
            {
                MessageBox.Show("Database is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                databaseTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(userIdTextBox.Text))
            {
                MessageBox.Show("User ID is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                userIdTextBox.Focus();
                return;
            }

            try
            {
                var config = new ConnectionConfiguration
                {
                    ConnectionName = nameTextBox.Text.Trim(),
                    DataSource = serverTextBox.Text.Trim(),
                    Database = databaseTextBox.Text.Trim(),
                    UserID = userIdTextBox.Text.Trim(),
                    Password = passwordTextBox.Text
                };

                if (_isEditMode && _existingConnection != null)
                {
                    config.ID = _existingConnection.ID;
                    LocalConfigDatabase.UpdateConnection(config);
                }
                else
                {
                    LocalConfigDatabase.AddConnection(config);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to save connection:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void TestToolStripButton_Click(object sender, EventArgs e)
        {
            var config = new ConnectionConfiguration
            {
                DataSource = serverTextBox.Text.Trim(),
                Database = databaseTextBox.Text.Trim(),
                UserID = userIdTextBox.Text.Trim(),
                Password = passwordTextBox.Text
            };

            Cursor = Cursors.WaitCursor;
            bool success = LocalConfigDatabase.TestConnection(config, out string errorMessage);
            Cursor = Cursors.Default;

            if (success)
            {
                MessageBox.Show(
                    "Connection successful!",
                    "Test Connection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    $"Connection failed:\n\n{errorMessage}",
                    "Test Connection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CancelToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ExitToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}
