using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Calculator;
using System.Collections.Generic;

namespace CalculatorTests
{
    [TestClass]
    public class DeclareVariable
    {
        [TestMethod]
        public void VariableWithCorrectName_VarIsAdded()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();

            calculator.DeclareVariable("x");
            Dictionary<string, double> vars = calculator.GetVars();

            Assert.IsTrue(Double.IsNaN(vars["x"]));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The identifier matches one of the previously declared ones")]
        public void VariableWithSameName_ArgumentExceptionThrown()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();

            calculator.DeclareVariable("x");
            calculator.DeclareVariable("x");
        }

        [TestMethod]
        public void UnderscoresInName_VarIsAdded()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();
            try
            {
                calculator.DeclareVariable("_first_x");
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The identifier can consist of letters, numbers, and an underscore character and can't start with a digit")]
        public void FirstCharacterIsDigit_ArgumentExceptionThrown()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();

            calculator.DeclareVariable("0x");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The identifier can consist of letters, numbers, and an underscore character and can't start with a digit")]
        public void ForbiddenCharacterInName_ArgumentExceptionThrown()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();

            calculator.DeclareVariable("x$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The identifier can consist of letters, numbers, and an underscore character and can't start with a digit")]
        public void NameIsEmptyString_ArgumentExceptionThrown()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();

            calculator.DeclareVariable("");
        }
    }

    [TestClass]
    public class SetVariable
    {
        [TestMethod]
        public void VariableWithCorrectNameAndCorrectDoubleValue_VarAndValueAreAdded()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();

            calculator.SetVariable("x", "3,14");
            Dictionary<string, double> vars = calculator.GetVars();

            Assert.IsTrue(vars["x"] == 3.14);
        }

        [TestMethod]
        public void ReassigneVarWithAnotherVarValue_VarAndValueAreAdded()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();

            calculator.SetVariable("x", "3,14");
            calculator.SetVariable("y", "x");

            Assert.IsTrue(calculator.GetValue("y") == 3.14);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "A variable named \"y\" is not declared")]
        public void ReassigneVarWithNameThatNotDeclared_InvalidOperationExceptionThrown()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();

            calculator.SetVariable("x", "y");
        }
    }

    [TestClass]
    public class DeclareFunction
    {
        Calculator.Calculator calculator;

        [TestInitialize]
        public void TestInit()
        {
            calculator = new Calculator.Calculator();
            calculator.DeclareVariable("x");
            calculator.DeclareVariable("y");

            calculator.SetVariable("x", "50");
            calculator.SetVariable("y", "0");
        }

        [TestMethod]
        public void DeclareFunctionWithoutOperation_FunctionIsDeclared()
        {
            calculator.DeclareFunction("fn", "x");
            Dictionary<string, double> fns = calculator.GetFns();

            Assert.IsTrue(fns["fn"] == 50);
        }

        [TestMethod]
        public void DeclareFunctionWithOperationPlus_FunctionIsDeclared()
        {
            calculator.DeclareFunction("fn", "x+y");
            Dictionary<string, double> fns = calculator.GetFns();

            Assert.IsTrue(fns["fn"] == 50);
        }

        [TestMethod]
        public void DeclareFunctionWithOperationMinus_FunctionIsDeclared()
        {
            calculator.DeclareFunction("fn", "x-y");

            Assert.IsTrue(calculator.GetValue("fn") == 50);
        }

        [TestMethod]
        public void DeclareFunctionWithOperationMultiply_FunctionIsDeclared()
        {
            calculator.DeclareFunction("fn", "x*y");

            Assert.IsTrue(calculator.GetValue("fn") == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Division by 0")]
        public void DeclareFunctionWhenDivisorIs0_InvalidOperationExceptionThrown()
        {
            calculator.DeclareFunction("XDivideY", "x/y");
        }
    }
}