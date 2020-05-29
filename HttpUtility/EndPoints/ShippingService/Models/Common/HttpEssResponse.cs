using System.Collections.Generic;

namespace HttpUtility.EndPoints.ShippingService.Models
{
    public class HttpEssResponse<TEntity>
    {
        public string Description { get; set; }
        public string ErrorMessage { get; set; }
        public List<object> Fields { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string TraceId { get; set; }
        public TEntity Result { get; set; }
    }
}