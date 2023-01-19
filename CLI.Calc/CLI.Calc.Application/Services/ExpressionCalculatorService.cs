using CLI.Calc.Application.Contracts;

namespace CLI.Calc.Application.Services
{
    public class ExpressionCalculatorService : IExpressionCalculatorService
    {
        readonly ICalculator _calculator;

        public ExpressionCalculatorService(ICalculator calculator)
        {
            _calculator = calculator;
        }

        /// <summary>
        /// Add new operator and it's behavious to the calculator
        /// </summary>
        /// <param name="key">Operator key to be added exemple "/"</param>
        /// <param name="operation">The operation to be performed</param>
        public void AddOperator(string key, Func<int, int, decimal> operation, bool isPriorOperator)
        {
            _calculator.AddCustomOperator(key, operation, isPriorOperator);
        }

        /// <summary>
        /// Remove an operator by it's key
        /// </summary>
        /// <param name="key">Operator key to be added exemple "/"</param>
        public void RemoveOperator(string key)
        {
            _calculator.RemoveCustomOperator(key);
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
            var numbers = new Dictionary<int, int>();
            var powerOperators = new List<(int, string)>();
            var lowerOperators = new List<(int, string)>();

            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                if (token.All(char.IsDigit))
                {
                    numbers.Add(i, int.Parse(token));
                }
                else if (_calculator.IsKeyFound(token))
                {
                    if (_calculator.GetOperatorPriorioty(token))
                    {
                        powerOperators.Add((i, token));
                    }
                    else
                    {
                        lowerOperators.Add((i, token));
                    }
                }
            }

            foreach (var operatorName in powerOperators.ToList())
            {
                var index = operatorName.Item1;

                var prevNumber = numbers[index - 1];
                var nextNumber = numbers[index + 1];

                var newNumber = _calculator.ApplyOperator(operatorName.Item2, prevNumber, nextNumber);

                numbers[index + 1] = newNumber;
                numbers.Remove(index - 1);
            }

            // Reset the order 
            var newNumbers = numbers.Select(x => x.Value).ToList();

            foreach (var operatorName in lowerOperators.ToList())
            {
                var index = 1;

                var prevNumber = newNumbers[index - 1];
                var nextNumber = newNumbers[index];

                var newNumber = _calculator.ApplyOperator(operatorName.Item2, prevNumber, nextNumber);

                newNumbers[index] = newNumber;
                newNumbers.Remove(newNumbers[index - 1]);
            }

            return newNumbers.FirstOrDefault();
        }
    }
}
