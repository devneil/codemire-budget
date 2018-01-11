using System;
using HomeBudget.Common;
using HomeBudget.OperationUseCases.RequestDto;

namespace HomeBudget.OperationUseCases
{
    public interface IOperationRepository : IRepository
    {
        decimal GetAccountBalance(string accountName);
        
        ITransactionDto[] GetAccountTransactions();
        AccountBalanceInfoDto[] GetAccountBalances();
    }
}