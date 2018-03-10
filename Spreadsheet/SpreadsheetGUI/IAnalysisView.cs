using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    /// <summary>
    /// Controller is allowed to use these to modify the view.
    /// </summary>
    public interface IAnalysisView
    {
        event Action<string> NewFileChosen;

        event Action<string> SaveFileChosen;

        event Action<string> ContentsChanged;

        event Action SelectionChanged;

        event Action<int> ColChanged;

        event Action<int> RowChanged;

        event Action<FormClosingEventArgs> CloseEvent;

        void CellNameText(string CellName);

        void ContentsBox(object contents);

        void ValueBox(object value);
        void UpdatedValue(int col, int row, object value);

        //bool isChanged { get; }

        string Title { set; get; }

        //string Content { set; }

        //string Value {   set; }

        //string Cell { set; }

        void DoClose();

        void QuitWarning(FormClosingEventArgs e);

        void CircularExceptionWarinig();

        void FormulaExceptionWarning();

        void CouldNotLoadFileMessage();

        void CouldNotSaveFileMessage();
    }
}
