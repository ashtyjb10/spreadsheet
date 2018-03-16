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
        public string NickName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string baseAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string wordToSubmit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string timeRemaining { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string score { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string board { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string statsBoard { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        private void oneOneTxt_MouseDown(object sender, MouseEventArgs e)
        {
            this.oneOneTxt.BackColor = Color.Red;
          
        }

        private void oneTwoTxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.oneTwoTxt.BackColor == Color.White)
            {
                this.oneTwoTxt.BackColor = Color.Red;

            }
        }

        private void oneThreetxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.oneThreeTxt.BackColor == Color.White)
            {
                this.oneThreeTxt.BackColor = Color.Red;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void EnableControls(bool enabled)
        {
            //throw new NotImplementedException();
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


    }
}
