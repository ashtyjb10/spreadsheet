using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetGUI;

namespace SSGUITesters
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CloseNonChangedSpreadsheet()
        {
            ViewStub stub = new ViewStub();
            Controller controller = new Controller(stub);
            stub.FireCloseEvent();
            Assert.IsTrue(stub.CalledCloseEvent);

        }

        [TestMethod]
        public void CloseChangedSpreadsheet()
        {
            ViewStub stub = new ViewStub();
            Controller controller = new Controller(stub);
            stub.FireContentsChanged("=A2");
            stub.FireCloseEvent();
            Assert.IsTrue(stub.CalledCloseEvent);
            Assert.IsTrue(stub.ContentsBoxCalled);
            Assert.IsTrue(stub.ValueBoxCalled);
            Assert.IsTrue(stub.UpdateValueCalled);
        }
    }
}
