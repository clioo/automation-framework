using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using System.Threading.Tasks;

namespace HttpUtility.Services
{
    public class HttpAction<TActionRequest, TGetRequest, TRevertRequest, TModel>
        where TActionRequest : EntityBase
        where TGetRequest : EntityBase
        where TRevertRequest : EntityBase
        where TModel : class
    {
        public TModel InitialVal { get; set; }
        public TModel FinalVal { get; set; }

        public delegate Task<HttpResponseExtended<TModel>> GetAction(TGetRequest getRequest);
        public delegate Task<HttpResponseExtended<TModel>> ExecuteAction(TActionRequest createRequest);
        public delegate Task<HttpResponseExtended<TModel>> RevertAction(TRevertRequest deleteRequest);

        public GetAction Get { get; set; }
        public ExecuteAction Execute { get; set; }
        public RevertAction Revert { get; set; }

        public HttpAction(GetAction get, ExecuteAction execute, RevertAction revert)
        {
            Get = get;
            Execute = execute;
            Revert = revert;
        }

        public virtual async Task BeginExecute(TActionRequest request, TGetRequest get)
        {
            var getResponse = await Get(get);
            InitialVal = getResponse.Result;

            await Execute(request);
        }

        public virtual async Task BeginRevert(TRevertRequest request, TGetRequest get)
        {
            await Revert(request);

            var getResponse = await Get(get);
            FinalVal = getResponse.Result;
        }

        public virtual bool Confirm()
        {
            if (FinalVal == null && InitialVal == null)
            {
                return true;
            }
            else if (FinalVal != null && InitialVal != null)
            {
                return FinalVal.Equals(InitialVal);
            }
            else
            {
                return false;
            }
        }
    }

    public class CreateProductHttpAction : HttpAction<PostProductRequest, GetProductRequest, DeleteProductRequest, ProductResponse>
    {
        public PostProductRequest CreateRequest;
        public GetProductRequest GetRequest;
        public DeleteProductRequest DeleteRequest;

        public CreateProductHttpAction(GetAction get, ExecuteAction execute, RevertAction revert, PostProductRequest createProductRequest) : base(get, execute, revert)
        {
            CreateRequest = createProductRequest;
            GetRequest = new GetProductRequest { ExternalIdentifier = createProductRequest.ExternalIdentifier };
            DeleteRequest = new DeleteProductRequest { ExternalIdentifier = createProductRequest.ExternalIdentifier };
        }

        public async Task BeginExecute()
        {
            var getResponse = await Get(GetRequest);
            InitialVal = getResponse.Result;

            await Execute(CreateRequest);
        }

        public async Task BeginRevert()
        {
            await Revert(DeleteRequest);

            var getResponse = await Get(GetRequest);
            FinalVal = getResponse.Result;
        }
    }
}
