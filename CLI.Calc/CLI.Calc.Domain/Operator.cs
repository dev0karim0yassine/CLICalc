namespace CLI.Calc.Domain
{
    public class Operator
    {
        public string OperatorName { get; set; }
        public Func<int, int, decimal> Operation { get; set; }
        public bool IsBaseOperator { get; set; }
        public bool IsPriorOperator { get; set; }

        public Operator(string operatorName, Func<int, int, decimal> operation, bool isBaseOperator, bool isPriorOperator)
        {
            OperatorName = operatorName;
            Operation = operation;
            IsBaseOperator = isBaseOperator;
            IsPriorOperator = isPriorOperator;
        }
    }
}
