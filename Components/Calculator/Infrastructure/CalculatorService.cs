using Calculator.Core;

namespace Calculator.Infrastructure
{
    class CalculatorService : ICalculatorService
    {
        public int Sum(int first, int second)
        {
            return first + second;
        }
    }
}
