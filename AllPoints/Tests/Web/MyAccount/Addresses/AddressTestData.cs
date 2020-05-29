using AllPoints.Features.Models;
using AllPoints.Models;

namespace AllPoints.Features.MyAccount.Addresses
{
    public class AddressViewData : LoginModel
    {
        public string Level { get; set; }
        public AddressModel Address { get; set; }
    }
}