using System;
using System.Collections.Generic;
using System.Linq;
using HomeBudget.Common;
using HomeBudget.OperationUseCases.RequestDto;

namespace HomeBudget.OperationUseCases
{
    public class Operations
    {
        public static decimal GetPositionAtDate(DateTime date, AccountBalanceInfoDto[] accounts, ITransactionDto[] transactions)
        {
            foreach (var transactionDto in transactions)
            {
                if (transactionDto.Date <= date)
                {
                    var account = accounts.First(acc => acc.AccountName == transactionDto.AccountName);
                    account.Balance += transactionDto.Value;
                }
            }
            return accounts.Sum(acc => acc.Balance);
        }
        
        public static ICollection<PositionInfoDto> GetPositionsUpToDate(DateTime date, AccountBalanceInfoDto[] accounts, ITransactionDto[] transactions)
        {
            var positions = new Dictionary<DateTime, decimal>();
            foreach (var transactionDto in transactions)
            {
                if (transactionDto.Date <= date)
                {
                    var account = accounts.First(acc => acc.AccountName == transactionDto.AccountName);
                    account.Balance += transactionDto.Value;
                    positions[transactionDto.Date] = accounts.Sum(acc => acc.Balance);
                }
            }
            return PositionsFromDictionary(positions);
        }

        private static ICollection<PositionInfoDto> PositionsFromDictionary(Dictionary<DateTime, decimal> positions)
        {
            var collection = new PositionInfoDto[positions.Count];
            int i = 0;
            foreach (KeyValuePair<DateTime, decimal> pair in positions)
            {
                collection[i++] = new PositionInfoDto(pair.Value, pair.Key);
            }
            return collection;
        }
    }
}