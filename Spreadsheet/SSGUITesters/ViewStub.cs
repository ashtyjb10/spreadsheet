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
        public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<string> NewFileChosen;

        public event Action<string> SaveFileChosen;

        public event Action<string> ContentsChanged;

        public event Action SelectionChanged;

        public event Action<int> ColChanged;

        public event Action<int> RowChanged;

        public event Action<FormClosingEventArgs> CloseEvent;

        public void CellNameText(string CellName)
        {
            throw new NotImplementedException();
        }

        public void CircularExceptionWarinig()
        {
            throw new NotImplementedException();
        }

        public void ContentsBox(object contents)
        {
            throw new NotImplementedException();
        }

        public void DoClose()
        {
            throw new NotImplementedException();
        }

        public void FormulaExceptionWarning()
        {
            throw new NotImplementedException();
        }

        public void QuitWarning(FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void UpdatedValue(int col, int row, object value)
        {
            throw new NotImplementedException();
        }

        public void ValueBox(object value)
        {
            throw new NotImplementedException();
        }
    }
}
