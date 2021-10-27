using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public struct Function
    {
        public string leftOperand;
        public Nullable<char> operation;
        public string rightOperand;

        public Function(string leftOperand) : this()
        {
            this.leftOperand = leftOperand;
        }

        public Function(string leftOperand, char operation, string rightOperand)
        {
            this.leftOperand = leftOperand;
            this.operation = operation;
            this.rightOperand = rightOperand;
        }
    }

    public class Calculator
    {
        private Dictionary<string, double> _vars;
        private Dictionary<string, HashSet<string>> _dependencies;
        private Dictionary<string, Function> _fns;
        private Dictionary<string, double> _fnValues;
        private static readonly char[] _actionArray = { '+', '-', '*', '/' };

        public Calculator()
        {
            _vars = new Dictionary<string, double>();
            _dependencies = new Dictionary<string, HashSet<string>>();
            _fns = new Dictionary<string, Function>();
            _fnValues = new Dictionary<string, double>();
        }

        public Dictionary<string, double> GetVars()
        {
            return _vars;
        }

        public Dictionary<string, double> GetFns()
        {
            return _fnValues;
        }

        public Nullable<double> GetValue(string identifier)
        {
            if (IsVarDeclared(identifier))
            {
                return _vars[identifier];
            }

            if (IsFnDeclared(identifier))
            {
                return _fnValues[identifier];
            }

            return null;
        }

        public void DeclareVariable(string varName)
        {
            if (IsVarDeclared(varName) || IsFnDeclared(varName))
            {
                throw new ArgumentException("The identifier matches one of the previously declared ones");
            }
            if (!IsIdCorrect(varName))
            {
                throw new ArgumentException("The identifier can consist of letters, numbers, and an underscore character and can't start with a digit");
            }
            _vars[varName] = Double.NaN;
            Console.WriteLine(_vars[varName]);
        }

        public void SetVariable(string varName, string varValue)
        {
            if (IsFnDeclared(varName))
            {
                throw new ArgumentException("The variable name must not match the name of the declared function");
            }
            if (!IsIdCorrect(varName))
            {
                throw new ArgumentException("The identifier can consist of letters, numbers, and an underscore character and can't start with a digit");
            }

            Nullable<double> previousValue = GetValue(varName);
            try
            {
                _vars[varName] = Convert.ToDouble(varValue);
            }
            catch (FormatException)
            {
                if (IsVarDeclared(varValue))
                {
                    _vars[varName] = _vars[varValue];
                }
                else if (IsFnDeclared(varValue))
                {
                    _vars[varName] = _fnValues[varValue];
                }
                else
                {
                    throw new InvalidOperationException("A variable named \"" + varValue + "\" is not declared");
                }
            }

            try
            {
                CheckForDependencies(varName);
            }
            catch (InvalidOperationException)
            {
                if (previousValue != null)
                { 
                    _vars[varName] = (double)previousValue; 
                }
                throw;
            }
        }

        public void DeclareFunction(string fnName, string fnValue)
        {
            if (IsVarDeclared(fnName) || IsFnDeclared(fnName))
            {
                throw new ArgumentException("The identifier matches one of the previously declared ones");
            }
            if (!IsIdCorrect(fnName))
            {
                throw new ArgumentException("The identifier can consist of letters, numbers, and an underscore character and can't start with a digit");
            }
            
            string leftOperand, rightOperand = null;
            Function resultFunction;
            if (FindOperation(fnValue) != null)
            {
                char operation = (char)FindOperation(fnValue);
                leftOperand = fnValue.Substring(0, fnValue.IndexOf(operation));
                rightOperand = fnValue.Substring(fnValue.IndexOf(operation) + 1);
                if (!IsVarDeclared(rightOperand) && !IsFnDeclared(rightOperand))
                {
                    throw new InvalidOperationException("A variable named \"" + rightOperand + "\" is not declared");
                }
                resultFunction = new Function(leftOperand, operation, rightOperand);
            }
            else
            {
                leftOperand = fnValue;
                resultFunction = new Function(leftOperand);
            }
            
            if (!IsVarDeclared(leftOperand) && !IsFnDeclared(leftOperand))
            {
                throw new InvalidOperationException("A variable named \"" + leftOperand + "\" is not declared");
            }
            
            try
            {
                if (rightOperand != null)
                {
                    CreateDependeciesSet(rightOperand);
                    _dependencies[rightOperand].Add(fnName);
                }
                CreateDependeciesSet(leftOperand);
                _dependencies[leftOperand].Add(fnName);
                _fns[fnName] = resultFunction;
                CalculateFunction(fnName);
            }
            catch (InvalidOperationException)
            {
                _fns.Remove(fnName);
                _dependencies.Remove(rightOperand);
                _dependencies.Remove(leftOperand);
                throw;
            }
        }
        
        private void CreateDependeciesSet(string key)
        {
            if (!_dependencies.ContainsKey(key))
            {
                _dependencies.Add(key, new HashSet<string>());
            }
        }

        private void CalculateFunction(string fnName)
        {
            Function function = _fns[fnName];
            if (function.operation != null)
            {
                try
                {
                    _fnValues[fnName] = CalculateOperation((double)GetValue(function.leftOperand), (double)GetValue(function.rightOperand), (char)function.operation);
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
            }
            else
            {
                _fnValues[fnName] = (double)GetValue(function.leftOperand);
            }
            CheckForDependencies(fnName);
        }
        private double CalculateOperation(double leftOperand, double rightOperand, char operation)
        {
            switch (operation)
            {
                case '+': return leftOperand + rightOperand;
                case '-': return leftOperand - rightOperand;
                case '*': return leftOperand * rightOperand;
                case '/':
                    if (rightOperand == 0)
                    {
                        throw new InvalidOperationException("Division by 0");
                    }
                    return leftOperand / rightOperand;
                default: throw new ArgumentException("Unknown action");
            }
        }
        private void CheckForDependencies(string dependencyName)
        {
            if (_dependencies.ContainsKey(dependencyName))
            {
                foreach (string functionName in _dependencies[dependencyName])
                {
                    CalculateFunction(functionName);
                }
            }
        }

        private bool IsVarDeclared(string varName)
        {
            return _vars.ContainsKey(varName);
        }

        private bool IsFnDeclared(string fnName)
        {
            return _fns.ContainsKey(fnName);
        }

        private Nullable<char> FindOperation(string fnValue)
        {
            foreach (char action in _actionArray)
            {
                if (fnValue.Contains(action))
                {
                    return action;
                }
            }
            return null;
        }

        private bool IsIdCorrect(string id)
        {
            if (String.IsNullOrEmpty(id) || Char.IsDigit(id[0]) || !id.All(x => char.IsLetter(x) || char.IsDigit(x) || x == '_'))
            {
                return false;
            }
            return true;
        }
    }
}
