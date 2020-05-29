using DatabaseUtility.API.Models.Membership;
using DatabaseUtility.Models;
using DatabaseUtility.Services;
using System;

namespace DatabaseUtility.API
{
    public static class UserProcesses
    {
        public static void AddAddress(this User user, MembershipService service, AddressCreate address)
        {
            var createAddress = new AddressContent
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                Name = address.Name,
                StateProvinceRegion = address.StateProvinceRegion,
                City = address.City,
                Country = address.Country,
                Postal = address.Postal,
                IsInternational = address.IsInternational,
                Owner = user.ConstuctOwner(address.IsAccountMasterLevel, service)
            };
            var newAddress = service.CreateAddress(createAddress).Result;
            if (address.setAsDefault)
            {
                user.Contents.DefaultAddressIdentifier = newAddress.Contents.Identifier;
                user = service.UpdateUser(user).Result;
            }
        }

        public static void AddCardToken(this User user, MembershipService service, CardTokenCreate cardToken)
        {
            var cardTokenAddress = new CardTokenAddress
            {
                Name = cardToken.CompanyName,
                AddressLine1 = cardToken.AddressAddressLine1,
                AddressLine2 = cardToken.AddressAddressLine2,
                StateProvinceRegion = cardToken.AddressStateProvinceRegion,
                City = cardToken.AddressCity,
                Country = cardToken.AddressCountry,
                Postal = cardToken.AddressPostal
            };
            var createCardToken = new CardTokenContent(cardToken.TokenId)
            {
                Address = cardTokenAddress,
                CardType = cardToken.CardType,
                CustomerId = user.Contents.Identifier.ToString(),
                ExpirationMonth = cardToken.ExpirationMonth,
                ExpirationYear = cardToken.ExpirationYear,
                NameOnCard = cardToken.NameOnCard,
                Email = cardToken.Email,
                LastFourDigits = cardToken.LastFourDigits,
                IsReadonly = cardToken.IsReadonly,
                Owner = user.ConstuctOwner(cardToken.isAccountMasterLevel, service)
            };
            var newCardToken = service.CreateCardToken(createCardToken).Result;
            if (cardToken.setAsDefault)
            {
                user.Contents.DefaultCreditCardPaymentIdentifier = newCardToken.Contents.Identifier;
                user.Contents.UseAccountTermsAsDefaultPayment = false;
                user = service.UpdateUser(user).Result;
            }
        }

        public static void PayWithTerms(this User user, MembershipService service)
        {
            user.Contents.DefaultCreditCardPaymentIdentifier = default(Guid);
            user.Contents.UseAccountTermsAsDefaultPayment = true;
            user = service.UpdateUser(user).Result;
        }

        public static ContactInfoGet GetContactInfo(this User user, MembershipService service)
        {
            var contact = service.GetContact(user.Contents.ContactIdentifier).Result;
            var accountId = user.Contents.AccountIdentifier;
            var account = service.GetAccount(accountId).Result;
            var accountMaster = service.GetAccountMaster(account.Contents.AccountMasterIdentifier).Result;

            return new ContactInfoGet
            {
                FirstName = contact.Contents.FirstName,
                LastName = contact.Contents.LastName,
                EmailAddress = contact.Contents.ContactEmail,
                PhoneNumber = contact.Contents.PhoneNumber,
                Company = accountMaster.Contents.Name
            };
        }

        private static Owner ConstuctOwner(this User user, bool isAccountMaster, MembershipService service)
        {
            Owner owner;
            if (isAccountMaster)
            {
                var accountId = service.GetAccount(user.Contents.AccountIdentifier).Result.Contents.AccountMasterIdentifier;
                var masterAccountId = service.GetAccountMaster(accountId).Result.Contents.Identifier;
                owner = new Owner { Collection = "AccountMaster", Identifier = masterAccountId };
            }
            else
            {
                owner = new Owner { Collection = "User", Identifier = user.Contents.Identifier };
            }
            return owner;
        }
    }
}