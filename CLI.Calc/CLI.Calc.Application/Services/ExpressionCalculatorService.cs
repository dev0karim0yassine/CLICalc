using CLI.Calc.Application.Contracts;

namespace CLI.Calc.Application.Services
{
    public class ExpressionCalculatorService : IExpressionCalculatorService
    {
        readonly ICalculator _calculator;
        readonly CalculatorExtender _calculatorExtender;

        public ExpressionCalculatorService(ICalculator calculator, CalculatorExtender calculatorExtender)
        {
            _calculator = calculator;
            _calculatorExtender = calculatorExtender;
        }

        /// <summary>
        /// Add new operator and it's behavious to the calculator
        /// </summary>
        /// <param name="key">Operator key to be added exemple "/"</param>
        /// <param name="operation">The operation to be performed</param>
        public void AddOperator(string key, Func<int, int, decimal> operation)
        {
            _calculatorExtender.AddOperator(key, operation);
        }

        /// <summary>
        /// Remove an operator by it's key
        /// </summary>
        /// <param name="key">Operator key to be added exemple "/"</param>
        public void RemoveOperator(string key)
        {
            _calculatorExtender.RemoveOperator(key);
        }

        /// <summary>
        /// Parse and calculate the given Expression
        /// </summary>
        /// <param name="expression">The expresion to parse and calculate</param>
        /// <returns>The result of the expression</returns>
        /// <exception cref="Exception"></exception>
        public decimal CalculateExpression(string expression)
        {
            var tokens = expression.Split(" ");
            var numbers = new LinkedList<decimal>();
            var operators = new LinkedList<string>();

            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                if (!token.All(char.IsDigit) && _calculator.IsValidKey(token))
                {
                    if (operators.Count > 0 && _calculator.IsValidKey(operators.Last()) &&
                           ShouldCalculateCurrent(token, operators.Last()))
                    {
                        var result = _calculator.ApplyOperator(ref operators, ref numbers, true);
                        numbers.AddLast(result);
                    }
                    operators.AddLast(token);
                }
                else
                {
                    numbers.AddLast(decimal.Parse(token));
                }
            }

            while (operators.Count > 0)
            {
                var result = _calculator.ApplyOperator(ref operators, ref numbers, false);
                numbers.AddFirst(result);
            }

            if (numbers.Count != 1)
            {
                throw new Exception("Invalid expression");
            }

            return numbers.Last();
        }

        private static bool ShouldCalculateCurrent(string currentOperator, string lastOperator)
        {
            int currentPrecedence = currentOperator == "*" || currentOperator == "/" ? 2 : 1;
            int stackPrecedence = lastOperator == "*" || lastOperator == "/" ? 2 : 1;
            return currentPrecedence <= stackPrecedence;
        }
    }
}
