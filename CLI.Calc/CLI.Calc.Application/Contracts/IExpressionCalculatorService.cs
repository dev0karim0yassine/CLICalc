namespace CLI.Calc.Application.Contracts
{
    public interface IExpressionCalculatorService
    {
        void AddOperator(string key, Func<int, int, decimal> operation);
        
        void RemoveOperator(string key);

        decimal CalculateExpression(string expression);
    }
}