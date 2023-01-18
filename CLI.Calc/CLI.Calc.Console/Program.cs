
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
expEvaluator.AddOperator("/", (first, second) => (decimal)first / second);

Console.WriteLine(expEvaluator.CalculateExpression("2 + 3"));
Console.WriteLine(expEvaluator.CalculateExpression("2 - 3"));
Console.WriteLine(expEvaluator.CalculateExpression("2 * 3 - 1"));
Console.WriteLine(expEvaluator.CalculateExpression("6 - 2 * 5"));
Console.WriteLine(expEvaluator.CalculateExpression("6 - 2 * 5 / 1"));

Console.ReadKey();

