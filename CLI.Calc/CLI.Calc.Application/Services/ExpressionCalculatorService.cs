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
        public void AddOperator(string key, Func<int, int, decimal> operation)
        {
            _calculator.AddCustomOperator(key, operation);
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
            var numbers = new LinkedList<decimal>();
            var operators = new LinkedList<string>();

            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                if (token.All(char.IsDigit))
                {
                    numbers.AddLast(decimal.Parse(token));
                }
                else if (_calculator.IsKeyFound(token))
                {
                    var previousOperator = operators.Count > 0 ? operators.Last() : default;
                    var currentOperator = token;
                    if (previousOperator != default && CanApplyCurrent(currentOperator, previousOperator))
                    {
                        var result = _calculator.ApplyOperator(ref operators, ref numbers, calculateAll: false);
                        numbers.AddLast(result);
                    }
                    operators.AddLast(token);
                }
            }

            return _calculator.ApplyOperator(ref operators, ref numbers, calculateAll: true);

        }

        private static bool CanApplyCurrent(string currentOperator, string lastOperator)
        {

            int currentOperatorPriotory = GetOperatorPriorioty(currentOperator);
            int lastOperatorPriotory = GetOperatorPriorioty(lastOperator);
            return currentOperatorPriotory <= lastOperatorPriotory;
        }
        
        private static int GetOperatorPriorioty(string _operator)
        {
            return _operator == "*" || _operator == "/" ? 2 : 1;
        }
    }
}
