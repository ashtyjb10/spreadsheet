// Written by Joe Zachary for CS 3500, January 2017.
// Modified by Nathan Herrmann for CS 3500, February 2018
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulas;
using System.Collections.Generic;

namespace FormulaTestCases
{
    /// <summary>
    /// These test cases are in no sense comprehensive!  They are intended to show you how
    /// client code can make use of the Formula class, and to show you how to create your
    /// own (which we strongly recommend).  To run them, pull down the Test menu and do
    /// Run > All Tests.
    /// </summary>
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void MyTest1()
        {
            Formula tester = new Formula("(((456 + 789 - 456) / 789) -1)");
            Assert.AreEqual(0, tester.Evaluate(Lookup4));
        }

        /// <summary>
        /// This tests that a syntactically incorrect parameter to Formula results
        /// in a FormulaFormatException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct1()
        {
            Formula f = new Formula("_");
        }

        /// <summary>
        /// This is another syntax error
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct2()
        {
            Formula f = new Formula("2++3");
        }

        /// <summary>
        /// Another syntax error.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct3()
        {
            Formula f = new Formula("2 3");
        }

        /// <summary>
        /// Makes sure that "2+3" evaluates to 5.  Since the Formula
        /// contains no variables, the delegate passed in as the
        /// parameter doesn't matter.  We are passing in one that
        /// maps all variables to zero.
        /// </summary>
        [TestMethod]
        public void Evaluate1()
        {
            Formula f = new Formula("2+3");
            Assert.AreEqual(f.Evaluate(v => 0), 5.0, 1e-6);
        }

        /// <summary>
        /// The Formula consists of a single variable (x5).  The value of
        /// the Formula depends on the value of x5, which is determined by
        /// the delegate passed to Evaluate.  Since this delegate maps all
        /// variables to 22.5, the return value should be 22.5.
        /// </summary>
        [TestMethod]
        public void Evaluate2()
        {
            Formula f = new Formula("x5");
            Assert.AreEqual(f.Evaluate(v => 22.5), 22.5, 1e-6);
        }

        /// <summary>
        /// Here, the delegate passed to Evaluate always throws a
        /// UndefinedVariableException (meaning that no variables have
        /// values).  The test case checks that the result of
        /// evaluating the Formula is a FormulaEvaluationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Evaluate3()
        {
            Formula f = new Formula("x + y");
            f.Evaluate(v => { throw new UndefinedVariableException(v); });
        }

        /// <summary>
        /// The delegate passed to Evaluate is defined below.  We check
        /// that evaluating the formula returns in 10.
        /// </summary>
        [TestMethod]
        public void Evaluate4()
        {
            Formula f = new Formula("x + y");
            Assert.AreEqual(f.Evaluate(Lookup4), 10.0, 1e-6);
        }

        /// <summary>
        /// This uses one of each kind of token.
        /// </summary>
        [TestMethod]
        public void Evaluate5()
        {
            Formula f = new Formula("(x + y) * (z / x) * 1.0");
            Assert.AreEqual(f.Evaluate(Lookup4), 20.0, 1e-6);
        }

        /// <summary>
        /// A Lookup method that maps x to 4.0, y to 6.0, and z to 8.0.
        /// All other variables result in an UndefinedVariableException.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public double Lookup4(String v)
        {
            switch (v)
            {
                case "x": return 4.0;
                case "y": return 6.0;
                case "z": return 8.0;
                default: throw new UndefinedVariableException(v);
            }
        }

        /// <summary>
        /// This method tests to make sure that normal format errors are calle when the new constructor is called
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ThreeParamConstruct1()
        {
            Formula f = new Formula(")(x - 9) + 32 * 4", s => s.ToLower(), v => true);
        }

        /// <summary>
        /// This method tests to make sure that if the validator returns false, an error is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ThreeParamConstruct2()
        {
            Formula f = new Formula("(x - 9) + 32 * 4", s => s.ToUpper(), v => false);
        }

        /// <summary>
        /// This method tests to make sure that the ToString values return string values in normalized form.
        /// </summary>
        [TestMethod]
        public void ToStringTester1()
        {
            Formula f1 = new Formula("(x - 9)", s => s.ToUpper(), v => true);
            Formula f2 = new Formula("(x - 9)");

            Assert.AreEqual(f1.Evaluate(x => 9), f2.Evaluate(x => 9));

            string f1String = f1.ToString();
            string f2String = f2.ToString();

            Assert.AreNotEqual(f1String, f2String);
        }

        //Ensures that an exception is thrown the N(x) is not a valid token.
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ToStringTester2()
        {
            Formula f = new Formula("(x - 9) + 32 * 4", s => "_", v => false);
        }

        /// <summary>
        /// This method tests to make sure that if the validator returns false, an error is thrown.
        /// </summary>
        [TestMethod]
        public void GetVariablesTester1()
        {
            HashSet<string> toCheck = new HashSet<string>();
            int index = 0;
            toCheck.Add("(");
            toCheck.Add("X");
            toCheck.Add("-");
            toCheck.Add("9");
            toCheck.Add(")");
            Formula f = new Formula("(x - 9)", s => s.ToUpper(), v => true);

            ISet<string> set = f.GetVariables();

            foreach(string token in set)
            {
                Assert.AreEqual(true, toCheck.Contains(token));
                index++;
            }
        }

        /// <summary>
        /// Ensures that null parameters throw correct exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestForNullConstructor1()
        {
            Formula f = new Formula(null, s => s.ToLower(), v => true);
        }

        /// <summary>
        /// Ensures that null parameters throw correct exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestForNullConstructor2()
        {
            Formula f = new Formula("(x - 9) + 32 * 4", null, v => true);
        }

        /// <summary>
        /// Ensures that null parameters throw correct exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestForNullConstructor3()
        {
            Formula f = new Formula("(x - 9) + 32 * 4", s => s.ToLower(), null);
        }
    }
}
