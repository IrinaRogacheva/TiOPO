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
        public void ReassigneVarWithAnotherFnValue_VarAndValueAreAdded()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();
            calculator.SetVariable("x", "20");
            DeclareFunction(calculator, "fn", "x");
            calculator.SetVariable("y", "fn");

            Assert.IsTrue(calculator.GetValue("y") == 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The variable name must not match the name of the declared function")]
        public void ReassigneVarWithFnName_ArgumentExceptionThrown()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();
            DeclareVariable(calculator, "x");
            DeclareFunction(calculator, "fn", "x");

            calculator.SetVariable("fn", "3,14");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "A variable named \"y\" is not declared")]
        public void ReassigneVarWithNameThatNotDeclared_InvalidOperationExceptionThrown()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();

            calculator.SetVariable("x", "y");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Division by 0")]
        public void SetDivisibleValueToZero_InvalidOperationExceptionThrown()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();
            calculator.SetVariable("x", "60");
            calculator.SetVariable("y", "20");
            DeclareFunction(calculator, "fn", "x/y");

            calculator.SetVariable("y", "0");
            Assert.IsTrue(calculator.GetValue("y") == 20);
        }

        private void DeclareVariable(Calculator.Calculator calculator, string varName)
        {
            calculator.DeclareVariable(varName);
        }

        private void DeclareFunction(Calculator.Calculator calculator, string fnName, string fnValue)
        {
            calculator.DeclareFunction(fnName, fnValue);
        }
    }

    [TestClass]
    public class DeclareFunction
    {
        [TestMethod]
        public void DeclareFunctionWithOperation_FunctionIsDeclared()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();
            DeclareVariable(calculator, "x");
            DeclareVariable(calculator, "y");

            calculator.DeclareFunction("fn", "x+y");
            Dictionary<string, double> fns = calculator.GetFns();

            Assert.IsTrue(fns.ContainsKey("fn"));
            Assert.IsTrue(Double.IsNaN(fns["fn"]));
        }

        [TestMethod]
        public void RecalculateFunction_FunctionIsDeclared()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();
            DeclareVariable(calculator, "x");
            DeclareVariable(calculator, "y");
            calculator.DeclareFunction("fn", "x-y");

            SetVariable(calculator, "x", "50");
            SetVariable(calculator, "y", "20");

            Assert.IsTrue(calculator.GetValue("fn") == 30);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Division by 0")]
        public void DeclareFunctionWhenDivisorIs0_InvalidOperationExceptionThrown()
        {
            Calculator.Calculator calculator = new Calculator.Calculator();
            SetVariable(calculator, "x", "5");
            SetVariable(calculator, "y", "0");

            calculator.DeclareFunction("XDivideY", "x/y");
        }

        private void SetVariable(Calculator.Calculator calculator, string varName, string varValue)
        {
            calculator.SetVariable(varName, varValue);
        }

        private void DeclareVariable(Calculator.Calculator calculator, string varName)
        {
            calculator.DeclareVariable(varName);
        }
    }
}