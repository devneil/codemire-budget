using FluentAssertions;
using HomeBudget.MaintenanceUseCases;
using HomeBudget.MaintenanceUseCases.Request;
using HomeBudget.MaintenanceUseCases.RequestDto;
using NUnit.Framework;

namespace HomeBudget.RequestTest
{
    [TestFixture]
    public class RemoveAccountUseCaseTests : MaintenanceUseCaseTests
    {
        private RemoveAccountUseCase _useCase;
        private AccountInfoDto _removeAccountReceived;

        [SetUp]
        public void RemoveAccountSetUp()
        {
            _useCase = new RemoveAccountUseCase(Repos);
            MaintenanceEvents.OnRemoveAccountBroadcast += MaintenanceEvents_OnRemoveAccountBroadcast;
        }

        [TearDown]
        public void TearDown()
        {
            MaintenanceEvents.OnRemoveAccountBroadcast -= MaintenanceEvents_OnRemoveAccountBroadcast;
        }
        
        [Test]
        public void RemoveAccount()
        {
            var req = new MaintenanceRequest<string>("thisAccount");
            _useCase = new RemoveAccountUseCase(Repos);

            _useCase.Execute(req);

            Repos.RemoveAccountCalled.Should().BeTrue();
            Repos.RemoveAccountName.Should().Be("thisAccount");
            _removeAccountReceived.Name.Should().Be("thisAccount");
        }
    
        void MaintenanceEvents_OnRemoveAccountBroadcast(AccountInfoDto accountInfo)
        {
            _removeAccountReceived = accountInfo;
        }
    }
}