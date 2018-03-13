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
            this.oneOneTxt.Text = "TEst1";

        }
    }
}
