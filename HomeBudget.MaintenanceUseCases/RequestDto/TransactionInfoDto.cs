using System;
using HomeBudget.Common;

namespace HomeBudget.MaintenanceUseCases.RequestDto
{
    public class TransactionInfoDto
    {
        public TransactionInfoDto(ITransactionDto transaction)
        {
            AccountName = transaction.AccountName;
            Value = transaction.Value;
            Date = transaction.Date;
        }

        public DateTime Date { get; private set; }
        public string AccountName { get; private set; }
        public decimal Value { get; private set; }
    }
}