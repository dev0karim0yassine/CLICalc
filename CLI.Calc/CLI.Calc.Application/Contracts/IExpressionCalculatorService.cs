namespace CLI.Calc.Application.Contracts
{
    public interface IExpressionCalculatorService
    {
        void AddOperator(string key, Func<int, int, decimal> operation);
        decimal CalculateExpression(string expression);
        void RemoveOperator(string key);
    }
}