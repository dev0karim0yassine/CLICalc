using CLI.Calc.Application.Contracts;
using Moq;

namespace CLI.Calc.Application.Test
{
    public class ExpressionCalculatorServiceTest
    {
        private readonly Mock<ICalculator> calculator;
        private readonly Mock<CalculatorExtender> calculatorExtender;
        public ExpressionCalculatorServiceTest()
        {
            calculator = new Mock<ICalculator>();
            calculatorExtender = new Mock<CalculatorExtender>();
        }

        [Fact]
        public void AddOperator_WhenCalledWithValidOperator_AddsOperator()
        {
            // Arrange
            var expressionCalculatorService = new ExpressionCalculatorService(calculator.Object, calculatorExtender.Object);
            string key = "/";
            Func<int, int, decimal> operation = (first, second) => (decimal)first / (decimal)second;

            // Act
            expressionCalculatorService.AddOperator(key, operation);

            // Assert
            calculatorExtender.Verify(x => x.AddOperator(key, operation), Times.Once);
        }

        [Fact]
        public void RemoveOperator_WhenCalledWithValidOperator_RemovesOperator()
        {
            // Arrange
            var expressionCalculatorService = new ExpressionCalculatorService(calculator.Object, calculatorExtender.Object);
            string key = "+";

            // Act
            expressionCalculatorService.RemoveOperator(key);

            // Assert
            calculatorExtender.Verify(x => x.RemoveOperator(key), Times.Once);
        }
    }
}
