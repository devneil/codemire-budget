using HomeBudget.Common;
using HomeBudget.MaintenanceUseCases.RequestDto;

namespace HomeBudget.MaintenanceUseCases
{
    public static class MaintenanceEvents
    {
        public delegate void AccountInfoBroadcastHandler(AccountInfoDto accountInfo);
        public delegate void TransactionInfoBroadcastHandler(TransactionInfoDto transactionInfo);
        public delegate void RemoveTransactionBroadcastHandler(TransactionInfoDto transactionInfo);
        public delegate void RemoveAccountBroadcastHandler(AccountInfoDto accounInfo);

        public static event AccountInfoBroadcastHandler OnAccountInfoBroadcast;
        public static event TransactionInfoBroadcastHandler OnTransactionInfoBroadcast;
        public static event RemoveTransactionBroadcastHandler OnRemoveTransactionBroadcast;
        public static event RemoveAccountBroadcastHandler OnRemoveAccountBroadcast;
        
        internal static void BroadcastAccount(AccountInfoDto accountInfo)
        {
            var handler = OnAccountInfoBroadcast;
            if (handler != null) handler(accountInfo);
        }

        internal static void BroadcastTransaction(TransactionInfoDto transactionInfo)
        {
            var handler = OnTransactionInfoBroadcast;
            if (handler != null) handler(transactionInfo);
        }

        internal static void BroadcastRemoveTransaction(TransactionInfoDto transactionInfo)
        {
            var handler = OnRemoveTransactionBroadcast;
            if (handler != null) handler(transactionInfo);
        }
        
        internal static void BroadcastRemoveAccount(AccountInfoDto accountInfo)
        {
            var handler = OnRemoveAccountBroadcast;
            if (handler != null) handler(accountInfo);
        }

        
    }
}