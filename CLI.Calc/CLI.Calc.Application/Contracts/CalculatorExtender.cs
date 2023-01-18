namespace CLI.Calc.Application.Contracts
{
    public abstract class CalculatorExtender
    {
        public virtual int AddOperator(string key, Func<int, int, decimal> operation)
        {
            throw new NotImplementedException();
        }

        public virtual int RemoveOperator(string key)
        {
            throw new NotImplementedException();
        }
    }
}
