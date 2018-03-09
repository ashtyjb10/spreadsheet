using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SS;

namespace SpreadsheetGUI
{
    public class Controller
    {
        //Window being controlled
        private IAnalysisView window;
        private AbstractSpreadsheet spreadsheet;
        private string spreadsheetTitle = "";
        private int row;
        private int col;
        private String CellName = "";
        private String[] cellLett = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
        "N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};


        /// <summary>
        /// Control of the window.
        /// </summary>
        /// <param name="window"></param>
        public Controller(IAnalysisView window)
        {
            this.window = window;
            this.spreadsheet = new Spreadsheet();
            window.Title = "";
            window.NewFileChosen += HandleNewFileChosen;
            window.SaveFileChosen += HandleSaveFileChosen;
            window.CloseEvent += HandleCloseEvent;
            window.GetCellInfo += HandleGetCellInfo;
            window.ContentsChanged += HandleContentsChanged;
            window.SelectionChanged += HandleSelectionChanged;
            window.RowChanged += HandleRowChanged;
            window.ColChanged += HandleColChanged;
        }

        public Controller(IAnalysisView window, string fileName)
        {
            this.window = window;
            StreamReader reader = new StreamReader(fileName);
            this.spreadsheet = new Spreadsheet(reader, new Regex(@"^([a-zA-Z]+)([1-9])(\d+)?$"));
            reader.Close();
            window.Title = "";
            window.NewFileChosen += HandleNewFileChosen;
            window.SaveFileChosen += HandleSaveFileChosen;
            window.CloseEvent += HandleCloseEvent;
            window.GetCellInfo += HandleGetCellInfo;
            window.ContentsChanged += HandleContentsChanged;
            window.SelectionChanged += HandleSelectionChanged;
            window.RowChanged += HandleRowChanged;
            window.ColChanged += HandleColChanged;

            int itterator = 0;
            foreach(string cell in spreadsheet.GetNamesOfAllNonemptyCells())
            {
                string firstLet = cell.Substring(0, 1);
                string rest = cell.Substring(1, cell.Length - 1);
                int row = Convert.ToInt32(rest) - 1;

                int col = GetColumn(firstLet);

                window.UpdatedValue(col, row, spreadsheet.GetCellValue(cell));

                //if we are on the first one we need to update the current boxes!
                if (itterator == 0)
                {
                    window.CellNameText(cell);
                    window.ContentsBox(spreadsheet.GetCellContents(cell));
                    window.ValueBox(spreadsheet.GetCellValue(cell));
                    //update the current contents box and value box
                    itterator++;
                }
            }
        }

        private void HandleSaveFileChosen(string obj)
        {
            window.Title = obj;
            StreamWriter writer = new StreamWriter(obj);
            spreadsheet.Save(writer);
            writer.Close();
            

        }

        private void HandleRowChanged(int newRow)
        {
            row = newRow;
        }

        private void HandleColChanged(int newCol)
        {
            col = newCol;
            CellName = GetCellName();

            window.CellNameText(CellName);
        }

        private void HandleSelectionChanged()
        {
            object value = spreadsheet.GetCellValue(CellName);
            object contents = spreadsheet.GetCellContents(CellName);

            window.ValueBox(value);
            window.ContentsBox(contents);
        }

        private void HandleSave()
        {
            
        }

        private void HandleContentsChanged(string newContents)
        {
            //cells not changed yet.
           ISet<string> needToChangeCells =  spreadsheet.SetContentsOfCell(CellName, newContents);

            int itterator = 0;
            foreach (string cell in needToChangeCells)
            {
                string firstLet = cell.Substring(0, 1);
                string rest = cell.Substring(1, cell.Length-1);
                int row = Convert.ToInt32(rest)-1;

                int col = GetColumn(firstLet);

                window.UpdatedValue(col, row, spreadsheet.GetCellValue(cell));

                //if we are on the first one we need to update the current boxes!
                if (itterator == 0)
                {
                    window.ValueBox(spreadsheet.GetCellValue(cell));
                    //update the current contents box and value box
                    itterator++;
                }

                //get value and contents pass back to the window to reset.
            }

            window.ContentsBox(newContents);
            
            
            //throw new NotImplementedException();
        }

        private void HandleGetCellInfo(string obj)
        {
            throw new NotImplementedException();
        }

        private void HandleCloseEvent()
        {
             window.DoClose();
        }

        private void HandleNewFileChosen(string fileName)
        {
            SpreadsheetApplicationContext.GetContext().RunNew(fileName);
        }

        public bool isChanged()
        {
            return spreadsheet.Changed;
        }
        private String GetCellName()
        {
            //have column number need the letter a =0
            int tempRow = row + 1;
            return cellLett[col] + tempRow;
        }
        private int GetColumn(string let)
        {
            //spreadsheet starts at A1 (row = -1)
            int itterator = 0;
            foreach (string letter in cellLett)
            {
                if(letter.Equals(let))
                {
                    break;
                }
                itterator++;
            }
            return itterator;
        }
    }
}
