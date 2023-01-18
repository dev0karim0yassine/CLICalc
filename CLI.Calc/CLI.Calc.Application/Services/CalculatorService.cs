using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Calc.Application.Services
{
    public class CalculatorService
    {
        private Dictionary<string, Func<int, int, decimal>> _operators;

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
        public static decimal Plus(int n1, int n2) => (decimal)n1 + (decimal)n2;

        /// <summary>
        /// Subtract two numbers
        /// </summary>
        /// <param name="n1">First Number</param>
        /// <param name="n2">Second Number</param>
        /// <returns>Subtraction as decimal</returns>
        public static decimal Minus(int n1, int n2) => (decimal)n1 - (decimal)n2;

        /// <summary>
        /// Multiply two numbers
        /// </summary>
        /// <param name="n1">First Number</param>
        /// <param name="n2">Second Number</param>
        /// <returns>Multiplication as decimal</returns>
        public static decimal Multiply(int n1, int n2) => (decimal)n1 * (decimal)n2;

        /// <summary>
        /// Add new operator and it's behavious to the calculator
        /// </summary>
        /// <param name="key">Operator key to be added exemple "/"</param>
        /// <param name="operation">The operation to be performed</param>
        /// <returns>The number of current operators</returns>
        public int AddOperator(string key, Func<int, int, decimal> operation)
        {
            if (!IsValidKey(key))
            {
                _operators.Add(key, operation);
            }

            return _operators.Count;
        }

        /// <summary>
        /// Checks if the given key already exist in the list of operators
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsValidKey(string key)
        {
            return _operators.ContainsKey(key);
        }
    }
}
