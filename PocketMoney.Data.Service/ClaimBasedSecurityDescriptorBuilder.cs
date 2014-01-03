using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Claims;
using PocketMoney.Data.Security;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Data.Service
{
    internal class ClaimBasedSecurityDescriptorBuilder : ISecurityDescriptorBuilder
    {
        private const string VERSION = "1";
        private readonly List<ClaimsIdentity> claimSet = new List<ClaimsIdentity>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimBasedSecurityDescriptorBuilder"/> class.
        /// </summary>
        /// <param name="currentUser">
        /// The current user.
        /// </param>
        /// <param name="defaultRights">
        /// The default rights.
        /// </param>
        public ClaimBasedSecurityDescriptorBuilder(IUser currentUser, ObjectPermissions defaultRights)
        {
            this.AddSecurityDescriptorVersionClaim();
            if (currentUser == null)
            {
                return;
            }

            var cs =
                new ClaimsIdentity(
                    new[]
                        {
                         new Claim(PocketMoneyClaimTypes.UserId, currentUser.Id.ToString(), ClaimValueTypes.String, Issuer.PocketMoney),
                         new Claim(PocketMoneyClaimTypes.Rights, ((int)defaultRights).ToString(), ClaimValueTypes.Integer, Issuer.PocketMoney),
                     });
            this.claimSet.Add(cs);
            //AddOriginatingCompanyClaim(currentUser.Company);
        }

        private void AddOriginatingCompanyClaim(IFamily family)
        {
            var cs = new ClaimsIdentity
            (new[]
                     {
                         new Claim(PocketMoneyClaimTypes.FamilyId, family.Id.ToString(), ClaimValueTypes.String, Issuer.PocketMoney),
                     });
            claimSet.Add(cs);
        }

        private void AddSecurityDescriptorVersionClaim()
        {
            var cs = new ClaimsIdentity
             (new[]
                     {
                         new Claim(ClaimTypes.Version, VERSION, ClaimValueTypes.Integer, Issuer.PocketMoney),
                     });
            claimSet.Add(cs);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimBasedSecurityDescriptorBuilder"/> class.
        /// </summary>
        /// <param name="currentFamily">
        /// The current company.
        /// </param>
        /// <param name="defaultRights">
        /// The default rights.
        /// </param>
        public ClaimBasedSecurityDescriptorBuilder(IFamily currentFamily, ObjectPermissions defaultRights)
        {
            this.AddSecurityDescriptorVersionClaim();
            if (currentFamily == null)
            {
                return;
            }
            var cs = new ClaimsIdentity
                (new[]
                     {
                         new Claim(PocketMoneyClaimTypes.FamilyId, currentFamily.Id.ToString(), ClaimValueTypes.String, Issuer.PocketMoney),
                         new Claim(PocketMoneyClaimTypes.Rights, ((int) defaultRights).ToString(), ClaimValueTypes.Integer, Issuer.PocketMoney)
                     });
            claimSet.Add(cs);
            this.AddOriginatingCompanyClaim(currentFamily);
        }

        #region ISecurityDescriptorBuilder Members

        public void Add(IRole role, ObjectPermissions rights)
        {
            var cs = new ClaimsIdentity
                (new[]
                     {
                         new Claim(PocketMoneyClaimTypes.Role, role.Id.ToString(), ClaimValueTypes.String, Issuer.PocketMoney),
                         new Claim(PocketMoneyClaimTypes.Rights, ((int) rights).ToString(), ClaimValueTypes.Integer, Issuer.PocketMoney)
                     });
            claimSet.Add(cs);
        }


        public void Add(string emailAddress, ObjectPermissions rights)
        {
            var cs = new ClaimsIdentity
                (new[]
                     {
                         new Claim(PocketMoneyClaimTypes.Email, emailAddress.ToLower(), ClaimValueTypes.String, Issuer.PocketMoney),
                         new Claim(PocketMoneyClaimTypes.Rights, ((int) rights).ToString(), ClaimValueTypes.Integer, Issuer.PocketMoney)
                     });
            claimSet.Add(cs);
        }



        public string Build()
        {
            if (claimSet.Count == 0)
                throw new InvalidOperationException("No claims were added!");
            return claimSet.Serialize();
        }

        #endregion

    }
}
