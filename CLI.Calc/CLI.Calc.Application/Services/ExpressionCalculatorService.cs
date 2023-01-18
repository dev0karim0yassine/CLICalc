using CLI.Calc.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Calc.Application.Services
{
    public class ExpressionCalculatorService
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
        public static decimal CalculateExpression(string expression)
        {
            var tokens = expression.Split(" ");
            var numbers = new LinkedList<decimal>();
            var operators = new LinkedList<string>();

            //ToDo: Calculation Logic

            if (numbers.Count != 1)
            {
                throw new Exception("Invalid expression");
            }

            return numbers.Last();
        }

    }
}
