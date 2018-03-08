namespace SpreadsheetGUI
{
    partial class SSWindow
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
            this.spreadsheetPanel = new SSGui.SpreadsheetPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cellBox = new System.Windows.Forms.TextBox();
            this.contentsBox = new System.Windows.Forms.TextBox();
            this.cellLabel = new System.Windows.Forms.Label();
            this.contentsLabel = new System.Windows.Forms.Label();
            this.valueBox = new System.Windows.Forms.TextBox();
            this.valueLabel = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFromSpreadsheet = new System.Windows.Forms.ToolStripMenuItem();
            this.NewSpreadsheet = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // spreadsheetPanel
            // 
            this.spreadsheetPanel.AutoSize = true;
            this.spreadsheetPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.spreadsheetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetPanel.Location = new System.Drawing.Point(0, 72);
            this.spreadsheetPanel.Margin = new System.Windows.Forms.Padding(1);
            this.spreadsheetPanel.Name = "spreadsheetPanel";
            this.spreadsheetPanel.Size = new System.Drawing.Size(886, 406);
            this.spreadsheetPanel.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Location = new System.Drawing.Point(0, 48);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(886, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "File";
            // 
            // cellBox
            // 
            this.cellBox.AccessibleName = "";
            this.cellBox.Location = new System.Drawing.Point(263, 16);
            this.cellBox.Margin = new System.Windows.Forms.Padding(2);
            this.cellBox.Name = "cellBox";
            this.cellBox.ReadOnly = true;
            this.cellBox.Size = new System.Drawing.Size(95, 20);
            this.cellBox.TabIndex = 2;
            // 
            // contentsBox
            // 
            this.contentsBox.Location = new System.Drawing.Point(626, 17);
            this.contentsBox.Margin = new System.Windows.Forms.Padding(2);
            this.contentsBox.Name = "contentsBox";
            this.contentsBox.Size = new System.Drawing.Size(96, 20);
            this.contentsBox.TabIndex = 4;
            // 
            // cellLabel
            // 
            this.cellLabel.AutoSize = true;
            this.cellLabel.Location = new System.Drawing.Point(226, 17);
            this.cellLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cellLabel.Name = "cellLabel";
            this.cellLabel.Size = new System.Drawing.Size(24, 13);
            this.cellLabel.TabIndex = 5;
            this.cellLabel.Text = "Cell";
            this.cellLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // contentsLabel
            // 
            this.contentsLabel.AutoSize = true;
            this.contentsLabel.Location = new System.Drawing.Point(568, 17);
            this.contentsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.contentsLabel.Name = "contentsLabel";
            this.contentsLabel.Size = new System.Drawing.Size(49, 13);
            this.contentsLabel.TabIndex = 6;
            this.contentsLabel.Text = "Contents";
            // 
            // valueBox
            // 
            this.valueBox.Location = new System.Drawing.Point(436, 17);
            this.valueBox.Margin = new System.Windows.Forms.Padding(2);
            this.valueBox.Name = "valueBox";
            this.valueBox.ReadOnly = true;
            this.valueBox.Size = new System.Drawing.Size(112, 20);
            this.valueBox.TabIndex = 7;
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(386, 17);
            this.valueLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(34, 13);
            this.valueLabel.TabIndex = 8;
            this.valueLabel.Text = "Value";
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip2.Location = new System.Drawing.Point(0, 24);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip2.Size = new System.Drawing.Size(886, 24);
            this.menuStrip2.TabIndex = 9;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // menuStrip3
            // 
            this.menuStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip3.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.helpToolStripMenuItem});
            this.menuStrip3.Location = new System.Drawing.Point(0, 0);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Padding = new System.Windows.Forms.Padding(2, 1, 0, 1);
            this.menuStrip3.Size = new System.Drawing.Size(886, 24);
            this.menuStrip3.TabIndex = 10;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.openMenu,
            this.closeMenu});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 22);
            this.menuFile.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // openMenu
            // 
            this.openMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFromSpreadsheet,
            this.NewSpreadsheet});
            this.openMenu.Name = "openMenu";
            this.openMenu.Size = new System.Drawing.Size(152, 22);
            this.openMenu.Text = "Open";
            // 
            // OpenFromSpreadsheet
            // 
            this.OpenFromSpreadsheet.Name = "OpenFromSpreadsheet";
            this.OpenFromSpreadsheet.Size = new System.Drawing.Size(134, 22);
            this.OpenFromSpreadsheet.Text = "Open From";
            this.OpenFromSpreadsheet.Click += new System.EventHandler(this.OpenFromSpreadsheet_Click);
            // 
            // NewSpreadsheet
            // 
            this.NewSpreadsheet.Name = "NewSpreadsheet";
            this.NewSpreadsheet.Size = new System.Drawing.Size(134, 22);
            this.NewSpreadsheet.Text = "New";
            this.NewSpreadsheet.Click += new System.EventHandler(this.NewSpreadsheet_Click);
            // 
            // closeMenu
            // 
            this.closeMenu.Name = "closeMenu";
            this.closeMenu.Size = new System.Drawing.Size(152, 22);
            this.closeMenu.Text = "Close Window";
            this.closeMenu.Click += new System.EventHandler(this.closeMenu_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SSWindow
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(886, 478);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.contentsLabel);
            this.Controls.Add(this.cellLabel);
            this.Controls.Add(this.contentsBox);
            this.Controls.Add(this.cellBox);
            this.Controls.Add(this.spreadsheetPanel);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.menuStrip3);
            this.MainMenuStrip = this.menuStrip2;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SSWindow";
            this.Text = "Spreadsheet";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip3.ResumeLayout(false);
            this.menuStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SSGui.SpreadsheetPanel spreadsheetPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TextBox cellBox;
        private System.Windows.Forms.TextBox contentsBox;
        private System.Windows.Forms.Label cellLabel;
        private System.Windows.Forms.Label contentsLabel;
        private System.Windows.Forms.TextBox valueBox;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.MenuStrip menuStrip3;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenu;
        private System.Windows.Forms.ToolStripMenuItem closeMenu;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFromSpreadsheet;
        private System.Windows.Forms.ToolStripMenuItem NewSpreadsheet;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

