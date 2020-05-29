using HttpUtility.EndPoints.IntegrationsWebApp.Models;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class PostProductRequest : Product
    {
        //external identifier
        public string Identifier { get; set; }
    }
}
