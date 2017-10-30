using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcLibrary;

namespace UnitTestCalcLibrary
{
    [TestClass]
    public class UnitTestCalculator
    {
        [TestMethod]
        public void TestSum()
        {
            var calc = new Calculator();

            var result = calc.Sum("3", "2");
            var result1 = calc.Sum("0", "0");
            var result2 = calc.Sum("100", "0");
            var result3 = calc.Sum("a", "b");

            Assert.AreEqual("5", result);
            Assert.AreEqual("0", result1);
            Assert.AreEqual("100", result2);
            Assert.AreEqual("error", result3);
        }

        [TestMethod]
        public void TestdSum()
        {
            var calc = new Calculator();

            var result = calc.dSum("3", "2");
            var result1 = calc.dSum("0", "0");
            var result2 = calc.dSum("100", "0");
            var result3 = calc.dSum("a", "b");

            Assert.AreEqual(5, result);
            Assert.AreEqual(0, result1);
            Assert.AreEqual(100, result2);
            Assert.AreEqual(0, result3);
        }

        [TestMethod]
        public void TestSub()
        {
            var calc = new Calculator();

            var result = calc.Sub("3", "2");
            var result1 = calc.Sub("0", "0");
            var result2 = calc.Sub("2", "3");
            var result3 = calc.Sub("a", "b");

            Assert.AreEqual(1, result);
            Assert.AreEqual(0, result1);
            Assert.AreEqual(-1, result2);
            Assert.AreEqual(0, result3);
        }

        [TestMethod]
        public void TestMul()
        {
            var calc = new Calculator();

            var result = calc.Mul("3", "2");
            var result1 = calc.Mul("0", "0");
            var result2 = calc.Mul("-2", "3");
            var result3 = calc.Mul("a", "b");
            var result4 = calc.Mul("-2", "-3");

            Assert.AreEqual(6, result);
            Assert.AreEqual(0, result1);
            Assert.AreEqual(-6, result2);
            Assert.AreEqual(0, result3);
            Assert.AreEqual(6, result4);
        }

        [TestMethod]
        public void TestDiv()
        {
            var calc = new Calculator();

            var result = calc.Div("3", "2");
            var result1 = calc.Div("0", "0");
            var result2 = calc.Div("-3", "2");
            var result3 = calc.Div("a", "b");
            var result4 = calc.Div("-3", "-2");
            var result5 = calc.Div("0", "-2");
            var result6 = calc.Div("-3", "0");

            Assert.AreEqual(1.5, result);
            Assert.AreEqual(0, result1);
            Assert.AreEqual(-1.5, result2);
            Assert.AreEqual(0, result3);
            Assert.AreEqual(1.5, result4);
            Assert.AreEqual(0, result5);
            Assert.AreEqual(0, result6);
        }

        [TestMethod]
        public void TestSqr()
        {
            var calc = new Calculator();

            var result = calc.Sqr("3");
            var result1 = calc.Sqr("0");
            var result2 = calc.Sqr("-3");
            var result3 = calc.Sqr("a");

            Assert.AreEqual(9, result);
            Assert.AreEqual(0, result1);
            Assert.AreEqual(9, result2);
            Assert.AreEqual(0, result3);
        }

        [TestMethod]
        public void TestSqrt()
        {
            var calc = new Calculator();

            var result = calc.Sqrt("4");
            var result1 = calc.Sqrt("0");
            var result2 = calc.Sqrt("-4");
            var result3 = calc.Sqrt("a");

            Assert.AreEqual(2, result);
            Assert.AreEqual(0, result1);
            Assert.AreEqual(0, result2);
            Assert.AreEqual(0, result3);
        }
    }
}
