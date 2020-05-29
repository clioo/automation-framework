using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Contacts;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class ContactsEndpoint : EndpointBase<ContactRequest, HttpResponseExtended<ContactResponse>>, IContactsEndpoint
    {
        public ContactsEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }
    }
}
