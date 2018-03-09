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

        public SSWindow()
        {
            InitializeComponent();
        }

        public event Action<string> NewFileChosen;
        public event Action<string> GetCellInfo;
        public event Action<string> ContentsChanged;
        public event Action<string> SelectionChanged;
        public event Action<int> ColChanged;
        public event Action<int> RowChanged;
        public event Action CloseEvent;
        public event Action<string> SaveFileChosen;

        private void label1_Click(object sender, EventArgs e)
        {
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Closing events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeMenu_Click(object sender, EventArgs e)
        {
            if (CloseEvent != null)
            {
                CloseEvent();
            }

        }

        /// <summary>
        /// Closes this window.
        /// </summary>
        public void DoClose()
        {
            Close();
        }

        /// <summary>
        /// Creates a new window for an empy spreadsheet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewSpreadsheet_Click(object sender, EventArgs e)
        {
            SpreadsheetApplicationContext.GetContext().RunNew();
        }

        /// <summary>
        /// Loads an existing spreadsheet in a new window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Saves the spreadsheet, if there is no name then the save is redirected to save as.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //If there is no name for the spreadsheet.
            if (Title.Equals(""))
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                SaveFileChosen(Title);
            }
        }

        /// <summary>
        /// Save as requires a name as an input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "All| *.*|Spreadsheet|*.ss";
            saveFileDialog1.FilterIndex = 2;
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                if (SaveFileChosen != null)
                {
                    SaveFileChosen(saveFileDialog1.FileName);
                }
            }
        }

        /// <summary>
        /// Getter and setter for the title.
        /// </summary>
        public string Title
        {
            set { Text = value; }
            get { return Text; }
        }

        /// <summary>
        /// Content setter
        /// </summary>
        public string Content { set => contentsBox.Text = value; }
       
        /// <summary>
        ///Setter for Value 
        /// </summary>
        public string Value { set => valueBox.Text = value; }

        /// <summary>
        /// Setter for the Cell
        /// </summary>
        public string Cell { set => cellBox.Text = value; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contentsBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender is TextBox)
                {
                    TextBox txb = (TextBox)sender;
                    Console.WriteLine(txb.Text);
                    ContentsChanged(txb.Text);
                    MessageBox.Show(txb.Text);
                }
            }
        }

        private void spreadsheetPanel_SelectionChanged(SSGui.SpreadsheetPanel sender)
        {
            //get name, get value, get Contents
            sender.GetSelection(out int col, out int row);
            
            sender.GetValue(col, row, out string value);
            if (value == "")
            {

            }

            //pass col, row , and value into the controler.

            //change the row and the column and set the new name
            RowChanged(row);
            ColChanged(col);

            

            ValueBox(value);

        }

        /// <summary>
        /// Setter for the cell name box
        /// </summary>
        /// <param name="cName"></param>
        public void CellNameText(String cName)
        {
            cellBox.Text = cName;
        }

        /// <summary>
        /// Cetter for the Value Box
        /// </summary>
        /// <param name="value"></param>
        public void ValueBox(string value)
        {
            valueBox.Text = value;
        }

        /// <summary>
        /// setter for the contents box.
        /// </summary>
        /// <param name="contents"></param>
        public void ContentsBox(string contents)
        {
            contentsBox.Text = contents;
        }

        private void SpreadsheetPanel_Load(object sender, EventArgs e)
        {
        }
    }
}
