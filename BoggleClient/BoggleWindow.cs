using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoggleClient
{
    public partial class BoggleWindow : Form, IAnalysisView
    {
        

        private int lastSelected = 0;
        private Timer updateTimer = new Timer();
        private TextBox[] boxArray;
        public BoggleWindow()
        {
            InitializeComponent();

            boxArray = new TextBox[] {letter1, letter2, letter3, letter4, letter5, letter6, letter7,
                letter8, letter9, letter10, letter11, letter12, letter13, letter14, letter15, letter16};

            //Update timer.
            updateTimer.Interval = 1000;
            updateTimer.Tick += new EventHandler(timerTick);
            
            
            //Check status timer.
            Timer checkStatusTimer = new Timer();
            checkStatusTimer.Interval = 1000;
            checkStatusTimer.Enabled = true;
        }

        private void timerTick(object sender, EventArgs e)
        {
            tickingTimer();
        }

        public event Action<string, string> RegisterUser;
        public event Action<string> DesiredGameDuration;
        public event Action<string> ScoreWord;
        public event Action tickingTimer;

        public void echo()
        {
            this.letter1.AppendText("test");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void EnableControls(bool enabled)
        {
           
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            IsRegisteredUser = false;
            CancelButton.Enabled = true;

            if (DomainText.Text == "" || UsernameText.Text == "")
            {
                MessageBox.Show("The domain or username is empty.  Please provide complete information.", "Registration Error", MessageBoxButtons.OK);
            }
            else
            {
                RegisterUser(DomainText.Text, UsernameText.Text);
            }

            CancelButton.Enabled = false;
        }

        public void RegisteredUser()
        {
            if (IsRegisteredUser)
            {
                RegistrationPanel.Enabled = false;
                EnterGamePanel.Enabled = true;
                //GameBoard.Enabled = true;
            }
            else
            {
                Console.WriteLine("didnt work");
            }

        }

        public void GameJoined()
        {
            EnterGamePanel.Enabled = false;
            GameBoard.Enabled = true;
            GameJoinedLabel.Visible = true;
            //GameCompleteBox.Visible = true;
            //GamePendingBox.Visible = true;
            //GameActiveBox.Visible = true;
        }

        public void ViewPendingBox(bool visible)
        {
            GamePendingBox.Visible = visible;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
        }

        private void FindGameButton_Click(object sender, EventArgs e)
        {
            if (GameDurationTxt.Text == "")
            {
                MessageBox.Show("The game duration is empty.  Please provide complete information.",
                    "Time Duration Error", MessageBoxButtons.OK);
            }
            else
            {
                DesiredGameDuration(GameDurationTxt.Text);
                updateTimer.Enabled = true;
                updateTimer.Start();
                GameCompleteBox.Visible = false;
                PlayerOneScoreBox.Text = "";
                PlayerTwoScoreBox.Text = "";
            }
            // GameDuraton = 
        }

        /// <summary>
        /// currently registered?
        /// </summary>
        public bool IsRegisteredUser { get; set; }
        public string GameDuraton { get; set; }


        public bool timerEnabled { get => updateTimer.Enabled; set => updateTimer.Enabled = value; }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oneOneTxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (letter1.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 2 || lastSelected == 5 || lastSelected == 6)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter1.Text;
                    lastSelected = 1;
                }
            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oneTwoTxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (letter2.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 1 || lastSelected == 3 || lastSelected == 5 ||
                    lastSelected == 6 || lastSelected == 7 )
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter2.Text;
                    lastSelected = 2;
                }

            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oneThreetxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (letter3.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 2 || lastSelected == 4 || lastSelected == 6 ||
                    lastSelected == 7 || lastSelected == 8)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter3.Text;
                    lastSelected = 3;
                }
            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oneFourTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter4.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 3 || lastSelected == 7 || lastSelected == 8)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter4.Text;
                    lastSelected = 4;
                }
            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void twoOneTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter5.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 1 || lastSelected == 2 || lastSelected == 6 ||
                    lastSelected == 9 || lastSelected == 10)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter5.Text;
                    lastSelected = 5;
                }
            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void twoTwoTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter6.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 1 || lastSelected == 2 || lastSelected == 3 || 
                    lastSelected == 5 || lastSelected == 7 || lastSelected == 9 || lastSelected ==  10 || lastSelected == 11)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter6.Text;
                    lastSelected = 6;
                }
            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void twoThreeTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter7.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 2 || lastSelected == 3 || lastSelected == 4 ||
                    lastSelected == 6 || lastSelected == 8 || lastSelected == 10 || lastSelected == 11 ||
                    lastSelected == 12)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter7.Text;
                    lastSelected = 7;
                }
            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void twoFourTxt_MouseDown(object sender, EventArgs e)
        {

            if (letter8.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 3 || lastSelected == 4 || lastSelected == 7 ||
                    lastSelected == 11 || lastSelected == 12)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter8.Text;
                    lastSelected = 8;
                }
            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threeOneTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter9.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 5 || lastSelected == 6 || lastSelected == 10 ||
                    lastSelected == 13 || lastSelected == 14)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter9.Text;
                    lastSelected = 9;
                }
            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threeTwoTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter10.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 5 || lastSelected ==  6 || lastSelected == 7 ||
                    lastSelected == 9 || lastSelected == 11 || lastSelected == 13 || lastSelected == 14 ||
                    lastSelected == 15)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter10.Text;
                    lastSelected = 10;
                }
            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threeThreeTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter11.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 6 || lastSelected == 7 || lastSelected == 8 ||
                    lastSelected == 10 || lastSelected == 12 || lastSelected == 14 || lastSelected == 15 ||
                    lastSelected == 16)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter11.Text;
                    lastSelected = 11;
                }

            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threeFourTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter12.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 7 || lastSelected == 8 || lastSelected == 11 ||
                    lastSelected ==  15 || lastSelected == 16)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter12.Text;
                    lastSelected = 12;
                }

            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fourOneTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter13.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 9 || lastSelected == 10 || lastSelected == 14)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter13.Text;
                    lastSelected = 13;
                }

            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fourTwoTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter14.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 9 || lastSelected == 10 || lastSelected == 11 ||
                   lastSelected == 13 || lastSelected == 15)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter14.Text;
                    lastSelected = 14;
                }
                
            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fourThreeTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter15.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 10 || lastSelected == 11 || lastSelected == 12 || 
                    lastSelected == 14 || lastSelected == 16)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter15.Text;
                    lastSelected = 15;
                }

            }
        }

        /// <summary>
        /// When the box is clicked, visibily shows that box has been selected and makes the text
        /// from the box visible in the submit window.  If the box has already been selected, removes all selections and
        /// entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fourFourTxt_MouseDown(object sender, EventArgs e)
        {
            if (letter16.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 11 || lastSelected == 12 || lastSelected == 15)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + letter16.Text;
                    lastSelected = 16;
                }

            }
        }

        /// <summary>
        /// Clears the board of all selected letters.  Resets the submit word box and
        /// sets the last selected box to 0.
        /// </summary>
        private void ResetBoard()
        {
            foreach(TextBox box in this.boxArray)
            {
                box.BackColor = Color.White;
            }
            SubmitWordText.Text = "";
            lastSelected = 0;

        }

        /// <summary>
        /// Changes the color of the background of the box to red.
        /// </summary>
        /// <param name="sender"></param>
        private void ChangeColorSelected(TextBox sender)
        {
            sender.BackColor = Color.Red;
        }

        /// <summary>
        /// Sets the board to each letter in the board.  If the letter is Q, the letter is set to "QU"
        /// </summary>
        /// <param name="boardArray"></param>
        public void SetBoard(char[] boardArray)
        {
            int iterator = 0;
            
            foreach(char setTo in boardArray)
            {
                if(setTo.ToString().ToUpper() == "Q")
                {
                    boxArray[iterator].Text = "QU";
                }
                else
                {
                    boxArray[iterator].Text = setTo.ToString().ToUpper();
                }
                iterator++;
                
            }
        }

        public void setUserNames(string player1, string player2)
        {
            PlayerOneName.Text = player1;
            PlayerTwoName.Text = player2;
        }

        public void setScores(string player1, string player2)
        {
            PlayerOneScoreBox.Text = player1;
            PlayerTwoScoreBox.Text = player2;
        }

        public void setTime(string timeRemaining)
        {
            TimeRemainingText.Text = timeRemaining;
        }

        private void submitWordButton_Click(object sender, EventArgs e)
        {
            //only if game is active.
            ScoreWord(SubmitWordText.Text);
            ResetBoard();
        }
        public void ViewActiveBox(bool visible)
        {
            GameActiveBox.Visible = visible;
        }
        public void ViewCompletedBox(bool visible)
        {
            GameCompleteBox.Visible = visible;
            // what to do when game is completed??? we still wanna have access to the words played right?
            GameBoard.Enabled = false;
            wordsPlayedP1Txt.Enabled = true;
            wordsPlayedP2Txt.Enabled = true;
            EnterGamePanel.Enabled = true;
            updateTimer.Enabled = false;
            ResetBoard();
            
           

        }

        public void setPlayer1WordsPlayed(HashSet<string> wordsPlayed)
        {
            wordsPlayedP1Txt.Text = "";

            foreach(string wordScore in wordsPlayed)
            {
                wordsPlayedP1Txt.Text = wordsPlayedP1Txt.Text + wordScore + Environment.NewLine;
            }
        }

        public void setPlayer2WordsPlayed(HashSet<string> wordsPlayed)
        {
            wordsPlayedP2Txt.Text = "";

            foreach (string wordScore in wordsPlayed)
            {
                wordsPlayedP2Txt.Text = wordsPlayedP2Txt.Text + wordScore + Environment.NewLine;
            }
        }

        private void CancelFindGame_Click(object sender, EventArgs e)
        {
            CancelJoinGame();
        }
        public void JoinGameCanceled()
        {
            GamePendingBox.Visible = false;
            GameBoard.Enabled = false;
            EnterGamePanel.Enabled = true;
        }

        public event Action CancelJoinGame;
    }
}
