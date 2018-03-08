using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetGUI
{
    class Controller
    {
        //Window being controlled
        private IAnalysisView window;


        private string fileContents = "";

        /// <summary>
        /// Control of the window.
        /// </summary>
        /// <param name="window"></param>
        public Controller(IAnalysisWindow window)
        {
            this.window = window;
            window.NewFileChosen += HandleNewFileChosen;

            
        }

    }
}
