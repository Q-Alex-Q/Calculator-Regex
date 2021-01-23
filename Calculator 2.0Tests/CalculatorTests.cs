using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator_2._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Calculator_2._0.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void CalculateFileExpressionsTest()
        {
            //Arrage
            Calculator calc = new Calculator();

            //Act
            calc.CalculateFileExpressions(Directory.GetCurrentDirectory() + "\\TestFile.txt", Directory.GetCurrentDirectory() + "\\TestFileResult.txt");
            string[] expected = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\RightAnswersFile.txt");
            string[] actual = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\TestFileResult.txt");

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetExpressionResultTest()
        {
            //Arrage
            Calculator calc = new Calculator();
            double expected = 1515;

            //Act
            double actual = calc.GetExpressionResult("(-20+25)*3-5*(15*-20)");
            

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void GetExpressionResultTest2()
        {
            //Arrage
            Calculator calc = new Calculator();

            string actual = "";
            string expected = "На ноль делить нельзя.";

            //Act
            try
            {
                actual = calc.GetExpressionResult("20/(20-20)").ToString();
            }
            catch (DivideByZeroException e)
            {
                actual = e.Message;
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void GetExpressionResultTest3()
        {
            //Arrage
            Calculator calc = new Calculator();

            string actual = "";
            string expected = "Выражение имело неверный формат.";

            //Act
            try
            {
                actual = calc.GetExpressionResult("20/(x-20)-20").ToString();
            }
            catch (Exception)
            {
                actual = "Выражение имело неверный формат.";
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetExpressionResultTest4()
        {
            //Arrage
            Calculator calc = new Calculator();

            string actual = "";
            string expected = "20";

            //Act
            actual = calc.GetExpressionResult("(2+2*(3+3))+2*3").ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}