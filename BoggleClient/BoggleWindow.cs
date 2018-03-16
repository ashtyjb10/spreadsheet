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
            this.oneOneTxt.AppendText("test");
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
            if (oneOneTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 2 || lastSelected == 5 || lastSelected == 6)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + oneOneTxt.Text;
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
            if (oneTwoTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 1 || lastSelected == 3 || lastSelected == 5 ||
                    lastSelected == 6 || lastSelected == 7 )
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + oneTwoTxt.Text;
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
            if (oneThreeTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 2 || lastSelected == 4 || lastSelected == 6 ||
                    lastSelected == 7 || lastSelected == 8)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + oneThreeTxt.Text;
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
            if (oneFourTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 3 || lastSelected == 7 || lastSelected == 8)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + oneFourTxt.Text;
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
            if (twoOneTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 1 || lastSelected == 2 || lastSelected == 6 ||
                    lastSelected == 9 || lastSelected == 10)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + twoOneTxt.Text;
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
            if (twoTwoTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 1 || lastSelected == 2 || lastSelected == 3 || 
                    lastSelected == 5 || lastSelected == 7 || lastSelected == 9 || lastSelected ==  10 || lastSelected == 11)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + twoTwoTxt.Text;
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
            if (twoThreeTxt.BackColor == Color.Red)
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
                    SubmitWordText.Text = SubmitWordText.Text + twoThreeTxt.Text;
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

            if (twoFourTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 3 || lastSelected == 4 || lastSelected == 7 ||
                    lastSelected == 11 || lastSelected == 12)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + twoFourTxt.Text;
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
            if (threeOneTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 5 || lastSelected == 6 || lastSelected == 10 ||
                    lastSelected == 13 || lastSelected == 14)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + threeOneTxt.Text;
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
            if (threeTwoTxt.BackColor == Color.Red)
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
                    SubmitWordText.Text = SubmitWordText.Text + threeTwoTxt.Text;
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
            if (threeThreeTxt.BackColor == Color.Red)
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
                    SubmitWordText.Text = SubmitWordText.Text + threeThreeTxt.Text;
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
            if (threeFourTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 7 || lastSelected == 8 || lastSelected == 11 ||
                    lastSelected ==  15 || lastSelected == 16)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + threeFourTxt.Text;
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
            if (fourOneTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 9 || lastSelected == 10 || lastSelected == 14)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + fourOneTxt.Text;
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
            if (fourTwoTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 9 || lastSelected == 10 || lastSelected == 11 ||
                   lastSelected == 13 || lastSelected == 15)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + fourTwoTxt.Text;
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
            if (fourThreeTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 10 || lastSelected == 11 || lastSelected == 12 || 
                    lastSelected == 14 || lastSelected == 16)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + fourThreeTxt.Text;
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
            if (fourFourTxt.BackColor == Color.Red)
            {
                ResetBoard();
            }
            else
            {
                if (lastSelected == 0 || lastSelected == 11 || lastSelected == 12 || lastSelected == 15)
                {
                    ChangeColorSelected((TextBox)sender);
                    SubmitWordText.Text = SubmitWordText.Text + fourFourTxt.Text;
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
            oneOneTxt.BackColor = Color.White;
            oneTwoTxt.BackColor = Color.White;
            oneThreeTxt.BackColor = Color.White;
            oneFourTxt.BackColor = Color.White;
            twoOneTxt.BackColor = Color.White;
            twoTwoTxt.BackColor = Color.White;
            twoThreeTxt.BackColor = Color.White;
            twoFourTxt.BackColor = Color.White;
            threeOneTxt.BackColor = Color.White;
            threeTwoTxt.BackColor = Color.White;
            threeThreeTxt.BackColor = Color.White;
            threeFourTxt.BackColor = Color.White;
            fourOneTxt.BackColor = Color.White;
            fourTwoTxt.BackColor = Color.White;
            fourThreeTxt.BackColor = Color.White;
            fourFourTxt.BackColor = Color.White;
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
    }
}
