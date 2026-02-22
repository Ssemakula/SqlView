namespace SqlView
{
    partial class DisplayForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayForm));
            splitContainer = new SplitContainer();
            sqlTextBox = new TextBox();
            sqlToolStrip = new ToolStrip();
            connectionToolStripComboBox = new ToolStripComboBox();
            connectToolStripButton = new ToolStripButton();
            removeToolStripButton = new ToolStripButton();
            openToolStripButton = new ToolStripButton();
            resultDataGridView = new DataGridView();
            gridToolStrip = new ToolStrip();
            firstToolStripButton = new ToolStripButton();
            prevToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            posToolStripLabel = new ToolStripLabel();
            ofToolStripLabel = new ToolStripLabel();
            countToolStripLabel = new ToolStripLabel();
            toolStripSeparator2 = new ToolStripSeparator();
            nextToolStripButton = new ToolStripButton();
            lastToolStripButton = new ToolStripButton();
            refreshToolStripButton = new ToolStripButton();
            excelToolStripButton = new ToolStripButton();
            exitToolStripButton = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            sqlToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)resultDataGridView).BeginInit();
            gridToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer.Location = new Point(12, 12);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(sqlTextBox);
            splitContainer.Panel1.Controls.Add(sqlToolStrip);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(resultDataGridView);
            splitContainer.Panel2.Controls.Add(gridToolStrip);
            splitContainer.Size = new Size(1084, 553);
            splitContainer.SplitterDistance = 361;
            splitContainer.TabIndex = 1;
            // 
            // sqlTextBox
            // 
            sqlTextBox.AcceptsReturn = true;
            sqlTextBox.AcceptsTab = true;
            sqlTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            sqlTextBox.Location = new Point(3, 28);
            sqlTextBox.Multiline = true;
            sqlTextBox.Name = "sqlTextBox";
            sqlTextBox.ScrollBars = ScrollBars.Both;
            sqlTextBox.Size = new Size(355, 514);
            sqlTextBox.TabIndex = 1;
            sqlTextBox.WordWrap = false;
            // 
            // sqlToolStrip
            // 
            sqlToolStrip.Items.AddRange(new ToolStripItem[] { connectionToolStripComboBox, connectToolStripButton, removeToolStripButton, openToolStripButton });
            sqlToolStrip.Location = new Point(0, 0);
            sqlToolStrip.Name = "sqlToolStrip";
            sqlToolStrip.Size = new Size(361, 25);
            sqlToolStrip.TabIndex = 0;
            sqlToolStrip.Text = "toolStrip1";
            // 
            // connectionToolStripComboBox
            // 
            connectionToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            connectionToolStripComboBox.Name = "connectionToolStripComboBox";
            connectionToolStripComboBox.Size = new Size(250, 25);
            connectionToolStripComboBox.SelectedIndexChanged += ConnectionToolStripComboBox_SelectedIndexChanged;
            // 
            // connectToolStripButton
            // 
            connectToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            connectToolStripButton.Image = Properties.Resources.plug;
            connectToolStripButton.ImageTransparentColor = Color.Magenta;
            connectToolStripButton.Name = "connectToolStripButton";
            connectToolStripButton.Size = new Size(23, 22);
            connectToolStripButton.Text = "Add Database Connection";
            connectToolStripButton.Click += ConnectToolStripButton_Click;
            // 
            // removeToolStripButton
            // 
            removeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            removeToolStripButton.Image = Properties.Resources.adapter;
            removeToolStripButton.ImageTransparentColor = Color.Magenta;
            removeToolStripButton.Name = "removeToolStripButton";
            removeToolStripButton.Size = new Size(23, 22);
            removeToolStripButton.Text = "Remove Connection";
            // 
            // openToolStripButton
            // 
            openToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openToolStripButton.Image = Properties.Resources.folder_open_icon;
            openToolStripButton.ImageTransparentColor = Color.Magenta;
            openToolStripButton.Name = "openToolStripButton";
            openToolStripButton.Size = new Size(23, 22);
            openToolStripButton.Text = "Load File";
            openToolStripButton.Click += OpenToolStripButton_Click;
            // 
            // resultDataGridView
            // 
            resultDataGridView.AllowUserToAddRows = false;
            resultDataGridView.AllowUserToDeleteRows = false;
            resultDataGridView.AllowUserToOrderColumns = true;
            resultDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            resultDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resultDataGridView.Location = new Point(11, 33);
            resultDataGridView.Name = "resultDataGridView";
            resultDataGridView.ReadOnly = true;
            resultDataGridView.Size = new Size(705, 509);
            resultDataGridView.TabIndex = 1;
            // 
            // gridToolStrip
            // 
            gridToolStrip.Items.AddRange(new ToolStripItem[] { firstToolStripButton, prevToolStripButton, toolStripSeparator1, posToolStripLabel, ofToolStripLabel, countToolStripLabel, toolStripSeparator2, nextToolStripButton, lastToolStripButton, refreshToolStripButton, excelToolStripButton, exitToolStripButton });
            gridToolStrip.Location = new Point(0, 0);
            gridToolStrip.Name = "gridToolStrip";
            gridToolStrip.Size = new Size(719, 25);
            gridToolStrip.TabIndex = 0;
            gridToolStrip.Text = "toolStrip1";
            // 
            // firstToolStripButton
            // 
            firstToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            firstToolStripButton.Image = Properties.Resources.first;
            firstToolStripButton.ImageTransparentColor = Color.Magenta;
            firstToolStripButton.Name = "firstToolStripButton";
            firstToolStripButton.Size = new Size(23, 22);
            firstToolStripButton.Text = "First row";
            // 
            // prevToolStripButton
            // 
            prevToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            prevToolStripButton.Image = Properties.Resources.prev;
            prevToolStripButton.ImageTransparentColor = Color.Magenta;
            prevToolStripButton.Name = "prevToolStripButton";
            prevToolStripButton.Size = new Size(23, 22);
            prevToolStripButton.Text = "Previous row";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // posToolStripLabel
            // 
            posToolStripLabel.Name = "posToolStripLabel";
            posToolStripLabel.Size = new Size(13, 22);
            posToolStripLabel.Text = "0";
            // 
            // ofToolStripLabel
            // 
            ofToolStripLabel.Name = "ofToolStripLabel";
            ofToolStripLabel.Size = new Size(18, 22);
            ofToolStripLabel.Text = "of";
            // 
            // countToolStripLabel
            // 
            countToolStripLabel.Name = "countToolStripLabel";
            countToolStripLabel.Size = new Size(13, 22);
            countToolStripLabel.Text = "0";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // nextToolStripButton
            // 
            nextToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            nextToolStripButton.Image = Properties.Resources.next;
            nextToolStripButton.ImageTransparentColor = Color.Magenta;
            nextToolStripButton.Name = "nextToolStripButton";
            nextToolStripButton.Size = new Size(23, 22);
            nextToolStripButton.Text = "Next row";
            // 
            // lastToolStripButton
            // 
            lastToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            lastToolStripButton.Image = Properties.Resources.last;
            lastToolStripButton.ImageTransparentColor = Color.Magenta;
            lastToolStripButton.Name = "lastToolStripButton";
            lastToolStripButton.Size = new Size(23, 22);
            lastToolStripButton.Text = "Last row";
            // 
            // refreshToolStripButton
            // 
            refreshToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            refreshToolStripButton.Image = Properties.Resources.refresh_reload_icon;
            refreshToolStripButton.ImageTransparentColor = Color.Magenta;
            refreshToolStripButton.Name = "refreshToolStripButton";
            refreshToolStripButton.Size = new Size(23, 22);
            refreshToolStripButton.Text = "Refresh grid";
            refreshToolStripButton.Click += RefreshToolStripButton_Click;
            // 
            // excelToolStripButton
            // 
            excelToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            excelToolStripButton.Image = Properties.Resources.excel_microsoft_office_office365_icon;
            excelToolStripButton.ImageTransparentColor = Color.Magenta;
            excelToolStripButton.Name = "excelToolStripButton";
            excelToolStripButton.Size = new Size(23, 22);
            excelToolStripButton.Text = "Export to Excel";
            excelToolStripButton.Click += ExcelToolStripButton_Click;
            // 
            // exitToolStripButton
            // 
            exitToolStripButton.Alignment = ToolStripItemAlignment.Right;
            exitToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            exitToolStripButton.Image = Properties.Resources.exit;
            exitToolStripButton.ImageTransparentColor = Color.Magenta;
            exitToolStripButton.Name = "exitToolStripButton";
            exitToolStripButton.Size = new Size(23, 22);
            exitToolStripButton.Text = "Exit";
            exitToolStripButton.Click += ExitToolStripButton_Click;
            // 
            // DisplayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1108, 577);
            Controls.Add(splitContainer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DisplayForm";
            Text = "Display";
            Load += DisplayForm_Load;
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel1.PerformLayout();
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            sqlToolStrip.ResumeLayout(false);
            sqlToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)resultDataGridView).EndInit();
            gridToolStrip.ResumeLayout(false);
            gridToolStrip.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer;
        private ToolStrip gridToolStrip;
        private TextBox sqlTextBox;
        private ToolStrip sqlToolStrip;
        private DataGridView resultDataGridView;
        private ToolStripComboBox connectionToolStripComboBox;
        private ToolStripButton openToolStripButton;
        private ToolStripButton firstToolStripButton;
        private ToolStripButton prevToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel posToolStripLabel;
        private ToolStripLabel ofToolStripLabel;
        private ToolStripLabel countToolStripLabel;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton nextToolStripButton;
        private ToolStripButton lastToolStripButton;
        private ToolStripButton refreshToolStripButton;
        private ToolStripButton excelToolStripButton;
        private ToolStripButton exitToolStripButton;
        private ToolStripButton connectToolStripButton;
        private ToolStripButton removeToolStripButton;
    }
}
