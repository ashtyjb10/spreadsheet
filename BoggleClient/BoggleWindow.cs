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
        
        /// <summary>
        /// Private global variables
        /// </summary>
        private int lastSelected = 0;
        private Timer updateTimer = new Timer();
        private TextBox[] boxArray;
        private int gameFirstActive = 0;

        /// <summary>
        /// Constructor that sets up the timer and each of the letter boxes to be updated.
        /// </summary>
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

        /// <summary>
        /// Prompts the timer ticking in the controller.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTick(object sender, EventArgs e)
        {
            TickingTimer();
        }

        /// <summary>
        /// Actions to be triggered.
        /// </summary>
        public event Action<string, string> RegisterUser;
        public event Action<string> DesiredGameDuration;
        public event Action<string> ScoreWord;
        public event Action TickingTimer;
        public event Action CancelJoinGame;
        public event Action QuitGameClicked;

        
        public void echo()
        {
            this.letter1.AppendText("test");
        }

        /// <summary>
        /// Registers a user with the server provided there is correct information.  If the information is
        /// incorrect a proper message is displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            
        }

        public void RegistrationCanceled()
        {
            CancelButton.Enabled = false;
            UsernameText.Text = "";
        }

        /// <summary>
        /// Promprs the registration of the user in the controller.
        /// </summary>
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

        /// <summary>
        /// Prompts the joinning of a game.
        /// </summary>
        public void GameJoined()
        {
            EnterGamePanel.Enabled = false;
            GameBoard.Enabled = true;
            GameJoinedLabel.Visible = true;
            //GameCompleteBox.Visible = true;
            //GamePendingBox.Visible = true;
            //GameActiveBox.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visible"></param>
        public void ViewPendingBox(bool visible)
        {
            GamePendingBox.Visible = visible;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            CancelRegisterButtonPressed();
        }
        public event Action CancelRegisterButtonPressed;
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
                ResetLetterBoard();
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
        private void ResetLetterBoard()
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

        /// <summary>
        /// Sets the usernames of each player in the game.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public void setUserNames(string player1, string player2)
        {
            PlayerOneName.Text = player1;
            PlayerTwoName.Text = player2;
        }

        /// <summary>
        /// Sets the scores of each player in the game.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public void setScores(string player1, string player2)
        {
            PlayerOneScoreBox.Text = player1;
            PlayerTwoScoreBox.Text = player2;
        }

        /// <summary>
        /// Sets the time remaining in the game.
        /// </summary>
        /// <param name="timeRemaining"></param>
        public void setTime(string timeRemaining)
        {
            TimeRemainingText.Text = timeRemaining;
        }

        /// <summary>
        /// Sumbits a word to the server and resets the letter board for new input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitWordButton_Click(object sender, EventArgs e)
        {
            //only if game is active.
            ScoreWord(SubmitWordText.Text);
            ResetLetterBoard();
        }

        /// <summary>
        /// Sets the active box visible.  If this is the first itteration of an active baord, the 
        /// letter board is cleared.
        /// </summary>
        /// <param name="visible"></param>
        public void ViewActiveBox(bool visible)
        {
            GameActiveBox.Visible = visible;
            if(gameFirstActive <= 0)
            {
                ResetLetterBoard();
            }
        }

        /// <summary>
        /// Shows the completed game box.  Sets the windows of the words that palyers played to be enabled
        /// to scroll through, stops the timer from prompting the server for updates and updating the board.
        /// </summary>
        /// <param name="visible"></param>
        public void ViewCompletedBox(bool visible)
        {
            GameCompleteBox.Visible = visible;
            // what to do when game is completed??? we still wanna have access to the words played right?
            GameBoard.Enabled = false;
            wordsPlayedP1Txt.Enabled = true;
            wordsPlayedP2Txt.Enabled = true;
            EnterGamePanel.Enabled = true;
            updateTimer.Enabled = false;
            ResetLetterBoard();
        }

        /// <summary>
        /// Sets the player 1 words played at the end of the game.
        /// </summary>
        /// <param name="wordsPlayed"></param>
        public void setPlayer1WordsPlayed(HashSet<string> wordsPlayed)
        {
            wordsPlayedP1Txt.Text = "";

            foreach(string wordScore in wordsPlayed)
            {
                wordsPlayedP1Txt.Text = wordsPlayedP1Txt.Text + wordScore + Environment.NewLine;
            }
        }

        /// <summary>
        /// Sets the player two words played at the end of the game.
        /// </summary>
        /// <param name="wordsPlayed"></param>
        public void setPlayer2WordsPlayed(HashSet<string> wordsPlayed)
        {
            wordsPlayedP2Txt.Text = "";

            foreach (string wordScore in wordsPlayed)
            {
                wordsPlayedP2Txt.Text = wordsPlayedP2Txt.Text + wordScore + Environment.NewLine;
            }
        }

        /// <summary>
        /// Leaves a pending game before it begins.  Resets the letter board.  Time is disabled to stop prompting
        /// the server.
        /// </summary>
        public void JoinGameCanceled()
        {
            GamePendingBox.Visible = false;
            GameBoard.Enabled = false;
            EnterGamePanel.Enabled = true;
            updateTimer.Enabled = false;
            ResetBoard();
        }
        /// <summary>
        /// Handles the invalid user token.
        /// </summary>
        public void InvalidUserToken()
        {
            EnterGamePanel.Enabled = false;
            RegistrationPanel.Enabled = true;
            UsernameText.Text = "";
        }
        /// <summary>
        /// Handles the game invalid token
        /// </summary>
        public void GameIdInvalid()
        {
            RegistrationPanel.Enabled = true;
            GameDurationTxt.Text = "";
            EnterGamePanel.Enabled = false;
            GameBoard.Enabled = false;
        }
        
        /// <summary>
        /// Quits an active game before the game is completed.  Resets the entire game board to default settings.
        /// shows a message that the game has been quit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitGame_Click(object sender, EventArgs e)
        {
            GameBoard.Enabled = false;
            EnterGamePanel.Enabled = false;
            RegistrationPanel.Enabled = true;
            updateTimer.Enabled = false;
            ResetBoard();
            MessageBox.Show("Game has been quit!", "Quit Game", MessageBoxButtons.OK);
        }

        
        /// <summary>
        /// Resets the entire game board back to defaul settings.
        /// </summary>
        private void ResetBoard()
        {
            PlayerOneName.Text = "Player1";
            PlayerTwoName.Text = "Player2";
            PlayerOneScoreBox.Text = "0";
            PlayerTwoScoreBox.Text = "0";
            TimeRemainingText.Text = "000";
            GamePendingBox.Visible = false;
            GameActiveBox.Visible = false;
            GameCompleteBox.Visible = false;
            GameJoinedLabel.Visible = false;
            ResetLetterBoard();
            QuitGameClicked();

        }

        /// <summary>
        /// Prompts the cancel action handler in the controller to handle the cancel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelFindGame_Click_1(object sender, EventArgs e)
        {
            CancelJoinGame();
        }

        /// <summary>
        /// Help window used to teach the user how to use the client and play boggle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpMePlayBoggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To use this Boggle Client you must first provide the domain name of a Boggle Server!" + "\r" +
                "If you dont have one, try this!  http://ice.eng.utah.edu" + "\r" + "When you have been registered with the server " +
                "you must enter the desired length of the game, between 5 seconds and 120 seconds." + "\r" + "You will be thrown into the game, but dont panic! " +
                "Use the board to connect any words you can see, either above, to the sides or corners of each tile.  If you make a mistake, simply click on a selected tile" +
                "and the current word will be erased.  When you have a word, click submit word and you will be given points according to the word you gave!" + "\r" +
                "Watch the time and get as many words as you can!  If you have to quit early, use the quit button.  If you have a problem connecting or registering, use the cancel buttons " +
                "to quit the process." +"\r" + "Happy Boggle-ing!", "Teach Me How To Boggle! " + MessageBoxButtons.OK);
        }
    }
}
