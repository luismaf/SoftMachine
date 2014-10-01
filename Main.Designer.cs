namespace SoftMachine
{
    partial class Main
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clbTables = new System.Windows.Forms.CheckedListBox();
            this.gbSelectTables = new System.Windows.Forms.GroupBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.cbxRowsAffected = new System.Windows.Forms.CheckBox();
            this.cbxLasterInsertId = new System.Windows.Forms.CheckBox();
            this.cbxDeleteFKUN = new System.Windows.Forms.CheckBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.gbCode = new System.Windows.Forms.GroupBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gbSP = new System.Windows.Forms.GroupBox();
            this.txtSP = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gbSelectTables.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.gbCode.SuspendLayout();
            this.gbSP.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.txtDB);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtUserId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 23);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(651, 43);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection Information";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(584, 17);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(56, 19);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(457, 19);
            this.txtDB.Margin = new System.Windows.Forms.Padding(2);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(114, 20);
            this.txtDB.TabIndex = 9;
            this.txtDB.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(432, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "DB";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(370, 19);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(56, 20);
            this.txtPassword.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(314, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(256, 19);
            this.txtUserId.Margin = new System.Windows.Forms.Padding(2);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(55, 20);
            this.txtUserId.TabIndex = 5;
            this.txtUserId.Text = "root";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "User ID";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(160, 19);
            this.txtPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(38, 20);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "3306";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(50, 19);
            this.txtServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(76, 20);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1262, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // clbTables
            // 
            this.clbTables.CheckOnClick = true;
            this.clbTables.FormattingEnabled = true;
            this.clbTables.Location = new System.Drawing.Point(2, 42);
            this.clbTables.Margin = new System.Windows.Forms.Padding(2);
            this.clbTables.Name = "clbTables";
            this.clbTables.ScrollAlwaysVisible = true;
            this.clbTables.Size = new System.Drawing.Size(122, 454);
            this.clbTables.TabIndex = 2;
            // 
            // gbSelectTables
            // 
            this.gbSelectTables.Controls.Add(this.btnSelectAll);
            this.gbSelectTables.Controls.Add(this.clbTables);
            this.gbSelectTables.Enabled = false;
            this.gbSelectTables.Location = new System.Drawing.Point(9, 71);
            this.gbSelectTables.Margin = new System.Windows.Forms.Padding(2);
            this.gbSelectTables.Name = "gbSelectTables";
            this.gbSelectTables.Padding = new System.Windows.Forms.Padding(2);
            this.gbSelectTables.Size = new System.Drawing.Size(125, 503);
            this.gbSelectTables.TabIndex = 3;
            this.gbSelectTables.TabStop = false;
            this.gbSelectTables.Text = "Select Tables";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(22, 17);
            this.btnSelectAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(79, 19);
            this.btnSelectAll.TabIndex = 6;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.cbxRowsAffected);
            this.gbOptions.Controls.Add(this.cbxLasterInsertId);
            this.gbOptions.Controls.Add(this.cbxDeleteFKUN);
            this.gbOptions.Controls.Add(this.btnGenerate);
            this.gbOptions.Enabled = false;
            this.gbOptions.Location = new System.Drawing.Point(141, 71);
            this.gbOptions.Margin = new System.Windows.Forms.Padding(2);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Padding = new System.Windows.Forms.Padding(2);
            this.gbOptions.Size = new System.Drawing.Size(517, 184);
            this.gbOptions.TabIndex = 4;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // cbxRowsAffected
            // 
            this.cbxRowsAffected.AutoSize = true;
            this.cbxRowsAffected.Location = new System.Drawing.Point(380, 100);
            this.cbxRowsAffected.Margin = new System.Windows.Forms.Padding(2);
            this.cbxRowsAffected.Name = "cbxRowsAffected";
            this.cbxRowsAffected.Size = new System.Drawing.Size(96, 17);
            this.cbxRowsAffected.TabIndex = 12;
            this.cbxRowsAffected.Text = "Rows Affected";
            this.cbxRowsAffected.UseVisualStyleBackColor = true;
            // 
            // cbxLasterInsertId
            // 
            this.cbxLasterInsertId.AutoSize = true;
            this.cbxLasterInsertId.Location = new System.Drawing.Point(380, 79);
            this.cbxLasterInsertId.Margin = new System.Windows.Forms.Padding(2);
            this.cbxLasterInsertId.Name = "cbxLasterInsertId";
            this.cbxLasterInsertId.Size = new System.Drawing.Size(87, 17);
            this.cbxLasterInsertId.TabIndex = 11;
            this.cbxLasterInsertId.Text = "Last Insert Id";
            this.cbxLasterInsertId.UseVisualStyleBackColor = true;
            // 
            // cbxDeleteFKUN
            // 
            this.cbxDeleteFKUN.AutoSize = true;
            this.cbxDeleteFKUN.Location = new System.Drawing.Point(380, 58);
            this.cbxDeleteFKUN.Margin = new System.Windows.Forms.Padding(2);
            this.cbxDeleteFKUN.Name = "cbxDeleteFKUN";
            this.cbxDeleteFKUN.Size = new System.Drawing.Size(112, 17);
            this.cbxDeleteFKUN.TabIndex = 9;
            this.cbxDeleteFKUN.Text = "Delete For FK/UN";
            this.cbxDeleteFKUN.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(437, 160);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(70, 19);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // gbCode
            // 
            this.gbCode.Controls.Add(this.txtCode);
            this.gbCode.Enabled = false;
            this.gbCode.Location = new System.Drawing.Point(664, 23);
            this.gbCode.Margin = new System.Windows.Forms.Padding(2);
            this.gbCode.Name = "gbCode";
            this.gbCode.Padding = new System.Windows.Forms.Padding(2);
            this.gbCode.Size = new System.Drawing.Size(573, 559);
            this.gbCode.TabIndex = 5;
            this.gbCode.TabStop = false;
            this.gbCode.Text = "Result";
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.Black;
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(2, 15);
            this.txtCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCode.Size = new System.Drawing.Size(569, 542);
            this.txtCode.TabIndex = 0;
            this.txtCode.WordWrap = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // gbSP
            // 
            this.gbSP.Controls.Add(this.txtSP);
            this.gbSP.Location = new System.Drawing.Point(138, 260);
            this.gbSP.Name = "gbSP";
            this.gbSP.Size = new System.Drawing.Size(511, 321);
            this.gbSP.TabIndex = 6;
            this.gbSP.TabStop = false;
            this.gbSP.Text = "Stored Procedures";
            // 
            // txtSP
            // 
            this.txtSP.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSP.Location = new System.Drawing.Point(6, 19);
            this.txtSP.Multiline = true;
            this.txtSP.Name = "txtSP";
            this.txtSP.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSP.Size = new System.Drawing.Size(499, 296);
            this.txtSP.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 593);
            this.Controls.Add(this.gbSP);
            this.Controls.Add(this.gbCode);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.gbSelectTables);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SoftMachine v1.01";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbSelectTables.ResumeLayout(false);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.gbCode.ResumeLayout(false);
            this.gbCode.PerformLayout();
            this.gbSP.ResumeLayout(false);
            this.gbSP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.CheckedListBox clbTables;
        private System.Windows.Forms.GroupBox gbSelectTables;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox cbxDeleteFKUN;
        private System.Windows.Forms.CheckBox cbxRowsAffected;
        private System.Windows.Forms.CheckBox cbxLasterInsertId;
        private System.Windows.Forms.GroupBox gbSP;
        private System.Windows.Forms.TextBox txtSP;
    }
}

