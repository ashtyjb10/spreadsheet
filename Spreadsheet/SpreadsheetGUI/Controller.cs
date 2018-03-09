using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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


        /// <summary>
        /// Control of the window.
        /// </summary>
        /// <param name="window"></param>
        public Controller(IAnalysisView window)
        {
            this.window = window;
            this.spreadsheet = new Spreadsheet();
            window.NewFileChosen += HandleNewFileChosen;
            window.CloseEvent += HandleCloseEvent;
            window.GetCellInfo += HandleGetCellInfo;
            window.ContentsChanged += HandleContentsChanged;
            window.Save += HandleSave;
            window.SelectionChanged += HandleSelectionChanged;
            window.RowChanged += HandleRowChanged;
            window.ColChanged += HandleColChanged;
        }



        private void HandleRowChanged(int newRow)
        {
            row = newRow;
        }

        private void HandleColChanged(int newCol)
        {
            col = newCol;
            CellName = GetCellName();
            Console.WriteLine(CellName);

            window.CellNameText(CellName);
        }

        private void HandleSelectionChanged(string cellNameChangedTo)
        {
            // change this now!!!

            window.Content = spreadsheet.GetCellContents(cellNameChangedTo).ToString();
            window.Value = spreadsheet.GetCellValue(cellNameChangedTo).ToString();
            
        }

        private void HandleSave()
        {
            
        }

        private void HandleContentsChanged(string newContents)
        {
            //cells not changed yet.
           ISet<string> needToChangeCells =  spreadsheet.SetContentsOfCell("A1", newContents);
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
            window.Title = fileName;
 
        }

        public bool isChanged()
        {
            return spreadsheet.Changed;
        }
        private String GetCellName()
        {
            String CellName = "";
            int row1 = row + 1;
            switch (col)
            {
                case 0:
                    CellName = "A"+row1;
                    break;
                case 1:
                    CellName = "B" + row1;
                    break;
                case 2:
                    CellName = "C" + row1;
                    break;
                case 3:
                    CellName = "D" + row1;
                    break;
                case 4:
                    CellName = "E" + row1;
                    break;
                case 5:
                    CellName = "F" + row1;
                    break;
                case 6:
                    CellName = "G" + row1;
                    break;
                case 7:
                    CellName = "H" + row1;
                    break;
                case 8:
                    CellName = "I" + row1;
                    break;
                case 9:
                    CellName = "J" + row1;
                    break;
                case 10:
                    CellName = "K" + row1;
                    break;
                case 11:
                    CellName = "L" + row1;
                    break;
                case 12:
                    CellName = "M" + row1;
                    break;
                case 13:
                    CellName = "N" + row1;
                    break;
                case 14:
                    CellName = "O" + row1;
                    break;
                case 15:
                    CellName = "P" + row1;
                    break;
                case 16:
                    CellName = "Q" + row1;
                    break;
                case 17:
                    CellName = "R" + row1;
                    break;
                case 18:
                    CellName = "S" + row1;
                    break;
                case 19:
                    CellName = "T" + row1;
                    break;
                case 20:
                    CellName = "U" + row1;
                    break;
                case 21:
                    CellName = "V" + row1;
                    break;
                case 22:
                    CellName = "W" + row1;
                    break;
                case 23:
                    CellName = "x" + row1;
                    break;
                case 24:
                    CellName = "Y" + row1;
                    break;
                case 25:
                    CellName = "Z" + row1;
                    break;

            }
             return CellName;
        }
    }
}
