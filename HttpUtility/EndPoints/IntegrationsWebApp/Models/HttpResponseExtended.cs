using System.Collections.Generic;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Models
{
    public class HttpResponseExtended<TEntity> : HttpResponse<TEntity>
    {
        public string Description { get; set; }
        public string ErrorMessage { get; set; }
        public List<object> Fields { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
    }
}