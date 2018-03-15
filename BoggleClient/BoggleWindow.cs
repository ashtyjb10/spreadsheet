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

        public void echo()
        {
            this.oneOneTxt.AppendText("test");
        }

        private void oneOneTxt_MouseDown(object sender, MouseEventArgs e)
        {
            this.oneOneTxt.BackColor = Color.Red;
            Controller s = new Controller(this);
            s.Register("hey", "yes");
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
            throw new NotImplementedException();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
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

        private void CancelButton_Click(object sender, EventArgs e)
        {

        }
    }
}
