﻿namespace CLI.Calc.Application.Contracts
{
    public interface ICalculator
    {
        decimal Plus(int n1, int n2);
        decimal Minus(int n1, int n2);
        decimal Multiply(int n1, int n2);

        int AddOperator(string key, Func<int, int, decimal> operation);

        bool IsValidKey(string key);
    }
}
