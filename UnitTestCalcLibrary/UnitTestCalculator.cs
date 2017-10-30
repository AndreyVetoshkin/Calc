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
        public void TestSub()
        {
        }
    }
}
