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
            this.GameCompleteBox = new System.Windows.Forms.RichTextBox();
            this.GameJoinedLabel = new System.Windows.Forms.Label();
            this.GameActiveBox = new System.Windows.Forms.RichTextBox();
            this.GamePendingBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PlayerTwoScoreBox = new System.Windows.Forms.RichTextBox();
            this.PlayerOneScoreBox = new System.Windows.Forms.RichTextBox();
            this.PlayerTwoName = new System.Windows.Forms.Label();
            this.PlayerOneName = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.QuitGame = new System.Windows.Forms.Button();
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
            this.RegistrationPanel = new System.Windows.Forms.Panel();
            this.UsernameText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DomainText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.EnterGamePanel = new System.Windows.Forms.Panel();
            this.FindGameButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.GameDurationTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.GameBoard.SuspendLayout();
            this.RegistrationPanel.SuspendLayout();
            this.EnterGamePanel.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(790, 24);
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
            this.GameBoard.Controls.Add(this.GameCompleteBox);
            this.GameBoard.Controls.Add(this.GameJoinedLabel);
            this.GameBoard.Controls.Add(this.GameActiveBox);
            this.GameBoard.Controls.Add(this.GamePendingBox);
            this.GameBoard.Controls.Add(this.label2);
            this.GameBoard.Controls.Add(this.PlayerTwoScoreBox);
            this.GameBoard.Controls.Add(this.PlayerOneScoreBox);
            this.GameBoard.Controls.Add(this.PlayerTwoName);
            this.GameBoard.Controls.Add(this.PlayerOneName);
            this.GameBoard.Controls.Add(this.textBox1);
            this.GameBoard.Controls.Add(this.QuitGame);
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
            this.GameBoard.Location = new System.Drawing.Point(153, 76);
            this.GameBoard.Margin = new System.Windows.Forms.Padding(2);
            this.GameBoard.Name = "GameBoard";
            this.GameBoard.Size = new System.Drawing.Size(534, 333);
            this.GameBoard.TabIndex = 1;
            // 
            // GameCompleteBox
            // 
            this.GameCompleteBox.BackColor = System.Drawing.Color.Lime;
            this.GameCompleteBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameCompleteBox.Location = new System.Drawing.Point(392, 303);
            this.GameCompleteBox.Margin = new System.Windows.Forms.Padding(2);
            this.GameCompleteBox.Name = "GameCompleteBox";
            this.GameCompleteBox.ReadOnly = true;
            this.GameCompleteBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.GameCompleteBox.Size = new System.Drawing.Size(114, 22);
            this.GameCompleteBox.TabIndex = 45;
            this.GameCompleteBox.Text = "Game Complete";
            this.GameCompleteBox.Visible = false;
            // 
            // GameJoinedLabel
            // 
            this.GameJoinedLabel.AutoSize = true;
            this.GameJoinedLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.GameJoinedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameJoinedLabel.Location = new System.Drawing.Point(192, 17);
            this.GameJoinedLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.GameJoinedLabel.Name = "GameJoinedLabel";
            this.GameJoinedLabel.Size = new System.Drawing.Size(140, 24);
            this.GameJoinedLabel.TabIndex = 44;
            this.GameJoinedLabel.Text = "Game Joined!";
            this.GameJoinedLabel.Visible = false;
            // 
            // GameActiveBox
            // 
            this.GameActiveBox.BackColor = System.Drawing.Color.Yellow;
            this.GameActiveBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameActiveBox.Location = new System.Drawing.Point(392, 279);
            this.GameActiveBox.Margin = new System.Windows.Forms.Padding(2);
            this.GameActiveBox.Name = "GameActiveBox";
            this.GameActiveBox.ReadOnly = true;
            this.GameActiveBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.GameActiveBox.Size = new System.Drawing.Size(114, 22);
            this.GameActiveBox.TabIndex = 43;
            this.GameActiveBox.Text = "Game Active";
            this.GameActiveBox.Visible = false;
            // 
            // GamePendingBox
            // 
            this.GamePendingBox.BackColor = System.Drawing.Color.Red;
            this.GamePendingBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GamePendingBox.Location = new System.Drawing.Point(392, 254);
            this.GamePendingBox.Margin = new System.Windows.Forms.Padding(2);
            this.GamePendingBox.Name = "GamePendingBox";
            this.GamePendingBox.ReadOnly = true;
            this.GamePendingBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.GamePendingBox.Size = new System.Drawing.Size(114, 24);
            this.GamePendingBox.TabIndex = 42;
            this.GamePendingBox.Text = "Game Pending";
            this.GamePendingBox.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(388, 233);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 41;
            this.label2.Text = "Game Status:";
            // 
            // PlayerTwoScoreBox
            // 
            this.PlayerTwoScoreBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.85F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerTwoScoreBox.Location = new System.Drawing.Point(427, 23);
            this.PlayerTwoScoreBox.Margin = new System.Windows.Forms.Padding(2);
            this.PlayerTwoScoreBox.Name = "PlayerTwoScoreBox";
            this.PlayerTwoScoreBox.ReadOnly = true;
            this.PlayerTwoScoreBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.PlayerTwoScoreBox.Size = new System.Drawing.Size(56, 40);
            this.PlayerTwoScoreBox.TabIndex = 40;
            this.PlayerTwoScoreBox.Text = "000";
            // 
            // PlayerOneScoreBox
            // 
            this.PlayerOneScoreBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.85F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerOneScoreBox.Location = new System.Drawing.Point(46, 23);
            this.PlayerOneScoreBox.Margin = new System.Windows.Forms.Padding(2);
            this.PlayerOneScoreBox.Name = "PlayerOneScoreBox";
            this.PlayerOneScoreBox.ReadOnly = true;
            this.PlayerOneScoreBox.Size = new System.Drawing.Size(56, 40);
            this.PlayerOneScoreBox.TabIndex = 39;
            this.PlayerOneScoreBox.Text = "000";
            // 
            // PlayerTwoName
            // 
            this.PlayerTwoName.AutoSize = true;
            this.PlayerTwoName.Location = new System.Drawing.Point(424, 8);
            this.PlayerTwoName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PlayerTwoName.Name = "PlayerTwoName";
            this.PlayerTwoName.Size = new System.Drawing.Size(45, 13);
            this.PlayerTwoName.TabIndex = 38;
            this.PlayerTwoName.Text = "Player 2";
            // 
            // PlayerOneName
            // 
            this.PlayerOneName.AutoSize = true;
            this.PlayerOneName.Location = new System.Drawing.Point(43, 8);
            this.PlayerOneName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PlayerOneName.Name = "PlayerOneName";
            this.PlayerOneName.Size = new System.Drawing.Size(45, 13);
            this.PlayerOneName.TabIndex = 37;
            this.PlayerOneName.Text = "Player 1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 65);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(134, 165);
            this.textBox1.TabIndex = 36;
            // 
            // QuitGame
            // 
            this.QuitGame.Location = new System.Drawing.Point(290, 299);
            this.QuitGame.Margin = new System.Windows.Forms.Padding(2);
            this.QuitGame.Name = "QuitGame";
            this.QuitGame.Size = new System.Drawing.Size(82, 21);
            this.QuitGame.TabIndex = 35;
            this.QuitGame.Text = "Quit Game";
            this.QuitGame.UseVisualStyleBackColor = true;
            // 
            // TimeRemainingText
            // 
            this.TimeRemainingText.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeRemainingText.Location = new System.Drawing.Point(29, 263);
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
            this.label1.Location = new System.Drawing.Point(16, 236);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "TIME REMAINING:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // WordsPlayedText
            // 
            this.WordsPlayedText.Location = new System.Drawing.Point(392, 63);
            this.WordsPlayedText.Margin = new System.Windows.Forms.Padding(2);
            this.WordsPlayedText.Multiline = true;
            this.WordsPlayedText.Name = "WordsPlayedText";
            this.WordsPlayedText.ReadOnly = true;
            this.WordsPlayedText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.WordsPlayedText.Size = new System.Drawing.Size(134, 165);
            this.WordsPlayedText.TabIndex = 31;
            // 
            // SubmitWordText
            // 
            this.SubmitWordText.Location = new System.Drawing.Point(192, 279);
            this.SubmitWordText.Margin = new System.Windows.Forms.Padding(2);
            this.SubmitWordText.Name = "SubmitWordText";
            this.SubmitWordText.ReadOnly = true;
            this.SubmitWordText.Size = new System.Drawing.Size(150, 20);
            this.SubmitWordText.TabIndex = 30;
            // 
            // submitWordButton
            // 
            this.submitWordButton.Location = new System.Drawing.Point(164, 299);
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
            this.fourFourTxt.Location = new System.Drawing.Point(322, 214);
            this.fourFourTxt.Margin = new System.Windows.Forms.Padding(2);
            this.fourFourTxt.Multiline = true;
            this.fourFourTxt.Name = "fourFourTxt";
            this.fourFourTxt.ReadOnly = true;
            this.fourFourTxt.Size = new System.Drawing.Size(52, 54);
            this.fourFourTxt.TabIndex = 28;
            this.fourFourTxt.Text = "QU";
            this.fourFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fourFourTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fourFourTxt_MouseDown);
            // 
            // fourThreeTxt
            // 
            this.fourThreeTxt.AccessibleName = "";
            this.fourThreeTxt.BackColor = System.Drawing.Color.White;
            this.fourThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourThreeTxt.Location = new System.Drawing.Point(268, 214);
            this.fourThreeTxt.Margin = new System.Windows.Forms.Padding(2);
            this.fourThreeTxt.Multiline = true;
            this.fourThreeTxt.Name = "fourThreeTxt";
            this.fourThreeTxt.ReadOnly = true;
            this.fourThreeTxt.Size = new System.Drawing.Size(52, 54);
            this.fourThreeTxt.TabIndex = 27;
            this.fourThreeTxt.Text = "QU";
            this.fourThreeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fourThreeTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fourThreeTxt_MouseDown);
            // 
            // fourTwoTxt
            // 
            this.fourTwoTxt.AccessibleName = "";
            this.fourTwoTxt.BackColor = System.Drawing.Color.White;
            this.fourTwoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourTwoTxt.Location = new System.Drawing.Point(216, 214);
            this.fourTwoTxt.Margin = new System.Windows.Forms.Padding(2);
            this.fourTwoTxt.Multiline = true;
            this.fourTwoTxt.Name = "fourTwoTxt";
            this.fourTwoTxt.ReadOnly = true;
            this.fourTwoTxt.Size = new System.Drawing.Size(52, 54);
            this.fourTwoTxt.TabIndex = 26;
            this.fourTwoTxt.Text = "QU";
            this.fourTwoTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fourTwoTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fourTwoTxt_MouseDown);
            // 
            // fourOneTxt
            // 
            this.fourOneTxt.AccessibleName = "";
            this.fourOneTxt.BackColor = System.Drawing.Color.White;
            this.fourOneTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourOneTxt.Location = new System.Drawing.Point(162, 214);
            this.fourOneTxt.Margin = new System.Windows.Forms.Padding(2);
            this.fourOneTxt.Multiline = true;
            this.fourOneTxt.Name = "fourOneTxt";
            this.fourOneTxt.ReadOnly = true;
            this.fourOneTxt.Size = new System.Drawing.Size(52, 54);
            this.fourOneTxt.TabIndex = 25;
            this.fourOneTxt.Text = "QU";
            this.fourOneTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fourOneTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fourOneTxt_MouseDown);
            // 
            // threeFourTxt
            // 
            this.threeFourTxt.AccessibleName = "";
            this.threeFourTxt.BackColor = System.Drawing.Color.White;
            this.threeFourTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeFourTxt.Location = new System.Drawing.Point(322, 158);
            this.threeFourTxt.Margin = new System.Windows.Forms.Padding(2);
            this.threeFourTxt.Multiline = true;
            this.threeFourTxt.Name = "threeFourTxt";
            this.threeFourTxt.ReadOnly = true;
            this.threeFourTxt.Size = new System.Drawing.Size(52, 54);
            this.threeFourTxt.TabIndex = 24;
            this.threeFourTxt.Text = "QU";
            this.threeFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.threeFourTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.threeFourTxt_MouseDown);
            // 
            // threeThreeTxt
            // 
            this.threeThreeTxt.AccessibleName = "";
            this.threeThreeTxt.BackColor = System.Drawing.Color.White;
            this.threeThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeThreeTxt.Location = new System.Drawing.Point(268, 158);
            this.threeThreeTxt.Margin = new System.Windows.Forms.Padding(2);
            this.threeThreeTxt.Multiline = true;
            this.threeThreeTxt.Name = "threeThreeTxt";
            this.threeThreeTxt.ReadOnly = true;
            this.threeThreeTxt.Size = new System.Drawing.Size(52, 54);
            this.threeThreeTxt.TabIndex = 23;
            this.threeThreeTxt.Text = "QU";
            this.threeThreeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.threeThreeTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.threeThreeTxt_MouseDown);
            // 
            // threeTwoTxt
            // 
            this.threeTwoTxt.AccessibleName = "";
            this.threeTwoTxt.BackColor = System.Drawing.Color.White;
            this.threeTwoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeTwoTxt.Location = new System.Drawing.Point(216, 158);
            this.threeTwoTxt.Margin = new System.Windows.Forms.Padding(2);
            this.threeTwoTxt.Multiline = true;
            this.threeTwoTxt.Name = "threeTwoTxt";
            this.threeTwoTxt.ReadOnly = true;
            this.threeTwoTxt.Size = new System.Drawing.Size(52, 54);
            this.threeTwoTxt.TabIndex = 22;
            this.threeTwoTxt.Text = "QU";
            this.threeTwoTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.threeTwoTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.threeTwoTxt_MouseDown);
            // 
            // threeOneTxt
            // 
            this.threeOneTxt.AccessibleName = "";
            this.threeOneTxt.BackColor = System.Drawing.Color.White;
            this.threeOneTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeOneTxt.Location = new System.Drawing.Point(162, 158);
            this.threeOneTxt.Margin = new System.Windows.Forms.Padding(2);
            this.threeOneTxt.Multiline = true;
            this.threeOneTxt.Name = "threeOneTxt";
            this.threeOneTxt.ReadOnly = true;
            this.threeOneTxt.Size = new System.Drawing.Size(52, 54);
            this.threeOneTxt.TabIndex = 3;
            this.threeOneTxt.Text = "QU";
            this.threeOneTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.threeOneTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.threeOneTxt_MouseDown);
            // 
            // twoFourTxt
            // 
            this.twoFourTxt.AccessibleName = "";
            this.twoFourTxt.BackColor = System.Drawing.Color.White;
            this.twoFourTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoFourTxt.Location = new System.Drawing.Point(322, 103);
            this.twoFourTxt.Margin = new System.Windows.Forms.Padding(2);
            this.twoFourTxt.Multiline = true;
            this.twoFourTxt.Name = "twoFourTxt";
            this.twoFourTxt.ReadOnly = true;
            this.twoFourTxt.Size = new System.Drawing.Size(52, 54);
            this.twoFourTxt.TabIndex = 21;
            this.twoFourTxt.Text = "QU";
            this.twoFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.twoFourTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.twoFourTxt_MouseDown);
            // 
            // twoThreeTxt
            // 
            this.twoThreeTxt.AccessibleName = "";
            this.twoThreeTxt.BackColor = System.Drawing.Color.White;
            this.twoThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoThreeTxt.Location = new System.Drawing.Point(268, 103);
            this.twoThreeTxt.Margin = new System.Windows.Forms.Padding(2);
            this.twoThreeTxt.Multiline = true;
            this.twoThreeTxt.Name = "twoThreeTxt";
            this.twoThreeTxt.ReadOnly = true;
            this.twoThreeTxt.Size = new System.Drawing.Size(52, 54);
            this.twoThreeTxt.TabIndex = 20;
            this.twoThreeTxt.Text = "QU";
            this.twoThreeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.twoThreeTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.twoThreeTxt_MouseDown);
            // 
            // twoTwoTxt
            // 
            this.twoTwoTxt.AccessibleName = "";
            this.twoTwoTxt.BackColor = System.Drawing.Color.White;
            this.twoTwoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoTwoTxt.Location = new System.Drawing.Point(216, 103);
            this.twoTwoTxt.Margin = new System.Windows.Forms.Padding(2);
            this.twoTwoTxt.Multiline = true;
            this.twoTwoTxt.Name = "twoTwoTxt";
            this.twoTwoTxt.ReadOnly = true;
            this.twoTwoTxt.Size = new System.Drawing.Size(52, 54);
            this.twoTwoTxt.TabIndex = 19;
            this.twoTwoTxt.Text = "QU";
            this.twoTwoTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.twoTwoTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.twoTwoTxt_MouseDown);
            // 
            // twoOneTxt
            // 
            this.twoOneTxt.AccessibleName = "";
            this.twoOneTxt.BackColor = System.Drawing.Color.White;
            this.twoOneTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoOneTxt.Location = new System.Drawing.Point(162, 103);
            this.twoOneTxt.Margin = new System.Windows.Forms.Padding(2);
            this.twoOneTxt.Multiline = true;
            this.twoOneTxt.Name = "twoOneTxt";
            this.twoOneTxt.ReadOnly = true;
            this.twoOneTxt.Size = new System.Drawing.Size(52, 54);
            this.twoOneTxt.TabIndex = 18;
            this.twoOneTxt.Text = "QU";
            this.twoOneTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.twoOneTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.twoOneTxt_MouseDown);
            // 
            // oneFourTxt
            // 
            this.oneFourTxt.AccessibleName = "";
            this.oneFourTxt.BackColor = System.Drawing.Color.White;
            this.oneFourTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneFourTxt.Location = new System.Drawing.Point(322, 48);
            this.oneFourTxt.Margin = new System.Windows.Forms.Padding(2);
            this.oneFourTxt.Multiline = true;
            this.oneFourTxt.Name = "oneFourTxt";
            this.oneFourTxt.ReadOnly = true;
            this.oneFourTxt.Size = new System.Drawing.Size(52, 54);
            this.oneFourTxt.TabIndex = 17;
            this.oneFourTxt.Text = "QU";
            this.oneFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.oneFourTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.oneFourTxt_MouseDown);
            // 
            // oneThreeTxt
            // 
            this.oneThreeTxt.AccessibleName = "";
            this.oneThreeTxt.BackColor = System.Drawing.Color.White;
            this.oneThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneThreeTxt.Location = new System.Drawing.Point(268, 48);
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
            this.oneTwoTxt.Location = new System.Drawing.Point(216, 48);
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
            this.oneOneTxt.Location = new System.Drawing.Point(162, 48);
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
            this.BoggleLabel.Location = new System.Drawing.Point(366, 30);
            this.BoggleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BoggleLabel.Name = "BoggleLabel";
            this.BoggleLabel.Size = new System.Drawing.Size(138, 44);
            this.BoggleLabel.TabIndex = 2;
            this.BoggleLabel.Text = "Boggle";
            // 
            // RegistrationPanel
            // 
            this.RegistrationPanel.BackColor = System.Drawing.Color.Cyan;
            this.RegistrationPanel.Controls.Add(this.UsernameText);
            this.RegistrationPanel.Controls.Add(this.label5);
            this.RegistrationPanel.Controls.Add(this.DomainText);
            this.RegistrationPanel.Controls.Add(this.label4);
            this.RegistrationPanel.Controls.Add(this.CancelButton);
            this.RegistrationPanel.Controls.Add(this.RegisterButton);
            this.RegistrationPanel.Controls.Add(this.label3);
            this.RegistrationPanel.Location = new System.Drawing.Point(14, 30);
            this.RegistrationPanel.Margin = new System.Windows.Forms.Padding(2);
            this.RegistrationPanel.Name = "RegistrationPanel";
            this.RegistrationPanel.Size = new System.Drawing.Size(126, 168);
            this.RegistrationPanel.TabIndex = 3;
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
            this.CancelButton.Size = new System.Drawing.Size(73, 19);
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
            this.RegisterButton.Size = new System.Drawing.Size(73, 22);
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
            // EnterGamePanel
            // 
            this.EnterGamePanel.BackColor = System.Drawing.Color.Cyan;
            this.EnterGamePanel.Controls.Add(this.FindGameButton);
            this.EnterGamePanel.Controls.Add(this.label7);
            this.EnterGamePanel.Controls.Add(this.GameDurationTxt);
            this.EnterGamePanel.Controls.Add(this.label6);
            this.EnterGamePanel.Enabled = false;
            this.EnterGamePanel.Location = new System.Drawing.Point(14, 210);
            this.EnterGamePanel.Margin = new System.Windows.Forms.Padding(2);
            this.EnterGamePanel.Name = "EnterGamePanel";
            this.EnterGamePanel.Size = new System.Drawing.Size(129, 158);
            this.EnterGamePanel.TabIndex = 4;
            // 
            // FindGameButton
            // 
            this.FindGameButton.Location = new System.Drawing.Point(27, 66);
            this.FindGameButton.Margin = new System.Windows.Forms.Padding(2);
            this.FindGameButton.Name = "FindGameButton";
            this.FindGameButton.Size = new System.Drawing.Size(73, 20);
            this.FindGameButton.TabIndex = 3;
            this.FindGameButton.Text = "Find Game";
            this.FindGameButton.UseVisualStyleBackColor = true;
            this.FindGameButton.Click += new System.EventHandler(this.FindGameButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 32);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Desired Duration (5-120)";
            // 
            // GameDurationTxt
            // 
            this.GameDurationTxt.Location = new System.Drawing.Point(12, 46);
            this.GameDurationTxt.Margin = new System.Windows.Forms.Padding(2);
            this.GameDurationTxt.Name = "GameDurationTxt";
            this.GameDurationTxt.Size = new System.Drawing.Size(96, 20);
            this.GameDurationTxt.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(30, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Enter Game";
            // 
            // BoggleWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(790, 464);
            this.Controls.Add(this.EnterGamePanel);
            this.Controls.Add(this.RegistrationPanel);
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
            this.RegistrationPanel.ResumeLayout(false);
            this.RegistrationPanel.PerformLayout();
            this.EnterGamePanel.ResumeLayout(false);
            this.EnterGamePanel.PerformLayout();
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
        private System.Windows.Forms.Panel RegistrationPanel;
        private System.Windows.Forms.Button QuitGame;
        private System.Windows.Forms.TextBox UsernameText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox DomainText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel EnterGamePanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox PlayerTwoScoreBox;
        private System.Windows.Forms.RichTextBox PlayerOneScoreBox;
        private System.Windows.Forms.Label PlayerTwoName;
        private System.Windows.Forms.Label PlayerOneName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button FindGameButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox GameDurationTxt;
        private System.Windows.Forms.RichTextBox GameActiveBox;
        private System.Windows.Forms.RichTextBox GamePendingBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label GameJoinedLabel;
        private System.Windows.Forms.RichTextBox GameCompleteBox;
    }
}

