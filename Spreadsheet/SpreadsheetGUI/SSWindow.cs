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
            cellBox.Text = "A1";
        }
        
        public event Action<string> NewFileChosen;
        public event Action<string> ContentsChanged;
        public event Action SelectionChanged;
        public event Action<int> ColChanged;
        public event Action<int> RowChanged;
        public event Action<FormClosingEventArgs> CloseEvent;
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
                Close();
            }

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            //If the window needs to close for another reason then close;
            if (e.CloseReason == CloseReason.WindowsShutDown
                || e.CloseReason == CloseReason.TaskManagerClosing
                || e.CloseReason == CloseReason.ApplicationExitCall)
            {
                return;
            }
            // Initiate closing.
            CloseEvent(e);
        }


        /// <summary>
        /// Shows a dialog in the case that there is unsaved data.
        /// </summary>
        public void QuitWarning(FormClosingEventArgs e)
        {
            //Show dialog
           DialogResult dialogResult = MessageBox.Show("There is unsaved data.  Are you sure you want to quit?", 
               "UnsavedData", 
               MessageBoxButtons.YesNo);

            //If user choosees yes, close dialog.  Otherwise cancel closing.
            if(dialogResult == DialogResult.Yes)
            {
                
            }
            else
            {
                e.Cancel = true;
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
                    ContentsChanged(txb.Text);
                    RowChanged(currentRow);
                    ColChanged(currentCol);
                    //MessageBox.Show(txb.Text);
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

        /// <summary>
        /// Setter for the cell name box
        /// </summary>
        /// <param name="cName"></param>
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
    
        private void SpreadsheetPanel_Load(object sender, EventArgs e)
        {
        }

        public void CircularExceptionWarinig()
        {
            MessageBox.Show("Could not evaluate circular equation.  Please provide a correct equation."
                , "Circular Exception Error", MessageBoxButtons.OK);
        }

        public void FormulaExceptionWarning()
        {
            MessageBox.Show("Could not evaluate equation.  Please provide a correct equation format."
                , "Equation Format Error", MessageBoxButtons.OK);
        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Choosing any cell below will allow you to edit the cell." + "\r"
                + "Editing a cell by typing your input in the contents box at the top." + "\r"
                + "A valid entry is a number, words, or a valid formula." + "\r"
                + "A valid formula begins with an '=' along with cell names and these mathematical signs. '+ - * /'" + "\r"
                + "Example:  =A1 + A2" + "\r"
                + "Be careful use only valid fomulas and avoid circular equations that cannot be calculated." + "\r"
                + "A circular equation is tryint to make a cell equal to itself!" + "\r"
                + "Happy spreadsheet-ing!",
                "How To Use This Spreadsheet", MessageBoxButtons.OK);
        }
    }
}
