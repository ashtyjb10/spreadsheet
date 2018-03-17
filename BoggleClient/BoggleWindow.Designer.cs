using System;

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
            this.CancelFindGame = new System.Windows.Forms.Button();
            this.GameCompleteBox = new System.Windows.Forms.RichTextBox();
            this.GameJoinedLabel = new System.Windows.Forms.Label();
            this.GameActiveBox = new System.Windows.Forms.RichTextBox();
            this.GamePendingBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PlayerTwoScoreBox = new System.Windows.Forms.RichTextBox();
            this.PlayerOneScoreBox = new System.Windows.Forms.RichTextBox();
            this.PlayerTwoName = new System.Windows.Forms.Label();
            this.PlayerOneName = new System.Windows.Forms.Label();
            this.wordsPlayedP1Txt = new System.Windows.Forms.TextBox();
            this.QuitGame = new System.Windows.Forms.Button();
            this.TimeRemainingText = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.wordsPlayedP2Txt = new System.Windows.Forms.TextBox();
            this.SubmitWordText = new System.Windows.Forms.TextBox();
            this.submitWordButton = new System.Windows.Forms.Button();
            this.letter16 = new System.Windows.Forms.TextBox();
            this.letter15 = new System.Windows.Forms.TextBox();
            this.letter14 = new System.Windows.Forms.TextBox();
            this.letter13 = new System.Windows.Forms.TextBox();
            this.letter12 = new System.Windows.Forms.TextBox();
            this.letter11 = new System.Windows.Forms.TextBox();
            this.letter10 = new System.Windows.Forms.TextBox();
            this.letter9 = new System.Windows.Forms.TextBox();
            this.letter8 = new System.Windows.Forms.TextBox();
            this.letter7 = new System.Windows.Forms.TextBox();
            this.letter6 = new System.Windows.Forms.TextBox();
            this.letter5 = new System.Windows.Forms.TextBox();
            this.letter4 = new System.Windows.Forms.TextBox();
            this.letter3 = new System.Windows.Forms.TextBox();
            this.letter2 = new System.Windows.Forms.TextBox();
            this.letter1 = new System.Windows.Forms.TextBox();
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
            this.helpMePlayBoggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpMePlayBoggleToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // GameBoard
            // 
            this.GameBoard.BackColor = System.Drawing.Color.Cyan;
            this.GameBoard.Controls.Add(this.CancelFindGame);
            this.GameBoard.Controls.Add(this.GameCompleteBox);
            this.GameBoard.Controls.Add(this.GameJoinedLabel);
            this.GameBoard.Controls.Add(this.GameActiveBox);
            this.GameBoard.Controls.Add(this.GamePendingBox);
            this.GameBoard.Controls.Add(this.label2);
            this.GameBoard.Controls.Add(this.PlayerTwoScoreBox);
            this.GameBoard.Controls.Add(this.PlayerOneScoreBox);
            this.GameBoard.Controls.Add(this.PlayerTwoName);
            this.GameBoard.Controls.Add(this.PlayerOneName);
            this.GameBoard.Controls.Add(this.wordsPlayedP1Txt);
            this.GameBoard.Controls.Add(this.QuitGame);
            this.GameBoard.Controls.Add(this.TimeRemainingText);
            this.GameBoard.Controls.Add(this.label1);
            this.GameBoard.Controls.Add(this.wordsPlayedP2Txt);
            this.GameBoard.Controls.Add(this.SubmitWordText);
            this.GameBoard.Controls.Add(this.submitWordButton);
            this.GameBoard.Controls.Add(this.letter16);
            this.GameBoard.Controls.Add(this.letter15);
            this.GameBoard.Controls.Add(this.letter14);
            this.GameBoard.Controls.Add(this.letter13);
            this.GameBoard.Controls.Add(this.letter12);
            this.GameBoard.Controls.Add(this.letter11);
            this.GameBoard.Controls.Add(this.letter10);
            this.GameBoard.Controls.Add(this.letter9);
            this.GameBoard.Controls.Add(this.letter8);
            this.GameBoard.Controls.Add(this.letter7);
            this.GameBoard.Controls.Add(this.letter6);
            this.GameBoard.Controls.Add(this.letter5);
            this.GameBoard.Controls.Add(this.letter4);
            this.GameBoard.Controls.Add(this.letter3);
            this.GameBoard.Controls.Add(this.letter2);
            this.GameBoard.Controls.Add(this.letter1);
            this.GameBoard.Enabled = false;
            this.GameBoard.Location = new System.Drawing.Point(153, 76);
            this.GameBoard.Margin = new System.Windows.Forms.Padding(2);
            this.GameBoard.Name = "GameBoard";
            this.GameBoard.Size = new System.Drawing.Size(534, 333);
            this.GameBoard.TabIndex = 1;
            // 
            // CancelFindGame
            // 
            this.CancelFindGame.Location = new System.Drawing.Point(296, 294);
            this.CancelFindGame.Margin = new System.Windows.Forms.Padding(2);
            this.CancelFindGame.Name = "CancelFindGame";
            this.CancelFindGame.Size = new System.Drawing.Size(78, 31);
            this.CancelFindGame.TabIndex = 4;
            this.CancelFindGame.Text = "Cancel Find";
            this.CancelFindGame.UseVisualStyleBackColor = true;
            this.CancelFindGame.Click += new System.EventHandler(this.CancelFindGame_Click_1);
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
            // wordsPlayedP1Txt
            // 
            this.wordsPlayedP1Txt.AcceptsReturn = true;
            this.wordsPlayedP1Txt.Location = new System.Drawing.Point(9, 62);
            this.wordsPlayedP1Txt.Margin = new System.Windows.Forms.Padding(2);
            this.wordsPlayedP1Txt.Multiline = true;
            this.wordsPlayedP1Txt.Name = "wordsPlayedP1Txt";
            this.wordsPlayedP1Txt.ReadOnly = true;
            this.wordsPlayedP1Txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.wordsPlayedP1Txt.Size = new System.Drawing.Size(134, 165);
            this.wordsPlayedP1Txt.TabIndex = 36;
            // 
            // QuitGame
            // 
            this.QuitGame.Location = new System.Drawing.Point(211, 299);
            this.QuitGame.Margin = new System.Windows.Forms.Padding(1);
            this.QuitGame.Name = "QuitGame";
            this.QuitGame.Size = new System.Drawing.Size(82, 21);
            this.QuitGame.TabIndex = 35;
            this.QuitGame.Text = "Quit Game";
            this.QuitGame.UseVisualStyleBackColor = true;
            this.QuitGame.Click += new System.EventHandler(this.QuitGame_Click);
            // 
            // TimeRemainingText
            // 
            this.TimeRemainingText.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeRemainingText.Location = new System.Drawing.Point(29, 263);
            this.TimeRemainingText.Margin = new System.Windows.Forms.Padding(2);
            this.TimeRemainingText.Name = "TimeRemainingText";
            this.TimeRemainingText.ReadOnly = true;
            this.TimeRemainingText.Size = new System.Drawing.Size(93, 52);
            this.TimeRemainingText.TabIndex = 33;
            this.TimeRemainingText.Text = "000";
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
            // 
            // wordsPlayedP2Txt
            // 
            this.wordsPlayedP2Txt.AcceptsReturn = true;
            this.wordsPlayedP2Txt.Location = new System.Drawing.Point(392, 63);
            this.wordsPlayedP2Txt.Margin = new System.Windows.Forms.Padding(2);
            this.wordsPlayedP2Txt.Multiline = true;
            this.wordsPlayedP2Txt.Name = "wordsPlayedP2Txt";
            this.wordsPlayedP2Txt.ReadOnly = true;
            this.wordsPlayedP2Txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.wordsPlayedP2Txt.Size = new System.Drawing.Size(134, 165);
            this.wordsPlayedP2Txt.TabIndex = 31;
            // 
            // SubmitWordText
            // 
            this.SubmitWordText.Location = new System.Drawing.Point(224, 271);
            this.SubmitWordText.Margin = new System.Windows.Forms.Padding(1);
            this.SubmitWordText.Name = "SubmitWordText";
            this.SubmitWordText.ReadOnly = true;
            this.SubmitWordText.Size = new System.Drawing.Size(150, 20);
            this.SubmitWordText.TabIndex = 30;
            // 
            // submitWordButton
            // 
            this.submitWordButton.Location = new System.Drawing.Point(133, 271);
            this.submitWordButton.Margin = new System.Windows.Forms.Padding(1);
            this.submitWordButton.Name = "submitWordButton";
            this.submitWordButton.Size = new System.Drawing.Size(81, 21);
            this.submitWordButton.TabIndex = 29;
            this.submitWordButton.Text = "Submit Word";
            this.submitWordButton.UseVisualStyleBackColor = true;
            this.submitWordButton.Click += new System.EventHandler(this.submitWordButton_Click);
            // 
            // letter16
            // 
            this.letter16.AccessibleName = "";
            this.letter16.BackColor = System.Drawing.Color.White;
            this.letter16.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter16.Location = new System.Drawing.Point(322, 214);
            this.letter16.Margin = new System.Windows.Forms.Padding(2);
            this.letter16.Multiline = true;
            this.letter16.Name = "letter16";
            this.letter16.ReadOnly = true;
            this.letter16.Size = new System.Drawing.Size(52, 54);
            this.letter16.TabIndex = 28;
            this.letter16.Text = "-";
            this.letter16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter16.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fourFourTxt_MouseDown);
            // 
            // letter15
            // 
            this.letter15.AccessibleName = "";
            this.letter15.BackColor = System.Drawing.Color.White;
            this.letter15.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter15.Location = new System.Drawing.Point(268, 214);
            this.letter15.Margin = new System.Windows.Forms.Padding(2);
            this.letter15.Multiline = true;
            this.letter15.Name = "letter15";
            this.letter15.ReadOnly = true;
            this.letter15.Size = new System.Drawing.Size(52, 54);
            this.letter15.TabIndex = 27;
            this.letter15.Text = "-";
            this.letter15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter15.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fourThreeTxt_MouseDown);
            // 
            // letter14
            // 
            this.letter14.AccessibleName = "";
            this.letter14.BackColor = System.Drawing.Color.White;
            this.letter14.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter14.Location = new System.Drawing.Point(216, 214);
            this.letter14.Margin = new System.Windows.Forms.Padding(2);
            this.letter14.Multiline = true;
            this.letter14.Name = "letter14";
            this.letter14.ReadOnly = true;
            this.letter14.Size = new System.Drawing.Size(52, 54);
            this.letter14.TabIndex = 26;
            this.letter14.Text = "-";
            this.letter14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter14.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fourTwoTxt_MouseDown);
            // 
            // letter13
            // 
            this.letter13.AccessibleName = "";
            this.letter13.BackColor = System.Drawing.Color.White;
            this.letter13.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter13.Location = new System.Drawing.Point(162, 214);
            this.letter13.Margin = new System.Windows.Forms.Padding(2);
            this.letter13.Multiline = true;
            this.letter13.Name = "letter13";
            this.letter13.ReadOnly = true;
            this.letter13.Size = new System.Drawing.Size(52, 54);
            this.letter13.TabIndex = 25;
            this.letter13.Text = "-";
            this.letter13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter13.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fourOneTxt_MouseDown);
            // 
            // letter12
            // 
            this.letter12.AccessibleName = "";
            this.letter12.BackColor = System.Drawing.Color.White;
            this.letter12.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter12.Location = new System.Drawing.Point(322, 158);
            this.letter12.Margin = new System.Windows.Forms.Padding(2);
            this.letter12.Multiline = true;
            this.letter12.Name = "letter12";
            this.letter12.ReadOnly = true;
            this.letter12.Size = new System.Drawing.Size(52, 54);
            this.letter12.TabIndex = 24;
            this.letter12.Text = "-";
            this.letter12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter12.MouseDown += new System.Windows.Forms.MouseEventHandler(this.threeFourTxt_MouseDown);
            // 
            // letter11
            // 
            this.letter11.AccessibleName = "";
            this.letter11.BackColor = System.Drawing.Color.White;
            this.letter11.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter11.Location = new System.Drawing.Point(268, 158);
            this.letter11.Margin = new System.Windows.Forms.Padding(2);
            this.letter11.Multiline = true;
            this.letter11.Name = "letter11";
            this.letter11.ReadOnly = true;
            this.letter11.Size = new System.Drawing.Size(52, 54);
            this.letter11.TabIndex = 23;
            this.letter11.Text = "-";
            this.letter11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter11.MouseDown += new System.Windows.Forms.MouseEventHandler(this.threeThreeTxt_MouseDown);
            // 
            // letter10
            // 
            this.letter10.AccessibleName = "";
            this.letter10.BackColor = System.Drawing.Color.White;
            this.letter10.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter10.Location = new System.Drawing.Point(216, 158);
            this.letter10.Margin = new System.Windows.Forms.Padding(2);
            this.letter10.Multiline = true;
            this.letter10.Name = "letter10";
            this.letter10.ReadOnly = true;
            this.letter10.Size = new System.Drawing.Size(52, 54);
            this.letter10.TabIndex = 22;
            this.letter10.Text = "-";
            this.letter10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.threeTwoTxt_MouseDown);
            // 
            // letter9
            // 
            this.letter9.AccessibleName = "";
            this.letter9.BackColor = System.Drawing.Color.White;
            this.letter9.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter9.Location = new System.Drawing.Point(162, 158);
            this.letter9.Margin = new System.Windows.Forms.Padding(2);
            this.letter9.Multiline = true;
            this.letter9.Name = "letter9";
            this.letter9.ReadOnly = true;
            this.letter9.Size = new System.Drawing.Size(52, 54);
            this.letter9.TabIndex = 3;
            this.letter9.Text = "-";
            this.letter9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.threeOneTxt_MouseDown);
            // 
            // letter8
            // 
            this.letter8.AccessibleName = "";
            this.letter8.BackColor = System.Drawing.Color.White;
            this.letter8.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter8.Location = new System.Drawing.Point(322, 103);
            this.letter8.Margin = new System.Windows.Forms.Padding(2);
            this.letter8.Multiline = true;
            this.letter8.Name = "letter8";
            this.letter8.ReadOnly = true;
            this.letter8.Size = new System.Drawing.Size(52, 54);
            this.letter8.TabIndex = 21;
            this.letter8.Text = "-";
            this.letter8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.twoFourTxt_MouseDown);
            // 
            // letter7
            // 
            this.letter7.AccessibleName = "";
            this.letter7.BackColor = System.Drawing.Color.White;
            this.letter7.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter7.Location = new System.Drawing.Point(268, 103);
            this.letter7.Margin = new System.Windows.Forms.Padding(2);
            this.letter7.Multiline = true;
            this.letter7.Name = "letter7";
            this.letter7.ReadOnly = true;
            this.letter7.Size = new System.Drawing.Size(52, 54);
            this.letter7.TabIndex = 20;
            this.letter7.Text = "-";
            this.letter7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.twoThreeTxt_MouseDown);
            // 
            // letter6
            // 
            this.letter6.AccessibleName = "";
            this.letter6.BackColor = System.Drawing.Color.White;
            this.letter6.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter6.Location = new System.Drawing.Point(216, 103);
            this.letter6.Margin = new System.Windows.Forms.Padding(2);
            this.letter6.Multiline = true;
            this.letter6.Name = "letter6";
            this.letter6.ReadOnly = true;
            this.letter6.Size = new System.Drawing.Size(52, 54);
            this.letter6.TabIndex = 19;
            this.letter6.Text = "-";
            this.letter6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.twoTwoTxt_MouseDown);
            // 
            // letter5
            // 
            this.letter5.AccessibleName = "";
            this.letter5.BackColor = System.Drawing.Color.White;
            this.letter5.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter5.Location = new System.Drawing.Point(162, 103);
            this.letter5.Margin = new System.Windows.Forms.Padding(2);
            this.letter5.Multiline = true;
            this.letter5.Name = "letter5";
            this.letter5.ReadOnly = true;
            this.letter5.Size = new System.Drawing.Size(52, 54);
            this.letter5.TabIndex = 18;
            this.letter5.Text = "-";
            this.letter5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.twoOneTxt_MouseDown);
            // 
            // letter4
            // 
            this.letter4.AccessibleName = "";
            this.letter4.BackColor = System.Drawing.Color.White;
            this.letter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter4.Location = new System.Drawing.Point(322, 48);
            this.letter4.Margin = new System.Windows.Forms.Padding(2);
            this.letter4.Multiline = true;
            this.letter4.Name = "letter4";
            this.letter4.ReadOnly = true;
            this.letter4.Size = new System.Drawing.Size(52, 54);
            this.letter4.TabIndex = 17;
            this.letter4.Text = "-";
            this.letter4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.oneFourTxt_MouseDown);
            // 
            // letter3
            // 
            this.letter3.AccessibleName = "";
            this.letter3.BackColor = System.Drawing.Color.White;
            this.letter3.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter3.Location = new System.Drawing.Point(268, 48);
            this.letter3.Margin = new System.Windows.Forms.Padding(2);
            this.letter3.Multiline = true;
            this.letter3.Name = "letter3";
            this.letter3.ReadOnly = true;
            this.letter3.Size = new System.Drawing.Size(52, 54);
            this.letter3.TabIndex = 3;
            this.letter3.Text = "-";
            this.letter3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.oneThreetxt_MouseDown);
            // 
            // letter2
            // 
            this.letter2.AccessibleName = "";
            this.letter2.BackColor = System.Drawing.Color.White;
            this.letter2.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter2.Location = new System.Drawing.Point(216, 48);
            this.letter2.Margin = new System.Windows.Forms.Padding(2);
            this.letter2.Multiline = true;
            this.letter2.Name = "letter2";
            this.letter2.ReadOnly = true;
            this.letter2.Size = new System.Drawing.Size(52, 54);
            this.letter2.TabIndex = 16;
            this.letter2.Text = "-";
            this.letter2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.oneTwoTxt_MouseDown);
            // 
            // letter1
            // 
            this.letter1.AccessibleName = "";
            this.letter1.BackColor = System.Drawing.Color.White;
            this.letter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter1.Location = new System.Drawing.Point(162, 48);
            this.letter1.Margin = new System.Windows.Forms.Padding(2);
            this.letter1.Multiline = true;
            this.letter1.Name = "letter1";
            this.letter1.ReadOnly = true;
            this.letter1.Size = new System.Drawing.Size(52, 54);
            this.letter1.TabIndex = 0;
            this.letter1.Text = "-";
            this.letter1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.letter1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.oneOneTxt_MouseDown);
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
            this.FindGameButton.Location = new System.Drawing.Point(27, 71);
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
            this.GameDurationTxt.Location = new System.Drawing.Point(18, 47);
            this.GameDurationTxt.Margin = new System.Windows.Forms.Padding(2);
            this.GameDurationTxt.Name = "GameDurationTxt";
            this.GameDurationTxt.Size = new System.Drawing.Size(96, 20);
            this.GameDurationTxt.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(30, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Enter Game";
            // 
            // helpMePlayBoggleToolStripMenuItem
            // 
            this.helpMePlayBoggleToolStripMenuItem.Name = "helpMePlayBoggleToolStripMenuItem";
            this.helpMePlayBoggleToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.helpMePlayBoggleToolStripMenuItem.Text = "Help Me Play Boggle!";
            this.helpMePlayBoggleToolStripMenuItem.Click += new System.EventHandler(this.helpMePlayBoggleToolStripMenuItem_Click);
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
        private System.Windows.Forms.TextBox letter1;
        private System.Windows.Forms.Label BoggleLabel;
        private System.Windows.Forms.TextBox letter16;
        private System.Windows.Forms.TextBox letter15;
        private System.Windows.Forms.TextBox letter14;
        private System.Windows.Forms.TextBox letter13;
        private System.Windows.Forms.TextBox letter12;
        private System.Windows.Forms.TextBox letter11;
        private System.Windows.Forms.TextBox letter10;
        private System.Windows.Forms.TextBox letter9;
        private System.Windows.Forms.TextBox letter8;
        private System.Windows.Forms.TextBox letter7;
        private System.Windows.Forms.TextBox letter6;
        private System.Windows.Forms.TextBox letter5;
        private System.Windows.Forms.TextBox letter4;
        private System.Windows.Forms.TextBox letter3;
        private System.Windows.Forms.TextBox letter2;
        private System.Windows.Forms.Button submitWordButton;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.RichTextBox TimeRemainingText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox wordsPlayedP2Txt;
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
        private System.Windows.Forms.TextBox wordsPlayedP1Txt;
        private System.Windows.Forms.Button FindGameButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox GameDurationTxt;
        private System.Windows.Forms.RichTextBox GameActiveBox;
        private System.Windows.Forms.RichTextBox GamePendingBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label GameJoinedLabel;
        private System.Windows.Forms.RichTextBox GameCompleteBox;
        private System.Windows.Forms.Button CancelFindGame;
        private System.Windows.Forms.ToolStripMenuItem helpMePlayBoggleToolStripMenuItem;
    }
}

