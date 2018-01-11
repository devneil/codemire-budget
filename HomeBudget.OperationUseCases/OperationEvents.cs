using HomeBudget.OperationUseCases.RequestDto;

namespace HomeBudget.OperationUseCases
{
    public static class OperationEvents
    {
        public delegate void AccountBalanceInfoBroadcastHandler(AccountBalanceInfoDto accountBalanceInfo);
        public delegate void PositionInfoBroadcastHandler(PositionInfoDto positionInfo);
        public delegate void PositionsInfoBroadcastHandler(PositionInfoDto[] positionInfo);

        public static event AccountBalanceInfoBroadcastHandler OnAccountBalanceInfoBroadcast;
        public static event PositionInfoBroadcastHandler OnPositionInfoBroadcast;
        public static event PositionsInfoBroadcastHandler OnPositionsInfoBroadcast;
        
        internal static void BroadcastPositionsInfo(PositionInfoDto[] positionsinfo)
        {
            var handler = OnPositionsInfoBroadcast;
            if (handler != null) handler(positionsinfo);
        }

        internal static void BroadcastPositionInfo(PositionInfoDto positioninfo)
        {
            var handler = OnPositionInfoBroadcast;
            if (handler != null) handler(positioninfo);
        }

        internal static void BroadcastAccountBalance(AccountBalanceInfoDto accountBalanceInfo)
        {
            var handler = OnAccountBalanceInfoBroadcast;
            if (handler != null) handler(accountBalanceInfo);
        }

    }
}