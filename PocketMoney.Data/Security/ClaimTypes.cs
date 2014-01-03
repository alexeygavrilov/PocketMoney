using Microsoft.IdentityModel.Claims;

namespace PocketMoney.Data.Security
{
    public static class PocketMoneyClaimTypes
    {
        #region Identity Model ClaimTypes

        public static string UserName
        {
            get { return ClaimTypes.Name; }
        }

        public static string Role
        {
            get { return ClaimTypes.Role; }
        }

        public static string Email
        {
            get { return ClaimTypes.Email; }
        }

        public static string FirstName
        {
            get { return ClaimTypes.GivenName; }
        }

        public static string LastName
        {
            get { return ClaimTypes.Surname; }
        }

        #endregion

        #region Additional ClaimTypes

        public static string LastLoginDate
        {
            get { return "/PocketMoney/LastLoginDate"; }
        }

        public static string UserId
        {
            get { return "/PocketMoney/UserId"; }
        }

        public static string ProviderUserKey
        {
            get { return "/PocketMoney/ProviderUserKey"; }
        }

        public static string JobTitle
        {
            get { return "/PocketMoney/JobTitle"; }
        }

        public static string OriginatorFamilyId
        {
            get { return "/PocketMoney/OCId"; }
        }

        public static string FamilyId
        {
            get { return "/PocketMoney/FamilyId"; }
        }

        public static string Family
        {
            get { return "/PocketMoney/Family"; }
        }

        public static string City
        {
            get { return "/PocketMoney/City"; }
        }

        public static string State
        {
            get { return "/PocketMoney/State"; }
        }

        public static string Zip
        {
            get { return "/PocketMoney/Zip"; }
        }

        public static string Country
        {
            get { return "/PocketMoney/Country"; }
        }

        public static string CompanyType
        {
            get { return "/PocketMoney/CompanyType"; }
        }

        
        public static string Rights
        {
            get { return "/PocketMoney/Rights"; }
        }

        public static string UserProfile
        {
            get { return "/PocketMoney/UserProfile"; }
        }

        #endregion
    }
}
