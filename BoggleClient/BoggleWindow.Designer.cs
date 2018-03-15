namespace BoggleClient
{
    partial class BoggleWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameBoard = new System.Windows.Forms.Panel();
            this.QuitGame = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TimeRemainingText = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.WordsPlayedText = new System.Windows.Forms.TextBox();
            this.SubmitWordText = new System.Windows.Forms.TextBox();
            this.submitWordButton = new System.Windows.Forms.Button();
            this.fourFourTxt = new System.Windows.Forms.TextBox();
            this.fourThreeTxt = new System.Windows.Forms.TextBox();
            this.fourTwoTxt = new System.Windows.Forms.TextBox();
            this.fourOneTxt = new System.Windows.Forms.TextBox();
            this.threeFourTxt = new System.Windows.Forms.TextBox();
            this.threeThreeTxt = new System.Windows.Forms.TextBox();
            this.threeTwoTxt = new System.Windows.Forms.TextBox();
            this.threeOneTxt = new System.Windows.Forms.TextBox();
            this.twoFourTxt = new System.Windows.Forms.TextBox();
            this.twoThreeTxt = new System.Windows.Forms.TextBox();
            this.twoTwoTxt = new System.Windows.Forms.TextBox();
            this.twoOneTxt = new System.Windows.Forms.TextBox();
            this.oneFourTxt = new System.Windows.Forms.TextBox();
            this.oneThreeTxt = new System.Windows.Forms.TextBox();
            this.oneTwoTxt = new System.Windows.Forms.TextBox();
            this.oneOneTxt = new System.Windows.Forms.TextBox();
            this.BoggleLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.UsernameText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DomainText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.StatsText = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.GameBoard.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(712, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // GameBoard
            // 
            this.GameBoard.BackColor = System.Drawing.Color.Cyan;
            this.GameBoard.Controls.Add(this.QuitGame);
            this.GameBoard.Controls.Add(this.label2);
            this.GameBoard.Controls.Add(this.TimeRemainingText);
            this.GameBoard.Controls.Add(this.label1);
            this.GameBoard.Controls.Add(this.WordsPlayedText);
            this.GameBoard.Controls.Add(this.SubmitWordText);
            this.GameBoard.Controls.Add(this.submitWordButton);
            this.GameBoard.Controls.Add(this.fourFourTxt);
            this.GameBoard.Controls.Add(this.fourThreeTxt);
            this.GameBoard.Controls.Add(this.fourTwoTxt);
            this.GameBoard.Controls.Add(this.fourOneTxt);
            this.GameBoard.Controls.Add(this.threeFourTxt);
            this.GameBoard.Controls.Add(this.threeThreeTxt);
            this.GameBoard.Controls.Add(this.threeTwoTxt);
            this.GameBoard.Controls.Add(this.threeOneTxt);
            this.GameBoard.Controls.Add(this.twoFourTxt);
            this.GameBoard.Controls.Add(this.twoThreeTxt);
            this.GameBoard.Controls.Add(this.twoTwoTxt);
            this.GameBoard.Controls.Add(this.twoOneTxt);
            this.GameBoard.Controls.Add(this.oneFourTxt);
            this.GameBoard.Controls.Add(this.oneThreeTxt);
            this.GameBoard.Controls.Add(this.oneTwoTxt);
            this.GameBoard.Controls.Add(this.oneOneTxt);
            this.GameBoard.Enabled = false;
            this.GameBoard.Location = new System.Drawing.Point(166, 102);
            this.GameBoard.Margin = new System.Windows.Forms.Padding(2);
            this.GameBoard.Name = "GameBoard";
            this.GameBoard.Size = new System.Drawing.Size(442, 326);
            this.GameBoard.TabIndex = 1;
            // 
            // QuitGame
            // 
            this.QuitGame.Location = new System.Drawing.Point(285, 299);
            this.QuitGame.Margin = new System.Windows.Forms.Padding(2);
            this.QuitGame.Name = "QuitGame";
            this.QuitGame.Size = new System.Drawing.Size(145, 21);
            this.QuitGame.TabIndex = 35;
            this.QuitGame.Text = "Quit Game";
            this.QuitGame.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe MDL2 Assets", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(280, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 14);
            this.label2.TabIndex = 34;
            this.label2.Text = "WORDS PLAYED";
            // 
            // TimeRemainingText
            // 
            this.TimeRemainingText.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeRemainingText.Location = new System.Drawing.Point(315, 243);
            this.TimeRemainingText.Margin = new System.Windows.Forms.Padding(2);
            this.TimeRemainingText.Name = "TimeRemainingText";
            this.TimeRemainingText.ReadOnly = true;
            this.TimeRemainingText.Size = new System.Drawing.Size(82, 52);
            this.TimeRemainingText.TabIndex = 33;
            this.TimeRemainingText.Text = "1:20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(292, 225);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "TIME REMAINING:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // WordsPlayedText
            // 
            this.WordsPlayedText.Location = new System.Drawing.Point(263, 23);
            this.WordsPlayedText.Margin = new System.Windows.Forms.Padding(2);
            this.WordsPlayedText.Multiline = true;
            this.WordsPlayedText.Name = "WordsPlayedText";
            this.WordsPlayedText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.WordsPlayedText.Size = new System.Drawing.Size(134, 190);
            this.WordsPlayedText.TabIndex = 31;
            // 
            // SubmitWordText
            // 
            this.SubmitWordText.Location = new System.Drawing.Point(30, 248);
            this.SubmitWordText.Margin = new System.Windows.Forms.Padding(2);
            this.SubmitWordText.Name = "SubmitWordText";
            this.SubmitWordText.Size = new System.Drawing.Size(150, 20);
            this.SubmitWordText.TabIndex = 30;
            // 
            // submitWordButton
            // 
            this.submitWordButton.Location = new System.Drawing.Point(190, 248);
            this.submitWordButton.Margin = new System.Windows.Forms.Padding(2);
            this.submitWordButton.Name = "submitWordButton";
            this.submitWordButton.Size = new System.Drawing.Size(81, 21);
            this.submitWordButton.TabIndex = 29;
            this.submitWordButton.Text = "Submit Word";
            this.submitWordButton.UseVisualStyleBackColor = true;
            // 
            // fourFourTxt
            // 
            this.fourFourTxt.AccessibleName = "";
            this.fourFourTxt.BackColor = System.Drawing.Color.White;
            this.fourFourTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourFourTxt.Location = new System.Drawing.Point(190, 183);
            this.fourFourTxt.Margin = new System.Windows.Forms.Padding(2);
            this.fourFourTxt.Multiline = true;
            this.fourFourTxt.Name = "fourFourTxt";
            this.fourFourTxt.ReadOnly = true;
            this.fourFourTxt.Size = new System.Drawing.Size(52, 54);
            this.fourFourTxt.TabIndex = 28;
            this.fourFourTxt.Text = "QU";
            this.fourFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fourThreeTxt
            // 
            this.fourThreeTxt.AccessibleName = "";
            this.fourThreeTxt.BackColor = System.Drawing.Color.White;
            this.fourThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourThreeTxt.Location = new System.Drawing.Point(136, 183);
            this.fourThreeTxt.Margin = new System.Windows.Forms.Padding(2);
            this.fourThreeTxt.Multiline = true;
            this.fourThreeTxt.Name = "fourThreeTxt";
            this.fourThreeTxt.ReadOnly = true;
            this.fourThreeTxt.Size = new System.Drawing.Size(52, 54);
            this.fourThreeTxt.TabIndex = 27;
            this.fourThreeTxt.Text = "QU";
            this.fourThreeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fourTwoTxt
            // 
            this.fourTwoTxt.AccessibleName = "";
            this.fourTwoTxt.BackColor = System.Drawing.Color.White;
            this.fourTwoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourTwoTxt.Location = new System.Drawing.Point(84, 183);
            this.fourTwoTxt.Margin = new System.Windows.Forms.Padding(2);
            this.fourTwoTxt.Multiline = true;
            this.fourTwoTxt.Name = "fourTwoTxt";
            this.fourTwoTxt.ReadOnly = true;
            this.fourTwoTxt.Size = new System.Drawing.Size(52, 54);
            this.fourTwoTxt.TabIndex = 26;
            this.fourTwoTxt.Text = "QU";
            this.fourTwoTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fourOneTxt
            // 
            this.fourOneTxt.AccessibleName = "";
            this.fourOneTxt.BackColor = System.Drawing.Color.White;
            this.fourOneTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourOneTxt.Location = new System.Drawing.Point(30, 183);
            this.fourOneTxt.Margin = new System.Windows.Forms.Padding(2);
            this.fourOneTxt.Multiline = true;
            this.fourOneTxt.Name = "fourOneTxt";
            this.fourOneTxt.ReadOnly = true;
            this.fourOneTxt.Size = new System.Drawing.Size(52, 54);
            this.fourOneTxt.TabIndex = 25;
            this.fourOneTxt.Text = "QU";
            this.fourOneTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // threeFourTxt
            // 
            this.threeFourTxt.AccessibleName = "";
            this.threeFourTxt.BackColor = System.Drawing.Color.White;
            this.threeFourTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeFourTxt.Location = new System.Drawing.Point(190, 127);
            this.threeFourTxt.Margin = new System.Windows.Forms.Padding(2);
            this.threeFourTxt.Multiline = true;
            this.threeFourTxt.Name = "threeFourTxt";
            this.threeFourTxt.ReadOnly = true;
            this.threeFourTxt.Size = new System.Drawing.Size(52, 54);
            this.threeFourTxt.TabIndex = 24;
            this.threeFourTxt.Text = "QU";
            this.threeFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // threeThreeTxt
            // 
            this.threeThreeTxt.AccessibleName = "";
            this.threeThreeTxt.BackColor = System.Drawing.Color.White;
            this.threeThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeThreeTxt.Location = new System.Drawing.Point(136, 127);
            this.threeThreeTxt.Margin = new System.Windows.Forms.Padding(2);
            this.threeThreeTxt.Multiline = true;
            this.threeThreeTxt.Name = "threeThreeTxt";
            this.threeThreeTxt.ReadOnly = true;
            this.threeThreeTxt.Size = new System.Drawing.Size(52, 54);
            this.threeThreeTxt.TabIndex = 23;
            this.threeThreeTxt.Text = "QU";
            this.threeThreeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // threeTwoTxt
            // 
            this.threeTwoTxt.AccessibleName = "";
            this.threeTwoTxt.BackColor = System.Drawing.Color.White;
            this.threeTwoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeTwoTxt.Location = new System.Drawing.Point(84, 127);
            this.threeTwoTxt.Margin = new System.Windows.Forms.Padding(2);
            this.threeTwoTxt.Multiline = true;
            this.threeTwoTxt.Name = "threeTwoTxt";
            this.threeTwoTxt.ReadOnly = true;
            this.threeTwoTxt.Size = new System.Drawing.Size(52, 54);
            this.threeTwoTxt.TabIndex = 22;
            this.threeTwoTxt.Text = "QU";
            this.threeTwoTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // threeOneTxt
            // 
            this.threeOneTxt.AccessibleName = "";
            this.threeOneTxt.BackColor = System.Drawing.Color.White;
            this.threeOneTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeOneTxt.Location = new System.Drawing.Point(30, 127);
            this.threeOneTxt.Margin = new System.Windows.Forms.Padding(2);
            this.threeOneTxt.Multiline = true;
            this.threeOneTxt.Name = "threeOneTxt";
            this.threeOneTxt.ReadOnly = true;
            this.threeOneTxt.Size = new System.Drawing.Size(52, 54);
            this.threeOneTxt.TabIndex = 3;
            this.threeOneTxt.Text = "QU";
            this.threeOneTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // twoFourTxt
            // 
            this.twoFourTxt.AccessibleName = "";
            this.twoFourTxt.BackColor = System.Drawing.Color.White;
            this.twoFourTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoFourTxt.Location = new System.Drawing.Point(190, 72);
            this.twoFourTxt.Margin = new System.Windows.Forms.Padding(2);
            this.twoFourTxt.Multiline = true;
            this.twoFourTxt.Name = "twoFourTxt";
            this.twoFourTxt.ReadOnly = true;
            this.twoFourTxt.Size = new System.Drawing.Size(52, 54);
            this.twoFourTxt.TabIndex = 21;
            this.twoFourTxt.Text = "QU";
            this.twoFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // twoThreeTxt
            // 
            this.twoThreeTxt.AccessibleName = "";
            this.twoThreeTxt.BackColor = System.Drawing.Color.White;
            this.twoThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoThreeTxt.Location = new System.Drawing.Point(136, 72);
            this.twoThreeTxt.Margin = new System.Windows.Forms.Padding(2);
            this.twoThreeTxt.Multiline = true;
            this.twoThreeTxt.Name = "twoThreeTxt";
            this.twoThreeTxt.ReadOnly = true;
            this.twoThreeTxt.Size = new System.Drawing.Size(52, 54);
            this.twoThreeTxt.TabIndex = 20;
            this.twoThreeTxt.Text = "QU";
            this.twoThreeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // twoTwoTxt
            // 
            this.twoTwoTxt.AccessibleName = "";
            this.twoTwoTxt.BackColor = System.Drawing.Color.White;
            this.twoTwoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoTwoTxt.Location = new System.Drawing.Point(84, 72);
            this.twoTwoTxt.Margin = new System.Windows.Forms.Padding(2);
            this.twoTwoTxt.Multiline = true;
            this.twoTwoTxt.Name = "twoTwoTxt";
            this.twoTwoTxt.ReadOnly = true;
            this.twoTwoTxt.Size = new System.Drawing.Size(52, 54);
            this.twoTwoTxt.TabIndex = 19;
            this.twoTwoTxt.Text = "QU";
            this.twoTwoTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // twoOneTxt
            // 
            this.twoOneTxt.AccessibleName = "";
            this.twoOneTxt.BackColor = System.Drawing.Color.White;
            this.twoOneTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoOneTxt.Location = new System.Drawing.Point(30, 72);
            this.twoOneTxt.Margin = new System.Windows.Forms.Padding(2);
            this.twoOneTxt.Multiline = true;
            this.twoOneTxt.Name = "twoOneTxt";
            this.twoOneTxt.ReadOnly = true;
            this.twoOneTxt.Size = new System.Drawing.Size(52, 54);
            this.twoOneTxt.TabIndex = 18;
            this.twoOneTxt.Text = "QU";
            this.twoOneTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // oneFourTxt
            // 
            this.oneFourTxt.AccessibleName = "";
            this.oneFourTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneFourTxt.Location = new System.Drawing.Point(190, 17);
            this.oneFourTxt.Margin = new System.Windows.Forms.Padding(2);
            this.oneFourTxt.Multiline = true;
            this.oneFourTxt.Name = "oneFourTxt";
            this.oneFourTxt.ReadOnly = true;
            this.oneFourTxt.Size = new System.Drawing.Size(52, 54);
            this.oneFourTxt.TabIndex = 17;
            this.oneFourTxt.Text = "QU";
            this.oneFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // oneThreeTxt
            // 
            this.oneThreeTxt.AccessibleName = "";
            this.oneThreeTxt.BackColor = System.Drawing.Color.White;
            this.oneThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneThreeTxt.Location = new System.Drawing.Point(136, 17);
            this.oneThreeTxt.Margin = new System.Windows.Forms.Padding(2);
            this.oneThreeTxt.Multiline = true;
            this.oneThreeTxt.Name = "oneThreeTxt";
            this.oneThreeTxt.ReadOnly = true;
            this.oneThreeTxt.Size = new System.Drawing.Size(52, 54);
            this.oneThreeTxt.TabIndex = 3;
            this.oneThreeTxt.Text = "QU";
            this.oneThreeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.oneThreeTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.oneThreetxt_MouseDown);
            // 
            // oneTwoTxt
            // 
            this.oneTwoTxt.AccessibleName = "";
            this.oneTwoTxt.BackColor = System.Drawing.Color.White;
            this.oneTwoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneTwoTxt.Location = new System.Drawing.Point(84, 17);
            this.oneTwoTxt.Margin = new System.Windows.Forms.Padding(2);
            this.oneTwoTxt.Multiline = true;
            this.oneTwoTxt.Name = "oneTwoTxt";
            this.oneTwoTxt.ReadOnly = true;
            this.oneTwoTxt.Size = new System.Drawing.Size(52, 54);
            this.oneTwoTxt.TabIndex = 16;
            this.oneTwoTxt.Text = "QU";
            this.oneTwoTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.oneTwoTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.oneTwoTxt_MouseDown);
            // 
            // oneOneTxt
            // 
            this.oneOneTxt.AccessibleName = "";
            this.oneOneTxt.BackColor = System.Drawing.Color.White;
            this.oneOneTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneOneTxt.Location = new System.Drawing.Point(30, 17);
            this.oneOneTxt.Margin = new System.Windows.Forms.Padding(2);
            this.oneOneTxt.Multiline = true;
            this.oneOneTxt.Name = "oneOneTxt";
            this.oneOneTxt.ReadOnly = true;
            this.oneOneTxt.Size = new System.Drawing.Size(52, 54);
            this.oneOneTxt.TabIndex = 0;
            this.oneOneTxt.Text = "QU";
            this.oneOneTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.oneOneTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.oneOneTxt_MouseDown);
            // 
            // BoggleLabel
            // 
            this.BoggleLabel.AutoSize = true;
            this.BoggleLabel.BackColor = System.Drawing.Color.Gray;
            this.BoggleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoggleLabel.Location = new System.Drawing.Point(327, 44);
            this.BoggleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BoggleLabel.Name = "BoggleLabel";
            this.BoggleLabel.Size = new System.Drawing.Size(138, 44);
            this.BoggleLabel.TabIndex = 2;
            this.BoggleLabel.Text = "Boggle";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Cyan;
            this.panel2.Controls.Add(this.UsernameText);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.DomainText);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.CancelButton);
            this.panel2.Controls.Add(this.RegisterButton);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(14, 30);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(121, 174);
            this.panel2.TabIndex = 3;
            // 
            // UsernameText
            // 
            this.UsernameText.Location = new System.Drawing.Point(12, 95);
            this.UsernameText.Margin = new System.Windows.Forms.Padding(2);
            this.UsernameText.Name = "UsernameText";
            this.UsernameText.Size = new System.Drawing.Size(100, 20);
            this.UsernameText.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 80);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "USERNAME:";
            // 
            // DomainText
            // 
            this.DomainText.Location = new System.Drawing.Point(10, 58);
            this.DomainText.Margin = new System.Windows.Forms.Padding(2);
            this.DomainText.Name = "DomainText";
            this.DomainText.Size = new System.Drawing.Size(102, 20);
            this.DomainText.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "DOMAIN NAME:";
            // 
            // CancelButton
            // 
            this.CancelButton.Enabled = false;
            this.CancelButton.Location = new System.Drawing.Point(27, 144);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(67, 28);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "CANCEL";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(27, 118);
            this.RegisterButton.Margin = new System.Windows.Forms.Padding(2);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(66, 22);
            this.RegisterButton.TabIndex = 1;
            this.RegisterButton.Text = "REGISTER";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "REGISTRATION";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Cyan;
            this.panel3.Controls.Add(this.StatsText);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(14, 220);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(121, 209);
            this.panel3.TabIndex = 4;
            // 
            // StatsText
            // 
            this.StatsText.Location = new System.Drawing.Point(10, 33);
            this.StatsText.Margin = new System.Windows.Forms.Padding(2);
            this.StatsText.Name = "StatsText";
            this.StatsText.Size = new System.Drawing.Size(102, 144);
            this.StatsText.TabIndex = 1;
            this.StatsText.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(35, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "STATS:";
            // 
            // BoggleWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(712, 506);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.BoggleLabel);
            this.Controls.Add(this.GameBoard);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BoggleWindow";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.GameBoard.ResumeLayout(false);
            this.GameBoard.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel GameBoard;
        private System.Windows.Forms.TextBox oneOneTxt;
        private System.Windows.Forms.Label BoggleLabel;
        private System.Windows.Forms.TextBox fourFourTxt;
        private System.Windows.Forms.TextBox fourThreeTxt;
        private System.Windows.Forms.TextBox fourTwoTxt;
        private System.Windows.Forms.TextBox fourOneTxt;
        private System.Windows.Forms.TextBox threeFourTxt;
        private System.Windows.Forms.TextBox threeThreeTxt;
        private System.Windows.Forms.TextBox threeTwoTxt;
        private System.Windows.Forms.TextBox threeOneTxt;
        private System.Windows.Forms.TextBox twoFourTxt;
        private System.Windows.Forms.TextBox twoThreeTxt;
        private System.Windows.Forms.TextBox twoTwoTxt;
        private System.Windows.Forms.TextBox twoOneTxt;
        private System.Windows.Forms.TextBox oneFourTxt;
        private System.Windows.Forms.TextBox oneThreeTxt;
        private System.Windows.Forms.TextBox oneTwoTxt;
        private System.Windows.Forms.Button submitWordButton;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.RichTextBox TimeRemainingText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox WordsPlayedText;
        private System.Windows.Forms.TextBox SubmitWordText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button QuitGame;
        private System.Windows.Forms.TextBox UsernameText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox DomainText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox StatsText;
        private System.Windows.Forms.Label label6;
    }
}

