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
        }

        private void HandleSave()
        {
            string s = "tempSpreadsheet";
            TextWriter writer = new StreamWriter(s);
            spreadsheet.Save(writer);
            
        }

        private void HandleContentsChanged(string obj)
        {
            throw new NotImplementedException();
        }

        private void HandleGetCellInfo(string obj)
        {
            throw new NotImplementedException();
        }

        private void HandleCloseEvent()
        {
            throw new NotImplementedException();
        }

        private void HandleNewFileChosen(string fileName)
        {
            window.Title = fileName;
 
        }

        public bool isChanged()
        {
            return spreadsheet.Changed;
        }
    }
}
