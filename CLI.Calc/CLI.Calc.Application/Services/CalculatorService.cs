using CLI.Calc.Application.Contracts;
using CLI.Calc.Application.Exceptions;
using CLI.Calc.Domain;
using System.Linq;

namespace CLI.Calc.Application.Services
{
    public class CalculatorService : ICalculator
    {
        private readonly List<Operator> _operators;

        public CalculatorService()
        {
            _operators = new List<Operator>
            {
                new Operator("+", (first, second) => Plus(first, second), true, false),
                new Operator("-", (first, second) => Minus(first, second), true, false),
                new Operator("*", (first, second) => Multiply(first, second), true, true),
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
        /// <param name="isPriorOperator">Is Prior Operator</param>
        /// <returns>The number of current operators</returns>
        public int AddCustomOperator(string key, Func<int, int, decimal> operation, bool isPriorOperator)
        {
            if (IsKeyFound(key))
            {
                throw new CalculatorException($"Key {key} already exists.");
            }

            _operators.Add(new Operator(key, operation, false, isPriorOperator));

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

            var opertaor = _operators.First(o => o.OperatorName.Equals(key));

            if (opertaor.IsBaseOperator)
            {
                throw new CalculatorException($"cannot delete key {key}, (base opeartor).");
            }

            _operators.Remove(opertaor);

            return _operators.Count;
        }

        /// <summary>
        /// Checks if the given key already exist in the list of operators
        /// </summary>
        /// <param name="key">Operator key to check</param>
        /// <returns>true if the key exist in the list of operators, otherwise false</returns>
        public bool IsKeyFound(string key)
        {
            return _operators.Any(o => o.OperatorName.Equals(key));
        }

        /// <summary>
        /// Calculate the given numbers by applying the opperator
        /// </summary>
        /// <param name="operatorName">Operator Name</param>
        /// <param name="firstNumber">First Number</param>
        /// <param name="secondNumber">Second Number</param>
        /// <returns>the calculated reslut by operator</returns>
        public int ApplyOperator(string operatorName, int firstNumber, int secondNumber)
        {
            return (int)_operators.First(o => o.OperatorName.Equals(operatorName)).Operation(firstNumber, secondNumber);
        }

        /// <summary>
        /// Get Operator Priorioty
        /// </summary>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        public bool GetOperatorPriorioty(string operatorName)
        {
            return _operators.First(o => o.OperatorName.Equals(operatorName)).IsPriorOperator;
        }
    }
}
