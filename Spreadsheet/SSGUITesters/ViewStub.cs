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

        public void FireSelectionChanged()
        {
            SelectionChanged();
        }

        public bool SelectionChangedResult()
        {
            if (ValueBoxCalled == true && ContentsBoxCalled == true)
            {
                return true;
            }
            return false;
        }

        public void CircularExceptionWarinig()
        {
        }

        public void ContentsBox(object contents)
        {
            ContentsBoxCalled = true;
        }

        public bool ContentsBoxCalled
        {
            get; private set;
        }
        public void FireUpdateRow(int s)
        {
            RowChanged(s);
        }
        public void FireUpdateCol(int s)
        {
            ColChanged(s);
        }
        public void CellNameText(String s)
        {
            cellNameCalled = true;
        }
        public bool cellNameCalled
        {
            get; private set;
        }



        public void CouldNotLoadFileMessage()
        {
        }

        public void CouldNotSaveFileMessage()
        {
        }

        public void FormulaExceptionWarning()
        {
            ExceptionWarningCalled = true;
        }

        public bool ExceptionWarningCalled
        {
            get; private set;
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

        public void FireSaveFileChosen(string fileName)
        {
            Title = fileName;
            SaveFileChosen(fileName);
        }

        public void FireNewFileChosen(string fileName)
        {
            Title = fileName;
            NewFileChosen(fileName);
        }


    }
}
