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

        [TestMethod]
        public void SaveSpreadsheet()
        {
            ViewStub stub = new ViewStub();
            Controller controller = new Controller(stub);
            stub.FireContentsChanged("=A2");
            string fileName = "Anything";
            stub.FireSaveFileChosen(fileName);
            Assert.AreEqual(fileName, stub.Title);
        }

        [TestMethod]
        public void LoadNewSpreadsheet()
        {
            ViewStub stub = new ViewStub();
            Controller controller = new Controller(stub);
            stub.FireContentsChanged("=A2");
            string fileName = "Test.ss";
            stub.FireSaveFileChosen(fileName);
            stub.FireNewFileChosen(fileName);
            Assert.AreEqual(fileName, stub.Title);
        }
    }
}
