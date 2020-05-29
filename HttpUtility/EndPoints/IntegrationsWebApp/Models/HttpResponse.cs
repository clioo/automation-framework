using Dfsi.Framework;
using System.Collections.Generic;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Models
{
    public class HttpResponse<TEntity>
    {
        public TEntity Result { get; set; }
        private List<ValidationError> ValidationErrors { get; set; }
        public object Errors { get; set; }
        public object ModelState { get; set; }
        public string Message { get; set; }
    }
}