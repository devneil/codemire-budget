using System;

namespace HomeBudget.Common
{
    public interface ITransactionDto
    {
        decimal Value { get; }
        DateTime Date { get;}
        string AccountName { get; }
    }
}