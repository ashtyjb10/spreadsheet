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
    public partial class BoggleWindow : Form
    {
        public BoggleWindow()
        {
            InitializeComponent();
        }

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

    }
}
