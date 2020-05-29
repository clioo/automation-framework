using HttpUtility.EndPoints.Base.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Contacts;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface IContactsEndpoint : ICrudEndpoint<ContactRequest, HttpResponseExtended<ContactResponse>>
    {
    }
}
