using Xunit;
using FunctionConvertion.App;
using System;
using System.Data;

namespace FunctionConvertion.Tests
{
    public class FunctionCalculatorTests
    {
        [Theory]
        [InlineData(3, "y = x * 7.88", 23.64)]
        [InlineData(5, "y = x^2 + 2*x + 1", 36)]
        [InlineData(2, "y = x + 3", 5)]
        [InlineData(4, "y = (x * 2) / 4", 2)]
        [InlineData(3, "y = x^3 - 2*x + 1", 22)]
        [InlineData(1e9, "y = x + 1", 1000000001)]
        [InlineData(1e-9, "y = x * 1e9", 1)]
        [InlineData(-5, "y = x^2", 25)]
        [InlineData(3.14, "y = x * 2", 6.28)]
        public void Calculate_ShouldReturnCorrectResult(double value, string function, double expectedResult)
        {
            double result = FunctionCalculator.Calculate(value, function);
            Assert.Equal(expectedResult, result, 9); // Increased precision for small numbers
        }

        [Theory]
        [InlineData(2, "y = x^3", 8)]
        [InlineData(2, "y = x^-2", 0.25)]
        [InlineData(4, "y = x^0.5", 2)]
        [InlineData(2, "y = x^x", 4)]
        public void Calculate_ShouldHandleExponentsCorrectly(double value, string function, double expectedResult)
        {
            double result = FunctionCalculator.Calculate(value, function);
            Assert.Equal(expectedResult, result, 9);
        }

        [Fact]
        public void Calculate_ShouldThrowException_WhenInvalidFunction()
        {
            Assert.Throws<SyntaxErrorException>(() => FunctionCalculator.Calculate(3, "y = x @@ 2"));
        }

        [Fact]
        public void Calculate_ShouldThrowException_WhenDivisionByZero()
        {
            Assert.Throws<DivideByZeroException>(() => FunctionCalculator.Calculate(3, "y = x / 0"));
        }

        [Theory]
        [InlineData(3, "")]
        [InlineData(3, " ")]
        public void Calculate_ShouldThrowException_WhenEmptyFunction(double value, string emptyFunction)
        {
            Assert.Throws<ArgumentException>(() => FunctionCalculator.Calculate(value, emptyFunction));
        }

        [Fact]
        public void Calculate_ShouldThrowException_WhenNullFunction()
        {
            Assert.Throws<ArgumentNullException>(() => FunctionCalculator.Calculate(3, null));
        }
    }
}