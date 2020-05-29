using System.Threading.Tasks;
using HttpUtility.Clients;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpUtiityTests.Services
{
    [TestClass]
    public class HttpActionTests
    {
        [TestMethod]
        public async Task CreateProductAsyncRaw()
        {
            IntegrationsWebAppClient client = new IntegrationsWebAppClient("api.eovportal-softtek.com/api/V2", "AllPoints");
            var action = new PostProductRequest { Name = "Product Name", Identifier = "MyExternalId1234", ExternalIdentifier = "MyExternalId1234" };
            var revert = new DeleteProductRequest { ExternalIdentifier = action.Identifier };
            var test = new HttpAction<PostProductRequest, GetProductRequest, DeleteProductRequest, ProductResponse>(client.Products.GetByExternalIdentifier, client.Products.Create, client.Products.Remove);
            await test.BeginExecute(action, new GetProductRequest { ExternalIdentifier = action.Identifier });
            await test.BeginRevert(revert, new GetProductRequest { ExternalIdentifier = action.Identifier });
            var wasReverted = test.Confirm();
        }

        [TestMethod]
        public async Task CreateProductAsyncMediumRaw()
        {
            IntegrationsWebAppClient client = new IntegrationsWebAppClient("api.eovportal-softtek.com/api/V2", "AllPoints");
            var action = new PostProductRequest { Name = "Product Name", Identifier = "MyExternalId12345", ExternalIdentifier = "MyExternalId12345" };
            var revert = new DeleteProductRequest { ExternalIdentifier = action.Identifier };
            var test = new CreateProductHttpAction(client.Products.GetByExternalIdentifier, client.Products.Create, client.Products.Remove, action);
            await test.BeginExecute();
            await test.BeginRevert();
            var wasReverted = test.Confirm();
            Assert.IsTrue(wasReverted);
        }
    }
}
