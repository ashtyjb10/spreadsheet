using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Formulas
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MyTest_1()
        {
            Formula f = new Formula(")x+y3(", n => n.ToUpper(), v => false);
        }
    }
}
