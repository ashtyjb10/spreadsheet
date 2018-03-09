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
        private int currentRow;
        private int currentCol;
        public SSWindow()
        {
            InitializeComponent();
        }


        public event Action<string> NewFileChosen;
        public event Action<string> GetCellInfo;
        public event Action<string> ContentsChanged;
        public event Action SelectionChanged;
        public event Action<int> ColChanged;
        public event Action<int> RowChanged;
        public event Action Save;
        public event Action CloseEvent;

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
            //Sets the default extension to *.ss when opening a file.
            openFileDialog1.Filter = "All|*.*|Spreadsheet|*.ss";
            openFileDialog1.DefaultExt = "Text|*.ss";
            openFileDialog1.FilterIndex = 2;
            DialogResult result = openFileDialog1.ShowDialog();

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

        public string Content { set => contentsBox.Text = value; }
        public string Value { set => valueBox.Text = value; }

        private void spreadsheetPanel_Load(object sender, EventArgs e)
        {
        }

        private void contentsBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender is TextBox)
                {
                    TextBox txb = (TextBox)sender;
                    RowChanged(currentRow);
                    ColChanged(currentCol);
                    ContentsChanged(txb.Text);
                    MessageBox.Show(txb.Text);
                }
            }


        }

        private void spreadsheetPanel_SelectionChanged(SSGui.SpreadsheetPanel sender)
        {
            //get name, get value, get Contents
            sender.GetSelection(out int col, out int row);
            currentCol = col;
            currentRow = row;
            
            //pass col, row , and value into the controler.

            //change the row and the column and set the new name
            RowChanged(row);
            ColChanged(col);

            //get new cell value and contents
            SelectionChanged();
            sender.SetValue(col, row, valueBox.Text);
        }
        public void CellNameText(String cName)
        {
            cellBox.Text = cName;
        }
        public void ValueBox(object value)
        {
            valueBox.Text = value.ToString();

        }
        public void ContentsBox(object contents)
        {
            contentsBox.Text = contents.ToString();
        }
        public void UpdatedValue(int col, int row, object value)
        {
            spreadsheetPanel.SetValue(col, row, value.ToString());
        }
    }
}
