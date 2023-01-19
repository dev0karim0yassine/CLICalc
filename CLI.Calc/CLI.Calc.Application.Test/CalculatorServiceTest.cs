using CLI.Calc.Application.Exceptions;

namespace CLI.Calc.Application.Test
{
    public class CalculatorServiceTest
    {
        readonly CalculatorService _calculatorSut;

        public CalculatorServiceTest()
        {
            _calculatorSut = new CalculatorService();
        }


        [Fact]
        public void Plus_WhenCalledWithTwoNumbers_ReturnsSum()
        {
            // Arrange
            int n1 = 5;
            int n2 = 8;
            decimal expected = 13;

            // Act
            decimal result = _calculatorSut.Plus(n1, n2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Minus_WhenCalledWithTwoNumbers_ReturnsDifference()
        {
            // Arrange
            int n1 = 15;
            int n2 = 8;
            decimal expected = 7;

            // Act
            decimal result = _calculatorSut.Minus(n1, n2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Multiply_WhenCalledWithTwoNumbers_ReturnsMultiplication()
        {
            // Arrange
            int n1 = 5;
            int n2 = 8;
            decimal expected = 40;

            // Act
            decimal result = _calculatorSut.Multiply(n1, n2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddOperator_WhenCalledWithValidOperator_ReturnsNumberOfOperators()
        {
            // Arrange
            string key = "/";
            Func<int, int, decimal> operation = (first, second) => (decimal)first / (decimal)second;
            int expected = 4;

            // Act
            int result = _calculatorSut.AddCustomOperator(key, operation, true);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddCustomOperator_WhenCalledWithValidOperator_ThrowsException()
        {
            // Arrange
            string key = "+";
            Func<int, int, decimal> operation = (first, second) => (decimal)first + (decimal)second;

            // Act & Assert
            Assert.Throws<CalculatorException>(() => _calculatorSut.AddCustomOperator(key, operation, true));
        }

        [Fact]
        public void RemoveCustomOperator_WhenCalledWithABaseOperator_ThrowsException()
        {
            // Arrange
            string key = "+";

            // Act & Assert
            Assert.Throws<CalculatorException>(() => _calculatorSut.RemoveCustomOperator(key));
        }

        [Fact]
        public void RemoveCustomOperator_WhenCalledWithValidOperator_ThrowsException()
        {
            // Arrange
            string key = "/";

            // Act & Assert
            Assert.Throws<CalculatorException>(() => _calculatorSut.RemoveCustomOperator(key));
        }

        [Fact]
        public void IsValidKey_WhenCalledWithValidOperator_ReturnsTrue()
        {
            // Arrange
            string key = "*";

            // Act
            bool result = _calculatorSut.IsKeyFound(key);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ApplyOperator_WhenCalledWithValidOperator_ReturnsCalculatedResult()
        {
            // Arrange
            var _operator = "+";
            var number1 = 5;
            var number2 = 8;
            decimal expected = 13;

            // Act
            decimal result = _calculatorSut.ApplyOperator(_operator, number1, number2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetOperatorPriorioty_WhenCalledWithValidOperator_ReturnsFalse()
        {
            // Arrange
            var expected = false;

            // Act
            var result = _calculatorSut.GetOperatorPriorioty("+");

            // Assert
            Assert.Equal(expected, result);
        }

    }
}