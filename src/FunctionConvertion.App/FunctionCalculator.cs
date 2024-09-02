using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;

namespace FunctionConvertion.App
{
    public class FunctionCalculator
    {
        public static double Calculate(double value, string function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (string.IsNullOrWhiteSpace(function))
            {
                throw new ArgumentException("Function cannot be empty or whitespace.", nameof(function));
            }

            // Replace 'x' in the function with the actual value
            string expression = function.Replace("y =", "").Replace("x", value.ToString(CultureInfo.InvariantCulture));

            // Handle exponents (^) separately
            expression = HandleExponents(expression);

            // Check for division by zero
            if (IsDivisionByZero(expression))
            {
                throw new DivideByZeroException("Division by zero is not allowed.");
            }

            // Use DataTable to evaluate the mathematical expression
            DataTable dt = new DataTable();
            try
            {
                var result = dt.Compute(expression, "");
                if (result == DBNull.Value)
                {
                    throw new SyntaxErrorException($"Invalid function: {function}");
                }
                return Convert.ToDouble(result);
            }
            catch (Exception ex)
            {
                throw new SyntaxErrorException($"Invalid function: {function}", ex);
            }
        }

        private static string HandleExponents(string expression)
        {
            // Use regex to find all occurrences of exponential expressions
            var regex = new Regex(@"(\-?\d+(?:\.\d+)?)\s*\^\s*(\-?\d+(?:\.\d+)?)");
            return regex.Replace(expression, match =>
            {
                double baseNum = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                double exponent = double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                return Math.Pow(baseNum, exponent).ToString(CultureInfo.InvariantCulture);
            });
        }

        private static bool IsDivisionByZero(string expression)
        {
            var regex = new Regex(@"/\s*0(?![\d.])");
            return regex.IsMatch(expression);
        }
    }
}