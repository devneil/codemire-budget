using System;
using System.Linq;
using HomeBudget.Common;
using HomeBudget.OperationUseCases.Request;

namespace HomeBudget.OperationUseCases
{
    public class GetPositionsUseCase
    {
        private readonly IOperationRepository _repository;

        public GetPositionsUseCase(IOperationRepository repository)
        {
            _repository = repository;
        }

        public void Execute(IRequest request)
        {
            var req = (OperationRequest<DateTime>)request;

            var trans = _repository.GetAccountTransactions();
            var accounts = _repository.GetAccountBalances();

            var positions = Operations.GetPositionsUpToDate(req.Dto, accounts, trans);
            
            OperationEvents.BroadcastPositionsInfo(positions.ToArray());
        }
    }
}