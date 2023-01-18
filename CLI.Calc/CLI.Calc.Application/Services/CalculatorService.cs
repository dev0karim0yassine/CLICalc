using CLI.Calc.Application.Contracts;
using CLI.Calc.Application.Exceptions;

namespace CLI.Calc.Application.Services
{
    public class CalculatorService : ICalculator
    {
        private readonly Dictionary<string, Func<int, int, decimal>> _operators;

        public CalculatorService()
        {
            _operators = new()
            {
                { "+", (first, second) => Plus(first, second)  },
                { "-", (first, second) => Minus(first, second)  },
                { "*", (first, second) => Multiply(first, second)  }
            };
        }

        /// <summary>
        /// Add two numbers
        /// </summary>
        /// <param name="n1">First Number</param>
        /// <param name="n2">Second Number</param>
        /// <returns>Sum as decimal</returns>
        public decimal Plus(int n1, int n2) => (decimal)n1 + (decimal)n2;

        /// <summary>
        /// Subtract two numbers
        /// </summary>
        /// <param name="n1">First Number</param>
        /// <param name="n2">Second Number</param>
        /// <returns>Subtraction as decimal</returns>
        public decimal Minus(int n1, int n2) => (decimal)n1 - (decimal)n2;

        /// <summary>
        /// Multiply two numbers
        /// </summary>
        /// <param name="n1">First Number</param>
        /// <param name="n2">Second Number</param>
        /// <returns>Multiplication as decimal</returns>
        public decimal Multiply(int n1, int n2) => (decimal)n1 * (decimal)n2;

        /// <summary>
        /// Add new operator and it's behavious to the calculator
        /// </summary>
        /// <param name="key">Operator key to be added exemple "/"</param>
        /// <param name="operation">The operation to be performed</param>
        /// <returns>The number of current operators</returns>
        public int AddCustomOperator(string key, Func<int, int, decimal> operation)
        {
            if (IsKeyFound(key))
            {
                throw new CalculatorException($"Key {key} already exists.");
            }

            _operators.Add(key, operation);

            return _operators.Count;
        }

        /// <summary>
        /// Remove an operator by it's key
        /// </summary>
        /// <param name="key">Operator key to be added exemple "/"</param>
        /// <returns>The number of current operators</returns>
        public int RemoveCustomOperator(string key)
        {
            if (!IsKeyFound(key))
            {
                throw new CalculatorException($"Key {key} was not found.");
            }

            _operators.Remove(key);

            return _operators.Count;
        }

        /// <summary>
        /// Checks if the given key already exist in the list of operators
        /// </summary>
        /// <param name="key">Operator key to check</param>
        /// <returns>true if the key exist in the list of operators, otherwise false</returns>
        public bool IsKeyFound(string key)
        {
            return _operators.ContainsKey(key);
        }

        /// <summary>
        /// Calculate the given numbers by applying the opperator
        /// </summary>
        /// <param name="operators">List of operators</param>
        /// <param name="numbers">List of numbers</param>
        /// <param name="calculateAll">true to start from the end of the list, otherwise false</param>
        /// <returns>the calculated reslut by operator</returns>
        public decimal ApplyOperator(ref LinkedList<string> operators, ref LinkedList<decimal> numbers, bool calculateAll)
        {
            decimal result = 0;
            string key;
            decimal firstNumber;
            decimal secondNumber;

            if (!calculateAll)
            {
                key = operators.Last(); operators.RemoveLast();
                firstNumber = numbers.Last(); numbers.RemoveLast();
                secondNumber = numbers.Last(); numbers.RemoveLast();

                result = _operators[key]((int)firstNumber, (int)secondNumber);
            }
            else
            {
                while (operators.Count > 0)
                {
                    key = operators.First(); operators.RemoveFirst();
                    firstNumber = numbers.First(); numbers.RemoveFirst();
                    secondNumber = numbers.First(); numbers.RemoveFirst();
                    result = _operators[key]((int)firstNumber, (int)secondNumber);

                    numbers.AddFirst(result);
                }

                result = numbers.Last();
            }

            return result;
        }
    }
}
