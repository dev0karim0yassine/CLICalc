namespace CLI.Calc.Application.Contracts
{
    public interface ICalculator
    {
        decimal Plus(int n1, int n2);
        
        decimal Minus(int n1, int n2);
        
        decimal Multiply(int n1, int n2);

        bool IsKeyFound(string key);

        int AddCustomOperator(string key, Func<int, int, decimal> operation);

        int RemoveCustomOperator(string key);

        decimal ApplyOperator(ref LinkedList<string> operators, ref LinkedList<decimal> numbers, bool calculateAll);
    }
}
