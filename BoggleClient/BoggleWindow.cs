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
        public string NickName { get => NickName; set => NickName = value; }
        public string baseAddress { get => baseAddress; set => baseAddress = value; }
        public string wordToSubmit { get => wordToSubmit; set => wordToSubmit = value; }
        public string timeRemaining { get => timeRemaining; set => timeRemaining = value; }
        public string score { get => score; set => score = value; }
        public string board { get => board; set => board = value; }
        public string statsBoard { get => statsBoard; set => statsBoard = value; }

        private int lastSelected = 0;

        public BoggleWindow()
        {
            InitializeComponent();
        }

        public event Action<string, string> RegisterUser;
        public event Action<string> DesiredGameDuration;

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
            }
            // GameDuraton = 
        }

        /// <summary>
        /// currently registered?
        /// </summary>
        public bool IsRegisteredUser { get; set; }
        public string GameDuraton { get; set; }

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
            letter1.BackColor = Color.White;
            letter2.BackColor = Color.White;
            letter3.BackColor = Color.White;
            letter4.BackColor = Color.White;
            letter5.BackColor = Color.White;
            letter6.BackColor = Color.White;
            letter7.BackColor = Color.White;
            letter8.BackColor = Color.White;
            letter9.BackColor = Color.White;
            letter10.BackColor = Color.White;
            letter11.BackColor = Color.White;
            letter12.BackColor = Color.White;
            letter13.BackColor = Color.White;
            letter14.BackColor = Color.White;
            letter15.BackColor = Color.White;
            letter16.BackColor = Color.White;
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
        public void setBoard(char[] boardArray)
        {
            if (boardArray[0].ToString().ToUpper() == "Q")
            {
                letter1.Text = "QU";
            }
            else
            {
                letter1.Text = boardArray[0].ToString();
            }

            if (boardArray[1].ToString().ToUpper() == "Q")
            {
                letter2.Text = "QU";
            }
            else
            {
                letter2.Text = boardArray[1].ToString();
            }

            if (boardArray[2].ToString().ToUpper() == "Q")
            {
                letter3.Text = "QU";
            }
            else
            {
                letter3.Text = boardArray[2].ToString();
            }

            if (boardArray[3].ToString().ToUpper() == "Q")
            {
                letter4.Text = "QU";
            }
            else
            {
                letter4.Text = boardArray[3].ToString();
            }

            if (boardArray[4].ToString().ToUpper() == "Q")
            {
                letter5.Text = "QU";
            }
            else
            {
                letter5.Text = boardArray[4].ToString();
            }

            if (boardArray[5].ToString().ToUpper() == "Q")
            {
                letter6.Text = "QU";
            }
            else
            {
                letter6.Text = boardArray[5].ToString();
            }

            if (boardArray[6].ToString().ToUpper() == "Q")
            {
                letter7.Text = "QU";
            }
            else
            {
                letter7.Text = boardArray[6].ToString();
            }

            if (boardArray[7].ToString().ToUpper() == "Q")
            {
                letter8.Text = "QU";
            }
            else
            {
                letter8.Text = boardArray[7].ToString();
            }

            if (boardArray[8].ToString().ToUpper() == "Q")
            {
                letter9.Text = "QU";
            }
            else
            {
                letter9.Text = boardArray[8].ToString();
            }

            if (boardArray[9].ToString().ToUpper() == "Q")
            {
                letter10.Text = "QU";
            }
            else
            {
                letter10.Text = boardArray[9].ToString();
            }

            if (boardArray[10].ToString().ToUpper() == "Q")
            {
                letter11.Text = "QU";
            }
            else
            {
                letter11.Text = boardArray[10].ToString();
            }

            if (boardArray[11].ToString().ToUpper() == "Q")
            {
                letter12.Text = "QU";
            }
            else
            {
                letter12.Text = boardArray[11].ToString();
            }

            if (boardArray[12].ToString().ToUpper() == "Q")
            {
                letter13.Text = "QU";
            }
            else
            {
                letter13.Text = boardArray[12].ToString();
            }

            if (boardArray[13].ToString().ToUpper() == "Q")
            {
                letter14.Text = "QU";
            }
            else
            {
                letter14.Text = boardArray[13].ToString();
            }

            if (boardArray[14].ToString().ToUpper() == "Q")
            {
                letter15.Text = "QU";
            }
            else
            {
                letter15.Text = boardArray[14].ToString();
            }

            if (boardArray[15].ToString().ToUpper() == "Q")
            {
                letter16.Text = "QU";
            }
            else
            {
                letter16.Text = boardArray[15].ToString();
            }
        }
    }
}
