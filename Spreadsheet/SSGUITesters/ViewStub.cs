using SpreadsheetGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSGUITesters
{
    class ViewStub : IAnalysisView
    {
        public event Action<string> NewFileChosen;

        public event Action<string> SaveFileChosen;

        public event Action<string> ContentsChanged;

        public event Action SelectionChanged;

        public event Action<int> ColChanged;

        public event Action<int> RowChanged;

        public event Action<FormClosingEventArgs> CloseEvent;
    }
}
