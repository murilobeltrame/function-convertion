using FunctionConvertion.App;

// Example usage of the FunctionCalculator
double value = 3;
string function = "y = x * 7.88";

double result = FunctionCalculator.Calculate(value, function);

Console.WriteLine($"Input value: {value}");
Console.WriteLine($"Function: {function}");
Console.WriteLine($"Result: {result}");

// Additional example
value = 5;
function = "y = x^2 + 2*x + 1";
result = FunctionCalculator.Calculate(value, function);

Console.WriteLine($"\nInput value: {value}");
Console.WriteLine($"Function: {function}");
Console.WriteLine($"Result: {result}");
