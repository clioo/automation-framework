using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using System;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class ProductResponse : Product
    {
        public Guid Identifier { get; set; }
        public string UpdatedUtc { get; set; }
    }
}
