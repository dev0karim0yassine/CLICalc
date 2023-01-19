namespace CLI.Calc.Application.Contracts
{
    public interface IExpressionCalculatorService
    {
        void AddOperator(string key, Func<int, int, decimal> operation, bool isPriorOperator);
        
        void RemoveOperator(string key);

        decimal CalculateExpression(string expression);
    }
}