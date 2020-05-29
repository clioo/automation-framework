using Dfsi.Framework;
using System;
using System.Collections.Generic;

namespace HttpUtility.EndPoints
{
    /// <summary>
    /// Non Standard way to get the response from the API. Getting all posible values in current implementation
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    // TODO: Create good Response Variables
    public class HttpResponse
    {
        public object Entity { get; set; }

        public List<ValidationError> Errors { get; set; }
        public string ErrorMessage { get; set; }
        public List<object> Fields { get; set; }

        public Exception Exception { get; set; }

        public bool IsValid { get; set; }
    }
}