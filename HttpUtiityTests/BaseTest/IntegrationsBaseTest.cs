using HttpUtility.Clients;
using HttpUtility.Clients.Contracts;
using System.Threading.Tasks;

namespace HttpUtiityTests.TestBase
{
    public abstract class IntegrationsBaseTest<TEntity>
        where TEntity : class
    {
        protected readonly IIntegrationsWebAppClient Client;

        public IntegrationsBaseTest(string integrationsApiUrl, string tenantExternalId, string tenantInternalId)
        {
            Client = new IntegrationsWebAppClient(FixUrl(integrationsApiUrl), tenantExternalId, tenantInternalId, integrationsApiUrl.Contains("https"));
        }

        private string FixUrl(string url)
        {
            if (url.Contains("https")) return url.Replace("https://", "");
            if (url.Contains("http")) return url.Replace("http://", "");
            return url;
        }

        public abstract Task TestScenarioSetUp(TEntity testData);
        public abstract Task TestScenarioCleanUp(TEntity testData);
    }
}
