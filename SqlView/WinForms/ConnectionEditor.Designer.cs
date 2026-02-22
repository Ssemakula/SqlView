namespace SqlView.WinForms
{
    partial class ConnectionEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionEditor));
            toolStrip1 = new ToolStrip();
            saveToolStripButton = new ToolStripButton();
            cancelToolStripButton = new ToolStripButton();
            testToolStripButton = new ToolStripButton();
            exitToolStripButton = new ToolStripButton();
            passwordTextBox = new TextBox();
            passwordLabel = new Label();
            userIdTextBox = new TextBox();
            userIdLabel = new Label();
            databaseTextBox = new TextBox();
            databaseLabel = new Label();
            serverTextBox = new TextBox();
            serverLabel = new Label();
            nameTextBox = new TextBox();
            nameLabel = new Label();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { saveToolStripButton, cancelToolStripButton, testToolStripButton, exitToolStripButton });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Margin = new Padding(10, 0, 10, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(15, 0, 15, 0);
            toolStrip1.Size = new Size(407, 38);
            toolStrip1.TabIndex = 11;
            toolStrip1.Text = "toolStrip1";
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.Image = Properties.Resources.save_butt;
            saveToolStripButton.ImageTransparentColor = Color.Magenta;
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Size = new Size(35, 35);
            saveToolStripButton.Text = "Save";
            saveToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText;
            saveToolStripButton.Click += SaveToolStripButton_Click;
            // 
            // cancelToolStripButton
            // 
            cancelToolStripButton.Image = Properties.Resources.cancel_butt;
            cancelToolStripButton.ImageTransparentColor = Color.Magenta;
            cancelToolStripButton.Name = "cancelToolStripButton";
            cancelToolStripButton.Size = new Size(47, 35);
            cancelToolStripButton.Text = "Cancel";
            cancelToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText;
            cancelToolStripButton.Click += CancelToolStripButton_Click;
            // 
            // testToolStripButton
            // 
            testToolStripButton.Image = (Image)resources.GetObject("testToolStripButton.Image");
            testToolStripButton.ImageTransparentColor = Color.Magenta;
            testToolStripButton.Name = "testToolStripButton";
            testToolStripButton.Size = new Size(32, 35);
            testToolStripButton.Text = "Test";
            testToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText;
            testToolStripButton.Click += TestToolStripButton_Click;
            // 
            // exitToolStripButton
            // 
            exitToolStripButton.Alignment = ToolStripItemAlignment.Right;
            exitToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            exitToolStripButton.Image = Properties.Resources.exit;
            exitToolStripButton.ImageTransparentColor = Color.Magenta;
            exitToolStripButton.Name = "exitToolStripButton";
            exitToolStripButton.Size = new Size(23, 35);
            exitToolStripButton.Text = "Close";
            exitToolStripButton.Click += ExitToolStripButton_Click;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(115, 222);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(249, 23);
            passwordTextBox.TabIndex = 21;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(43, 226);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(60, 15);
            passwordLabel.TabIndex = 20;
            passwordLabel.Text = "Password:";
            // 
            // userIdTextBox
            // 
            userIdTextBox.Location = new Point(115, 180);
            userIdTextBox.Name = "userIdTextBox";
            userIdTextBox.Size = new Size(249, 23);
            userIdTextBox.TabIndex = 19;
            // 
            // userIdLabel
            // 
            userIdLabel.AutoSize = true;
            userIdLabel.Location = new Point(43, 184);
            userIdLabel.Name = "userIdLabel";
            userIdLabel.Size = new Size(33, 15);
            userIdLabel.TabIndex = 18;
            userIdLabel.Text = "User:";
            // 
            // databaseTextBox
            // 
            databaseTextBox.Location = new Point(115, 139);
            databaseTextBox.Name = "databaseTextBox";
            databaseTextBox.Size = new Size(249, 23);
            databaseTextBox.TabIndex = 17;
            // 
            // databaseLabel
            // 
            databaseLabel.AutoSize = true;
            databaseLabel.Location = new Point(43, 143);
            databaseLabel.Name = "databaseLabel";
            databaseLabel.Size = new Size(58, 15);
            databaseLabel.TabIndex = 16;
            databaseLabel.Text = "Database:";
            // 
            // serverTextBox
            // 
            serverTextBox.Location = new Point(115, 97);
            serverTextBox.Name = "serverTextBox";
            serverTextBox.Size = new Size(249, 23);
            serverTextBox.TabIndex = 15;
            // 
            // serverLabel
            // 
            serverLabel.AutoSize = true;
            serverLabel.Location = new Point(43, 101);
            serverLabel.Name = "serverLabel";
            serverLabel.Size = new Size(42, 15);
            serverLabel.TabIndex = 14;
            serverLabel.Text = "Server:";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(115, 59);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(249, 23);
            nameTextBox.TabIndex = 13;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(43, 63);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(42, 15);
            nameLabel.TabIndex = 12;
            nameLabel.Text = "Name:";
            // 
            // ConnectionEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(407, 290);
            Controls.Add(passwordTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(userIdTextBox);
            Controls.Add(userIdLabel);
            Controls.Add(databaseTextBox);
            Controls.Add(databaseLabel);
            Controls.Add(serverTextBox);
            Controls.Add(serverLabel);
            Controls.Add(nameTextBox);
            Controls.Add(nameLabel);
            Controls.Add(toolStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ConnectionEditor";
            Text = "Connection Editor";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton saveToolStripButton;
        private ToolStripButton cancelToolStripButton;
        private ToolStripButton testToolStripButton;
        private ToolStripButton exitToolStripButton;
        private TextBox passwordTextBox;
        private Label passwordLabel;
        private TextBox userIdTextBox;
        private Label userIdLabel;
        private TextBox databaseTextBox;
        private Label databaseLabel;
        private TextBox serverTextBox;
        private Label serverLabel;
        private TextBox nameTextBox;
        private Label nameLabel;
    }
}