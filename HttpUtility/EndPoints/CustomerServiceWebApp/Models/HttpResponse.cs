using Dfsi.Framework;
using System;
using System.Collections.Generic;

namespace HttpUtility.EndPoints.CustomerServiceWebApp.Models
{
    public class HttpResponse<TEntity>
    {
        public TEntity Result { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public int Status { get; set; }
        public Exception Exception { get; set; }
    }
}