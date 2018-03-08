using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public partial class SSWindow : Form, IAnalysisView
    {

        public bool isChanged => throw new NotImplementedException();
        

        
        string IAnalysisView.Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SSWindow()
        {
            InitializeComponent();
        }

        public event Action<string> NewFileChosen;
        public event Action CloseEvent;
        public event Action<string> GetCellInfo;
        public event Action<string> ContentsChanged;
        public event Action Save;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void closeMenu_Click(object sender, EventArgs e)
        {
            if (CloseEvent != null)
            {
                CloseEvent();
            }

        }

        /// <summary>
        /// Closes this window
        /// </summary>
        public void DoClose()
        {
            Close();
        }

        private void NewSpreadsheet_Click(object sender, EventArgs e)
        {
            SpreadsheetApplicationContext.GetContext().RunNew();
        }

        private void OpenFromSpreadsheet_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            openFileDialog1.DefaultExt = "ss";
            openFileDialog1.Filter = "Text Files (*.txt)";
            openFileDialog1.FilterIndex = 2;

            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                if (NewFileChosen != null)
                {
                    NewFileChosen(openFileDialog1.FileName);
                }
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Title.Equals(""))
            {

            }

            Save();
        }

        

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                if (NewFileChosen != null)
                {
                    NewFileChosen(saveFileDialog1.FileName);
                }
            }
        }

        public string Title
        {
            set { Text = value; }
            get { return Text; }
        }

    }
}
