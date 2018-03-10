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
        public string Title { get; set; }

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
            ContentsBoxCalled = true;
        }

        public bool ContentsBoxCalled
        {
            get; private set;
        }

        public void CouldNotLoadFileMessage()
        {
            throw new NotImplementedException();
        }

        public void CouldNotSaveFileMessage()
        {
            throw new NotImplementedException();
        }

        public void FormulaExceptionWarning()
        {
            throw new NotImplementedException();
        }

        public void QuitWarning(FormClosingEventArgs e)
        {
            CalledCloseEvent = true;
        }

        public void UpdatedValue(int col, int row, object value)
        {
            UpdateValueCalled = true;
        }

        public bool UpdateValueCalled
        {
            get; private set;
        }

        public void ValueBox(object value)
        {
            ValueBoxCalled = true;
        }

        public bool ValueBoxCalled
        {
            get; private set;
        }

        public void FireCloseEvent()
        {
            if(CloseEvent != null)
            {
                CloseEvent(null);
            }
        }
        public void QuitWarning()
        {
            CalledCloseEvent = true;
        }

        public void DoClose()
        {
            CalledCloseEvent = true;
        }

        public bool CalledCloseEvent
        {
            get; private set;
        }

        public void FireContentsChanged(string contents)
        {
            ContentsChanged(contents);
        }

        


    }
}
