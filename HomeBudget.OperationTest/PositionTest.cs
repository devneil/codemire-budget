using System;
using FluentAssertions;
using HomeBudget.Common;
using HomeBudget.OperationUseCases;
using HomeBudget.OperationUseCases.Request;
using HomeBudget.OperationUseCases.RequestDto;
using NUnit.Framework;

namespace HomeBudget.OperationTest
{
    [TestFixture]
    public class PositionTest
    {
        private PositionInfoDto _position;
        private PositionInfoDto[] _positions;

        [SetUp]
        public void SetUp()
        {
            OperationEvents.OnPositionInfoBroadcast += OperationEvents_OnPositionInfoBroadcast;
            OperationEvents.OnPositionsInfoBroadcast += OperationEvents_OnPositionsInfoBroadcast;
        }

        [TearDown]
        public void TearDown()
        {
            OperationEvents.OnPositionInfoBroadcast -= OperationEvents_OnPositionInfoBroadcast;
            OperationEvents.OnPositionsInfoBroadcast -= OperationEvents_OnPositionsInfoBroadcast;
        }

        [Test]
        public void CanGetCurrentPosition()
        {
            new GetPositionUseCase(new TestRepository()).Execute(new OperationRequest<DateTime>(DateTime.Now));

            _position.Position.Should().Be(100M);
            _position.Date.Should().Be(DateTime.Now.Date);
        }

        [Test]
        public void CanGetPositionAtDate()
        {
            new GetPositionUseCase(new TestRepository()).Execute(new OperationRequest<DateTime>(DateTime.Now.AddDays(5)));

            _position.Position.Should().Be(50M);
            _position.Date.Should().Be(DateTime.Now.Date.AddDays(5));
        }

        [Test]
        public void CanGetPositionsAtDates()
        {
            new GetPositionsUseCase(new TestRepository()).Execute(new OperationRequest<DateTime>(DateTime.Now.AddDays(5)));

            _positions[0].Position.Should().Be(100M);
            _positions[1].Position.Should().Be(30M);
            _positions[2].Position.Should().Be(50M);
        }
        public class TestRepository : IOperationRepository
        {
            public decimal GetAccountBalance(string accountName)
            {
                throw new NotImplementedException();
            }

            public ITransactionDto[] GetAccountTransactions()
            {
                return new ITransactionDto[]
                {
                    new TestTransaction(100M, DateTime.Now.Date, "Name"),
                    new TestTransaction(-70M, DateTime.Now.AddDays(2).Date, "Name"),
                    new TestTransaction(20M, DateTime.Now.AddDays(4).Date, "Name"),
                };
            }

            public AccountBalanceInfoDto[] GetAccountBalances()
            {
                return new AccountBalanceInfoDto[]{new AccountBalanceInfoDto("Name", 0M), };
            }
        }

        public class TestTransaction : ITransactionDto
        {
            public TestTransaction(decimal value, DateTime date, string accountName)
            {
                AccountName = accountName;
                Date = date;
                Value = value;
            }

            public decimal Value { get; private set; }
            public DateTime Date { get; private set; }
            public string AccountName { get; private set; }
        }

        private void OperationEvents_OnPositionInfoBroadcast(PositionInfoDto positioninfo)
        {
            _position = positioninfo;
        }

        void OperationEvents_OnPositionsInfoBroadcast(PositionInfoDto[] positionInfo)
        {
            _positions = positionInfo;
        }

    }
}