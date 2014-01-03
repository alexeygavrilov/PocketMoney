using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Data.Security;
using Microsoft.IdentityModel.Claims;
using PocketMoney.Util;

namespace PocketMoney.Data.Service
{
    public class ClaimBasedAuthorization : IAuthorization
    {
        #region IAuthorization Members

        public ISecurityDescriptorBuilder Create(IUser currentUser, ObjectPermissions currentUserRights)
        {
            return new ClaimBasedSecurityDescriptorBuilder(currentUser, currentUserRights);
        }

        public ISecurityDescriptorBuilder Create(IFamily currentFamily, ObjectPermissions currentCompanyRights)
        {
            return new ClaimBasedSecurityDescriptorBuilder(currentFamily, currentCompanyRights);
        }


        public ObjectPermissions GetPermissions(ISecuredObject target)
        {
            IEnumerable<ClaimsIdentity> authorizationClaims =
                ClaimsIdentityExtensions.Deserialize(target.GetSecurityDescriptor());
            var principal = HttpContext.Current != null ? HttpContext.Current.User : Thread.CurrentPrincipal;
            IEnumerable<Claim> principalClaims = principal.Claims();
            return GetEffecitvePermissions(authorizationClaims, principalClaims, principal.Identity.IsAuthenticated);
        }

        public bool IsAuthorized(ObjectPermissions accessRequested, ISecuredObject target)
        {
            IEnumerable<ClaimsIdentity> authorizationClaims =
                ClaimsIdentityExtensions.Deserialize(target.GetSecurityDescriptor());
            var principal = HttpContext.Current != null ? HttpContext.Current.User : Thread.CurrentPrincipal;
            IEnumerable<Claim> principalClaims = principal.Claims();
            if (principalClaims.Count() == 0)
                throw new SecurityException("Invalid current user's permissions");
            ObjectPermissions effectivePermissions = GetEffecitvePermissions(authorizationClaims, principalClaims, principal.Identity.IsAuthenticated);
            return ((effectivePermissions & accessRequested) == accessRequested);
        }


        #endregion

        private static ObjectPermissions GetEffecitvePermissions(IEnumerable<ClaimsIdentity> authorizationClaims,
                                                                 IEnumerable<Claim> principalClaims, bool isAuthenticated)
        {
            //Host administrator has full control regardless the claims
            if ((isAuthenticated) && (principalClaims.Any(x => x.ClaimType == PocketMoneyClaimTypes.Role && x.Value == "Administrator"))) return ObjectPermissions.FullControl;

            ObjectPermissions result = ObjectPermissions.None;
            GetOriginatorRights(authorizationClaims, principalClaims, ref result);
            foreach (ClaimsIdentity authorizationClaim in authorizationClaims)
            {
                //TODO: Claims must be signed with an issuer certificate
                // var permissionclaim = authorizationClaim.Claims.FirstOrDefault(x => x.ClaimType == ASTClaimTypes.Rights && x.Issuer == Issuer.AST);
                //TODO: but for now, dont check it.... 
                Claim permissionclaim =
                    authorizationClaim.Claims.FirstOrDefault(x => x.ClaimType == PocketMoneyClaimTypes.Rights);
                if (permissionclaim == null)
                {
                    continue;
                }
                bool toGrant = false;
                if (authorizationClaim.Claims.Any(x => x.ClaimType != PocketMoneyClaimTypes.Rights))
                    foreach (Claim identityClaim in authorizationClaim.Claims.Where(x => x.ClaimType != PocketMoneyClaimTypes.Rights))
                    {
                        Claim claim = identityClaim;
                        //If we have a claim of specified type and value - we grant permission
                        if (principalClaims.FirstOrDefault(
                                x => (x.ClaimType == claim.ClaimType) && (x.Value.ToLowerInvariant() == claim.Value.ToLowerInvariant())) !=
                            null)
                            toGrant = true;
                    }
                else
                {
                    //If there are no idenity claims - rights are garanted to Everyone
                    toGrant = true;
                }
                //Claims for Everyone should be authorized
                int claimValue;
                if ((toGrant) && (int.TryParse(permissionclaim.Value, out claimValue)))
                    result |= (ObjectPermissions)claimValue;
            }
            return result;
        }

        private static void GetOriginatorRights(IEnumerable<ClaimsIdentity> authorizationClaims, IEnumerable<Claim> principalClaims, ref ObjectPermissions result)
        {
            var originatorFamilyId = authorizationClaims.SelectMany(x => x.Claims).Where(x => x.ClaimType == PocketMoneyClaimTypes.OriginatorFamilyId).FirstOrDefault();
            if (originatorFamilyId == null) return; //Exit if there is no claim originator company profile id.
            //Company profile part
            var principalsFamily = principalClaims.FirstOrDefault(c => c.ClaimType == PocketMoneyClaimTypes.FamilyId);
            if (principalsFamily == null) return;
            try
            {
                var origGuid = Guid.Parse(originatorFamilyId.Value);
                var principlaCompanyGuid = Guid.Parse(principalsFamily.Value);
                if (origGuid != principlaCompanyGuid) return; //Principal and orignator are in different companies - do nothing...
                result |= ObjectPermissions.Read; //Family users would get read permission
                //Check if user is the company admin
                if (principalClaims.Any(c => c.ClaimType == PocketMoneyClaimTypes.Role && c.Value == "Administrator"))
                    result = ObjectPermissions.FullControl;

            }
            catch (Exception ex)
            {
                ex.LogError();
            }

        }
    }
}
