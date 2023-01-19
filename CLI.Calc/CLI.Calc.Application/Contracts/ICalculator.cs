namespace CLI.Calc.Application.Contracts
{
    public interface ICalculator
    {
        decimal Plus(int n1, int n2);
        
        decimal Minus(int n1, int n2);
        
        decimal Multiply(int n1, int n2);

        bool IsKeyFound(string key);

        int AddCustomOperator(string key, Func<int, int, decimal> operation, bool isPriorOperator);

        int RemoveCustomOperator(string key);

        int ApplyOperator(string operatorName, int firstNumber, int secondNumber);

        bool GetOperatorPriorioty(string operatorName);
    }
}
