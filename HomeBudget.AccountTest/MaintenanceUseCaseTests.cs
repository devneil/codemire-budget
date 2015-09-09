using NUnit.Framework;

namespace HomeBudget.RequestTest
{
    [TestFixture]
    public class MaintenanceUseCaseTests
    {
        protected TestAccountsRepository Repos;

        [SetUp]
        public void SetUp()
        {
            Repos = new TestAccountsRepository();
        }
    }
}