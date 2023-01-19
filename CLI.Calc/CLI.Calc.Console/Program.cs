
using CLI.Calc.Application.Services;
using CLI.Calc.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;


// Create the service collection
var services = new ServiceCollection();

// Add your services
//services.AddSingleton<CalculatorService>();

//services.AddSingleton<ICalculator>(x => x.GetService<CalculatorService>());
//services.AddSingleton<CalculatorExtender>(x => x.GetService<CalculatorService>());
services.AddSingleton<ICalculator, CalculatorService>();
services.AddSingleton<IExpressionCalculatorService, ExpressionCalculatorService>();

// Create the service provider
var serviceProvider = services.BuildServiceProvider();

// Get the service
var expEvaluator = serviceProvider.GetService<IExpressionCalculatorService>();

// Use the service
expEvaluator.AddOperator("/", (first, second) => (decimal)first / second, true);

Console.WriteLine(expEvaluator.CalculateExpression("6 - 2 * 5")); //-4
Console.WriteLine(expEvaluator.CalculateExpression("1 + 2 * 5 * 4 + 8 * 9 / 2 - 2")); //75
Console.WriteLine(expEvaluator.CalculateExpression("2 + 3")); //5
Console.WriteLine(expEvaluator.CalculateExpression("2 - 3")); //-1
Console.WriteLine(expEvaluator.CalculateExpression("2 * 3 - 1")); //5
Console.WriteLine(expEvaluator.CalculateExpression("6 - 2 * 5")); //-4
Console.WriteLine(expEvaluator.CalculateExpression("6 - 2 * 5 / 1"));

Console.ReadKey();

