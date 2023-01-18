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
            int result = _calculatorSut.AddOperator(key, operation);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void RemoveOperator_WhenCalledWithValidOperator_ReturnsNumberOfOperators()
        {
            // Arrange
            string key = "+";
            int expected = 2;

            // Act
            int result = _calculatorSut.RemoveOperator(key);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsValidKey_WhenCalledWithValidOperator_ReturnsTrue()
        {
            // Arrange
            string key = "*";

            // Act
            bool result = _calculatorSut.IsValidKey(key);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ApplyOperator_WhenCalledWithValidOperator_ReturnsCalculatedResult()
        {
            // Arrange
            LinkedList<string> operators = new LinkedList<string>();
            operators.AddFirst("+");
            LinkedList<decimal> numbers = new LinkedList<decimal>();
            numbers.AddFirst(5);
            numbers.AddFirst(8);
            bool firstInLastOut = true;
            decimal expected = 13;

            // Act
            decimal result = _calculatorSut.ApplyOperator(ref operators, ref numbers, firstInLastOut);

            // Assert
            Assert.Equal(expected, result);
        }

    }
}