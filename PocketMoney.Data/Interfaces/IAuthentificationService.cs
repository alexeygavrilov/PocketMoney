using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using PocketMoney.Data;
using PocketMoney.Data.Security;
using Microsoft.IdentityModel.Claims;

namespace PocketMoney.Data.Security
{
    public interface IAuthentificationService
    {
        bool ValidateUser(string userName, string password);
        //bool ValidateUser(X509Certificate2 clientCertificate, out string userName);
        IEnumerable<IRole> GetRoles(string userName);
        List<Claim> GetClaims(IClaimsPrincipal principal, RequestDetails requestDetails);
        List<string> GetSupportedClaimTypes();
    }
}
