using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.CustomerServiceWebApp
{
    //Couldnt get through windows Auth, insted used LoginEndPoint
    public class DashboardEndPoint : EndPoint<object>
    {
        public DashboardEndPoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }

        public async Task<object> GetCookie()
        {
            var response = await GetAll();
            return response;
        }
    }
}