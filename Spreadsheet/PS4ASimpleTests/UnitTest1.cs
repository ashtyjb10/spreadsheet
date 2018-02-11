using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace PS4ASimpleTests
{
    [TestClass]
    public class PS4Simple
    {
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MyTest_1()
        {
            Formula f = new Formula(")x+y3(", n => n.ToUpper(), v => false);
        }
    }
}
