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

    }
}
