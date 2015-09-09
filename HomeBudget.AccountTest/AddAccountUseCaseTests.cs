using System.Collections.Generic;
using FluentAssertions;
using HomeBudget.Common;
using HomeBudget.MaintenanceUseCases;
using HomeBudget.MaintenanceUseCases.Request;
using HomeBudget.MaintenanceUseCases.RequestDto;
using NUnit.Framework;

namespace HomeBudget.RequestTest
{
    [TestFixture]
    public class AddAccountUseCaseTests : MaintenanceUseCaseTests
    {
        private AddAccountUseCase _useCase;
        private AccountInfoDto _accountEventReceived;

        [SetUp]
        public void AddAccountSetUp()
        {
            _useCase = new AddAccountUseCase(Repos);
            MaintenanceEvents.OnAccountInfoBroadcast += MaintenanceEvents_OnAccountInfoBroadcast;
        }

        [TearDown]
        public void TearDown()
        {
            MaintenanceEvents.OnAccountInfoBroadcast -= MaintenanceEvents_OnAccountInfoBroadcast;
        }
        
        [Test]
        public void NewAccount_NoNameNoBalance_AddsDefaultNameZeroBalance()
        {
            IRequest req = new MaintenanceRequest<AccountDto>(null);
            _useCase.Execute(req);

            ConfirmValues("Account1", 0M);
        }

        [Test]
        public void NewAccount_WithName_AddsAccountWithName()
        {
            var acc = new AccountDto("AccountName", 0M);
            IRequest req = new MaintenanceRequest<AccountDto>(acc);
            _useCase.Execute(req);

            ConfirmValues("AccountName", 0M);
        }

        [Test]
        public void NewAccount_WithBalance_AddsAccountWithBalance()
        {
            var acc = new AccountDto(null, 100M);
            IRequest req = new MaintenanceRequest<AccountDto>(acc);
            _useCase.Execute(req);

            ConfirmValues("Account1", 100M);
        }

        [Test]
        public void NewAccount_WithSameName_AddsIncrementerToName()
        {
            var req = new MaintenanceRequest<AccountDto>(null);
            Repos.SetNumAccounts(1);
            _useCase.Execute(req);

            ConfirmValues("Account1_1", 0M);
        }

        private void ConfirmValues(string accName, decimal accBalance)
        {
            Repos.AddAccountCalled.Should().BeTrue();
            Repos.AddAccountName.Should().Be(accName);
            Repos.AddAccountBalance.Should().Be(accBalance);
            _accountEventReceived.Should().NotBeNull();
            _accountEventReceived.Name.Should().Be(accName);
            _accountEventReceived.Balance.Should().Be(accBalance);
        }

        void MaintenanceEvents_OnAccountInfoBroadcast(AccountInfoDto accountInfo)
        {
            _accountEventReceived = accountInfo;
        }

    }
}