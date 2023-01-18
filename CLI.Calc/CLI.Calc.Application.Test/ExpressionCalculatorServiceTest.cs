using CLI.Calc.Application.Contracts;
using Moq;
using System;

namespace CLI.Calc.Application.Test
{
    public class ExpressionCalculatorServiceTest
    {
        private readonly Mock<ICalculator> calculator;

        public ExpressionCalculatorServiceTest()
        {
            calculator = new Mock<ICalculator>();
        }

        [Fact]
        public void AddOperator_WhenCalledWithValidOperator_AddsOperator()
        {
            // Arrange
            var expressionCalculatorService = new ExpressionCalculatorService(calculator.Object);
            string key = "/";
            Func<int, int, decimal> operation = (first, second) => (decimal)first / (decimal)second;

            // Act
            expressionCalculatorService.AddOperator(key, operation);

            // Assert
            calculator.Verify(x => x.AddCustomOperator(key, operation), Times.Once);
        }

        [Fact]
        public void RemoveOperator_WhenCalledWithValidOperator_RemovesOperator()
        {
            // Arrange
            var expressionCalculatorService = new ExpressionCalculatorService(calculator.Object);
            string key = "+";

            // Act
            expressionCalculatorService.RemoveOperator(key);

            // Assert
            calculator.Verify(x => x.RemoveCustomOperator(key), Times.Once);
        }

        [Fact]
        public void CalculateExpression_WhenCalledWithValidExpression_ReturnsResult()
        {
            // Arrange
            var expressionCalculatorService = new ExpressionCalculatorService(calculator.Object);
            string expression = "3 + 2";

            calculator.Setup(c => c.ApplyOperator(ref It.Ref<LinkedList<string>>.IsAny, ref It.Ref<LinkedList<decimal>>.IsAny, true))
                .Returns(5).Verifiable();

            calculator.Setup(c => c.IsKeyFound(It.IsAny<string>()))
                .Returns(true).Verifiable();

            // Act
            var result = expressionCalculatorService.CalculateExpression(expression);

            // Assert
            Assert.Equal(5, result);
            calculator.Verify();
        }
        [Fact]
        public void CalculateExpression_WhenCalledWithAnotherValidExpression_ReturnsResult()
        {
            // Arrange
            var expressionCalculatorService = new ExpressionCalculatorService(calculator.Object);
            string expression = "3 + 2 - 1";

            calculator.Setup(c => c.ApplyOperator(ref It.Ref<LinkedList<string>>.IsAny, ref It.Ref<LinkedList<decimal>>.IsAny, false))
                .Returns(5).Verifiable();

            calculator.Setup(c => c.ApplyOperator(ref It.Ref<LinkedList<string>>.IsAny, ref It.Ref<LinkedList<decimal>>.IsAny, true))
                .Returns(4).Verifiable();

            calculator.Setup(c => c.IsKeyFound(It.IsAny<string>()))
                .Returns(true).Verifiable();

            // Act
            var result = expressionCalculatorService.CalculateExpression(expression);

            // Assert
            Assert.Equal(4, result);
            calculator.Verify();
        }
    }
}
