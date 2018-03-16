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
            this.GameCompleteBox = new System.Windows.Forms.RichTextBox();
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
            this.menuStrip1.Size = new System.Drawing.Size(1580, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 36);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(77, 36);
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
            this.GameBoard.Location = new System.Drawing.Point(306, 147);
            this.GameBoard.Margin = new System.Windows.Forms.Padding(4);
            this.GameBoard.Name = "GameBoard";
            this.GameBoard.Size = new System.Drawing.Size(1067, 640);
            this.GameBoard.TabIndex = 1;
            // 
            // GameJoinedLabel
            // 
            this.GameJoinedLabel.AutoSize = true;
            this.GameJoinedLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.GameJoinedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameJoinedLabel.Location = new System.Drawing.Point(385, 32);
            this.GameJoinedLabel.Name = "GameJoinedLabel";
            this.GameJoinedLabel.Size = new System.Drawing.Size(262, 42);
            this.GameJoinedLabel.TabIndex = 44;
            this.GameJoinedLabel.Text = "Game Joined!";
            this.GameJoinedLabel.Visible = false;
            // 
            // GameActiveBox
            // 
            this.GameActiveBox.BackColor = System.Drawing.Color.Yellow;
            this.GameActiveBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameActiveBox.Location = new System.Drawing.Point(784, 537);
            this.GameActiveBox.Name = "GameActiveBox";
            this.GameActiveBox.ReadOnly = true;
            this.GameActiveBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.GameActiveBox.Size = new System.Drawing.Size(225, 39);
            this.GameActiveBox.TabIndex = 43;
            this.GameActiveBox.Text = "Game Active";
            this.GameActiveBox.Visible = false;
            // 
            // GamePendingBox
            // 
            this.GamePendingBox.BackColor = System.Drawing.Color.Red;
            this.GamePendingBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GamePendingBox.Location = new System.Drawing.Point(784, 488);
            this.GamePendingBox.Name = "GamePendingBox";
            this.GamePendingBox.ReadOnly = true;
            this.GamePendingBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.GamePendingBox.Size = new System.Drawing.Size(225, 43);
            this.GamePendingBox.TabIndex = 42;
            this.GamePendingBox.Text = "Game Pending";
            this.GamePendingBox.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(777, 448);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 37);
            this.label2.TabIndex = 41;
            this.label2.Text = "Game Status:";
            // 
            // PlayerTwoScoreBox
            // 
            this.PlayerTwoScoreBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.85F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerTwoScoreBox.Location = new System.Drawing.Point(854, 44);
            this.PlayerTwoScoreBox.Name = "PlayerTwoScoreBox";
            this.PlayerTwoScoreBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.PlayerTwoScoreBox.Size = new System.Drawing.Size(107, 73);
            this.PlayerTwoScoreBox.TabIndex = 40;
            this.PlayerTwoScoreBox.Text = "000";
            // 
            // PlayerOneScoreBox
            // 
            this.PlayerOneScoreBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.85F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerOneScoreBox.Location = new System.Drawing.Point(91, 45);
            this.PlayerOneScoreBox.Name = "PlayerOneScoreBox";
            this.PlayerOneScoreBox.Size = new System.Drawing.Size(107, 73);
            this.PlayerOneScoreBox.TabIndex = 39;
            this.PlayerOneScoreBox.Text = "000";
            // 
            // PlayerTwoName
            // 
            this.PlayerTwoName.AutoSize = true;
            this.PlayerTwoName.Location = new System.Drawing.Point(849, 16);
            this.PlayerTwoName.Name = "PlayerTwoName";
            this.PlayerTwoName.Size = new System.Drawing.Size(91, 25);
            this.PlayerTwoName.TabIndex = 38;
            this.PlayerTwoName.Text = "Player 2";
            // 
            // PlayerOneName
            // 
            this.PlayerOneName.AutoSize = true;
            this.PlayerOneName.Location = new System.Drawing.Point(86, 16);
            this.PlayerOneName.Name = "PlayerOneName";
            this.PlayerOneName.Size = new System.Drawing.Size(91, 25);
            this.PlayerOneName.TabIndex = 37;
            this.PlayerOneName.Text = "Player 1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(17, 125);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(264, 314);
            this.textBox1.TabIndex = 36;
            // 
            // QuitGame
            // 
            this.QuitGame.Location = new System.Drawing.Point(581, 575);
            this.QuitGame.Margin = new System.Windows.Forms.Padding(4);
            this.QuitGame.Name = "QuitGame";
            this.QuitGame.Size = new System.Drawing.Size(164, 40);
            this.QuitGame.TabIndex = 35;
            this.QuitGame.Text = "Quit Game";
            this.QuitGame.UseVisualStyleBackColor = true;
            // 
            // TimeRemainingText
            // 
            this.TimeRemainingText.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeRemainingText.Location = new System.Drawing.Point(58, 505);
            this.TimeRemainingText.Margin = new System.Windows.Forms.Padding(4);
            this.TimeRemainingText.Name = "TimeRemainingText";
            this.TimeRemainingText.ReadOnly = true;
            this.TimeRemainingText.Size = new System.Drawing.Size(160, 96);
            this.TimeRemainingText.TabIndex = 33;
            this.TimeRemainingText.Text = "1:20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 453);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 32);
            this.label1.TabIndex = 32;
            this.label1.Text = "TIME REMAINING:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // WordsPlayedText
            // 
            this.WordsPlayedText.Location = new System.Drawing.Point(784, 122);
            this.WordsPlayedText.Margin = new System.Windows.Forms.Padding(4);
            this.WordsPlayedText.Multiline = true;
            this.WordsPlayedText.Name = "WordsPlayedText";
            this.WordsPlayedText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.WordsPlayedText.Size = new System.Drawing.Size(264, 314);
            this.WordsPlayedText.TabIndex = 31;
            // 
            // SubmitWordText
            // 
            this.SubmitWordText.Location = new System.Drawing.Point(385, 536);
            this.SubmitWordText.Margin = new System.Windows.Forms.Padding(4);
            this.SubmitWordText.Name = "SubmitWordText";
            this.SubmitWordText.Size = new System.Drawing.Size(296, 31);
            this.SubmitWordText.TabIndex = 30;
            // 
            // submitWordButton
            // 
            this.submitWordButton.Location = new System.Drawing.Point(327, 575);
            this.submitWordButton.Margin = new System.Windows.Forms.Padding(4);
            this.submitWordButton.Name = "submitWordButton";
            this.submitWordButton.Size = new System.Drawing.Size(162, 40);
            this.submitWordButton.TabIndex = 29;
            this.submitWordButton.Text = "Submit Word";
            this.submitWordButton.UseVisualStyleBackColor = true;
            // 
            // fourFourTxt
            // 
            this.fourFourTxt.AccessibleName = "";
            this.fourFourTxt.BackColor = System.Drawing.Color.White;
            this.fourFourTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourFourTxt.Location = new System.Drawing.Point(645, 412);
            this.fourFourTxt.Margin = new System.Windows.Forms.Padding(4);
            this.fourFourTxt.Multiline = true;
            this.fourFourTxt.Name = "fourFourTxt";
            this.fourFourTxt.ReadOnly = true;
            this.fourFourTxt.Size = new System.Drawing.Size(100, 100);
            this.fourFourTxt.TabIndex = 28;
            this.fourFourTxt.Text = "QU";
            this.fourFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fourThreeTxt
            // 
            this.fourThreeTxt.AccessibleName = "";
            this.fourThreeTxt.BackColor = System.Drawing.Color.White;
            this.fourThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourThreeTxt.Location = new System.Drawing.Point(537, 412);
            this.fourThreeTxt.Margin = new System.Windows.Forms.Padding(4);
            this.fourThreeTxt.Multiline = true;
            this.fourThreeTxt.Name = "fourThreeTxt";
            this.fourThreeTxt.ReadOnly = true;
            this.fourThreeTxt.Size = new System.Drawing.Size(100, 100);
            this.fourThreeTxt.TabIndex = 27;
            this.fourThreeTxt.Text = "QU";
            this.fourThreeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fourTwoTxt
            // 
            this.fourTwoTxt.AccessibleName = "";
            this.fourTwoTxt.BackColor = System.Drawing.Color.White;
            this.fourTwoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourTwoTxt.Location = new System.Drawing.Point(433, 412);
            this.fourTwoTxt.Margin = new System.Windows.Forms.Padding(4);
            this.fourTwoTxt.Multiline = true;
            this.fourTwoTxt.Name = "fourTwoTxt";
            this.fourTwoTxt.ReadOnly = true;
            this.fourTwoTxt.Size = new System.Drawing.Size(100, 100);
            this.fourTwoTxt.TabIndex = 26;
            this.fourTwoTxt.Text = "QU";
            this.fourTwoTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fourOneTxt
            // 
            this.fourOneTxt.AccessibleName = "";
            this.fourOneTxt.BackColor = System.Drawing.Color.White;
            this.fourOneTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourOneTxt.Location = new System.Drawing.Point(325, 412);
            this.fourOneTxt.Margin = new System.Windows.Forms.Padding(4);
            this.fourOneTxt.Multiline = true;
            this.fourOneTxt.Name = "fourOneTxt";
            this.fourOneTxt.ReadOnly = true;
            this.fourOneTxt.Size = new System.Drawing.Size(100, 100);
            this.fourOneTxt.TabIndex = 25;
            this.fourOneTxt.Text = "QU";
            this.fourOneTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // threeFourTxt
            // 
            this.threeFourTxt.AccessibleName = "";
            this.threeFourTxt.BackColor = System.Drawing.Color.White;
            this.threeFourTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeFourTxt.Location = new System.Drawing.Point(645, 304);
            this.threeFourTxt.Margin = new System.Windows.Forms.Padding(4);
            this.threeFourTxt.Multiline = true;
            this.threeFourTxt.Name = "threeFourTxt";
            this.threeFourTxt.ReadOnly = true;
            this.threeFourTxt.Size = new System.Drawing.Size(100, 100);
            this.threeFourTxt.TabIndex = 24;
            this.threeFourTxt.Text = "QU";
            this.threeFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // threeThreeTxt
            // 
            this.threeThreeTxt.AccessibleName = "";
            this.threeThreeTxt.BackColor = System.Drawing.Color.White;
            this.threeThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeThreeTxt.Location = new System.Drawing.Point(537, 304);
            this.threeThreeTxt.Margin = new System.Windows.Forms.Padding(4);
            this.threeThreeTxt.Multiline = true;
            this.threeThreeTxt.Name = "threeThreeTxt";
            this.threeThreeTxt.ReadOnly = true;
            this.threeThreeTxt.Size = new System.Drawing.Size(100, 100);
            this.threeThreeTxt.TabIndex = 23;
            this.threeThreeTxt.Text = "QU";
            this.threeThreeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // threeTwoTxt
            // 
            this.threeTwoTxt.AccessibleName = "";
            this.threeTwoTxt.BackColor = System.Drawing.Color.White;
            this.threeTwoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeTwoTxt.Location = new System.Drawing.Point(433, 304);
            this.threeTwoTxt.Margin = new System.Windows.Forms.Padding(4);
            this.threeTwoTxt.Multiline = true;
            this.threeTwoTxt.Name = "threeTwoTxt";
            this.threeTwoTxt.ReadOnly = true;
            this.threeTwoTxt.Size = new System.Drawing.Size(100, 100);
            this.threeTwoTxt.TabIndex = 22;
            this.threeTwoTxt.Text = "QU";
            this.threeTwoTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // threeOneTxt
            // 
            this.threeOneTxt.AccessibleName = "";
            this.threeOneTxt.BackColor = System.Drawing.Color.White;
            this.threeOneTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeOneTxt.Location = new System.Drawing.Point(325, 304);
            this.threeOneTxt.Margin = new System.Windows.Forms.Padding(4);
            this.threeOneTxt.Multiline = true;
            this.threeOneTxt.Name = "threeOneTxt";
            this.threeOneTxt.ReadOnly = true;
            this.threeOneTxt.Size = new System.Drawing.Size(100, 100);
            this.threeOneTxt.TabIndex = 3;
            this.threeOneTxt.Text = "QU";
            this.threeOneTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // twoFourTxt
            // 
            this.twoFourTxt.AccessibleName = "";
            this.twoFourTxt.BackColor = System.Drawing.Color.White;
            this.twoFourTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoFourTxt.Location = new System.Drawing.Point(645, 198);
            this.twoFourTxt.Margin = new System.Windows.Forms.Padding(4);
            this.twoFourTxt.Multiline = true;
            this.twoFourTxt.Name = "twoFourTxt";
            this.twoFourTxt.ReadOnly = true;
            this.twoFourTxt.Size = new System.Drawing.Size(100, 100);
            this.twoFourTxt.TabIndex = 21;
            this.twoFourTxt.Text = "QU";
            this.twoFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // twoThreeTxt
            // 
            this.twoThreeTxt.AccessibleName = "";
            this.twoThreeTxt.BackColor = System.Drawing.Color.White;
            this.twoThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoThreeTxt.Location = new System.Drawing.Point(537, 198);
            this.twoThreeTxt.Margin = new System.Windows.Forms.Padding(4);
            this.twoThreeTxt.Multiline = true;
            this.twoThreeTxt.Name = "twoThreeTxt";
            this.twoThreeTxt.ReadOnly = true;
            this.twoThreeTxt.Size = new System.Drawing.Size(100, 100);
            this.twoThreeTxt.TabIndex = 20;
            this.twoThreeTxt.Text = "QU";
            this.twoThreeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // twoTwoTxt
            // 
            this.twoTwoTxt.AccessibleName = "";
            this.twoTwoTxt.BackColor = System.Drawing.Color.White;
            this.twoTwoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoTwoTxt.Location = new System.Drawing.Point(433, 198);
            this.twoTwoTxt.Margin = new System.Windows.Forms.Padding(4);
            this.twoTwoTxt.Multiline = true;
            this.twoTwoTxt.Name = "twoTwoTxt";
            this.twoTwoTxt.ReadOnly = true;
            this.twoTwoTxt.Size = new System.Drawing.Size(100, 100);
            this.twoTwoTxt.TabIndex = 19;
            this.twoTwoTxt.Text = "QU";
            this.twoTwoTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // twoOneTxt
            // 
            this.twoOneTxt.AccessibleName = "";
            this.twoOneTxt.BackColor = System.Drawing.Color.White;
            this.twoOneTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoOneTxt.Location = new System.Drawing.Point(325, 198);
            this.twoOneTxt.Margin = new System.Windows.Forms.Padding(4);
            this.twoOneTxt.Multiline = true;
            this.twoOneTxt.Name = "twoOneTxt";
            this.twoOneTxt.ReadOnly = true;
            this.twoOneTxt.Size = new System.Drawing.Size(100, 100);
            this.twoOneTxt.TabIndex = 18;
            this.twoOneTxt.Text = "QU";
            this.twoOneTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // oneFourTxt
            // 
            this.oneFourTxt.AccessibleName = "";
            this.oneFourTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneFourTxt.Location = new System.Drawing.Point(645, 93);
            this.oneFourTxt.Margin = new System.Windows.Forms.Padding(4);
            this.oneFourTxt.Multiline = true;
            this.oneFourTxt.Name = "oneFourTxt";
            this.oneFourTxt.ReadOnly = true;
            this.oneFourTxt.Size = new System.Drawing.Size(100, 100);
            this.oneFourTxt.TabIndex = 17;
            this.oneFourTxt.Text = "QU";
            this.oneFourTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // oneThreeTxt
            // 
            this.oneThreeTxt.AccessibleName = "";
            this.oneThreeTxt.BackColor = System.Drawing.Color.White;
            this.oneThreeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneThreeTxt.Location = new System.Drawing.Point(537, 93);
            this.oneThreeTxt.Margin = new System.Windows.Forms.Padding(4);
            this.oneThreeTxt.Multiline = true;
            this.oneThreeTxt.Name = "oneThreeTxt";
            this.oneThreeTxt.ReadOnly = true;
            this.oneThreeTxt.Size = new System.Drawing.Size(100, 100);
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
            this.oneTwoTxt.Location = new System.Drawing.Point(433, 93);
            this.oneTwoTxt.Margin = new System.Windows.Forms.Padding(4);
            this.oneTwoTxt.Multiline = true;
            this.oneTwoTxt.Name = "oneTwoTxt";
            this.oneTwoTxt.ReadOnly = true;
            this.oneTwoTxt.Size = new System.Drawing.Size(100, 100);
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
            this.oneOneTxt.Location = new System.Drawing.Point(325, 93);
            this.oneOneTxt.Margin = new System.Windows.Forms.Padding(4);
            this.oneOneTxt.Multiline = true;
            this.oneOneTxt.Name = "oneOneTxt";
            this.oneOneTxt.ReadOnly = true;
            this.oneOneTxt.Size = new System.Drawing.Size(100, 100);
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
            this.BoggleLabel.Location = new System.Drawing.Point(731, 58);
            this.BoggleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.BoggleLabel.Name = "BoggleLabel";
            this.BoggleLabel.Size = new System.Drawing.Size(272, 85);
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
            this.RegistrationPanel.Location = new System.Drawing.Point(28, 58);
            this.RegistrationPanel.Margin = new System.Windows.Forms.Padding(4);
            this.RegistrationPanel.Name = "RegistrationPanel";
            this.RegistrationPanel.Size = new System.Drawing.Size(251, 324);
            this.RegistrationPanel.TabIndex = 3;
            // 
            // UsernameText
            // 
            this.UsernameText.Location = new System.Drawing.Point(24, 183);
            this.UsernameText.Margin = new System.Windows.Forms.Padding(4);
            this.UsernameText.Name = "UsernameText";
            this.UsernameText.Size = new System.Drawing.Size(196, 31);
            this.UsernameText.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 154);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "USERNAME:";
            // 
            // DomainText
            // 
            this.DomainText.Location = new System.Drawing.Point(20, 112);
            this.DomainText.Margin = new System.Windows.Forms.Padding(4);
            this.DomainText.Name = "DomainText";
            this.DomainText.Size = new System.Drawing.Size(200, 31);
            this.DomainText.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 73);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "DOMAIN NAME:";
            // 
            // CancelButton
            // 
            this.CancelButton.Enabled = false;
            this.CancelButton.Location = new System.Drawing.Point(54, 277);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(146, 36);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "CANCEL";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(54, 227);
            this.RegisterButton.Margin = new System.Windows.Forms.Padding(4);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(146, 42);
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
            this.label3.Location = new System.Drawing.Point(14, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 29);
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
            this.EnterGamePanel.Location = new System.Drawing.Point(28, 404);
            this.EnterGamePanel.Margin = new System.Windows.Forms.Padding(4);
            this.EnterGamePanel.Name = "EnterGamePanel";
            this.EnterGamePanel.Size = new System.Drawing.Size(258, 304);
            this.EnterGamePanel.TabIndex = 4;
            // 
            // FindGameButton
            // 
            this.FindGameButton.Location = new System.Drawing.Point(54, 126);
            this.FindGameButton.Name = "FindGameButton";
            this.FindGameButton.Size = new System.Drawing.Size(146, 39);
            this.FindGameButton.TabIndex = 3;
            this.FindGameButton.Text = "Find Game";
            this.FindGameButton.UseVisualStyleBackColor = true;
            this.FindGameButton.Click += new System.EventHandler(this.FindGameButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(248, 25);
            this.label7.TabIndex = 2;
            this.label7.Text = "Desired Duration (5-120)";
            // 
            // GameDurationTxt
            // 
            this.GameDurationTxt.Location = new System.Drawing.Point(25, 89);
            this.GameDurationTxt.Name = "GameDurationTxt";
            this.GameDurationTxt.Size = new System.Drawing.Size(189, 31);
            this.GameDurationTxt.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(60, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "Enter Game";
            // 
            // GameCompleteBox
            // 
            this.GameCompleteBox.BackColor = System.Drawing.Color.Lime;
            this.GameCompleteBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameCompleteBox.Location = new System.Drawing.Point(784, 582);
            this.GameCompleteBox.Name = "GameCompleteBox";
            this.GameCompleteBox.ReadOnly = true;
            this.GameCompleteBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.GameCompleteBox.Size = new System.Drawing.Size(225, 39);
            this.GameCompleteBox.TabIndex = 45;
            this.GameCompleteBox.Text = "Game Complete";
            this.GameCompleteBox.Visible = false;
            // 
            // BoggleWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1580, 893);
            this.Controls.Add(this.EnterGamePanel);
            this.Controls.Add(this.RegistrationPanel);
            this.Controls.Add(this.BoggleLabel);
            this.Controls.Add(this.GameBoard);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
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

