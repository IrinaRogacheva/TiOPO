using Calculator;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator.Calculator calculator = new Calculator.Calculator();
            calculator.DeclareVariable("x");
            calculator.DeclareFunction("fn", "x");

        }
    }
}
