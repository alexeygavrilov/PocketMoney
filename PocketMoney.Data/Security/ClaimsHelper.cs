using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.IdentityModel.Claims;
using System;
using System.IdentityModel.Tokens;

namespace PocketMoney.Data.Security
{
    public static class ClaimsHelper
    {
        public static IEnumerable<Claim> Claims(this IPrincipal principal)
        {
            return principal.ClaimsPrincipal().Identities.SelectMany(identity => identity.Claims());
        }

        public static IClaimsPrincipal ClaimsPrincipal(this IPrincipal principal)
        {
            if (principal is IClaimsPrincipal)
                return (IClaimsPrincipal)principal;
            return new DummyClaimsPrincipal(principal);
        }


        public static IEnumerable<Claim> Claims(this IIdentity identity)
        {
            return identity.ClaimsIdentity().Claims;
        }

        public static IClaimsIdentity ClaimsIdentity(this IIdentity identity)
        {
            var ci = identity as IClaimsIdentity;
            if (ci != null) return ci;
            return new DummyClaimsIdentity(identity);
        }

        private class DummyClaimsPrincipal : IClaimsPrincipal
        {
            private readonly IPrincipal _principal;
            private readonly ClaimsIdentityCollection _identities = new ClaimsIdentityCollection();
            public DummyClaimsPrincipal(IPrincipal principal)
            {
                _principal = principal;
                var ci = _principal.Identity as IClaimsIdentity;
                this._identities.Add(ci ?? new DummyClaimsIdentity(this._principal.Identity));
            }

            #region Implementation of IPrincipal

            public bool IsInRole(string role)
            {
                return _principal.IsInRole(role);
            }

            public IIdentity Identity
            {
                get
                {
                    return this._principal.Identity;
                }
            }

            #endregion

            #region Implementation of IClaimsPrincipal

            public IClaimsPrincipal Copy()
            {
                return (IClaimsPrincipal)this.MemberwiseClone();
            }

            public ClaimsIdentityCollection Identities
            {
                get
                {
                    return this._identities;
                }
            }

            #endregion
        }
        private class DummyClaimsIdentity : IClaimsIdentity
        {
            private readonly IIdentity _identity;

            private ClaimCollection _claims;

            private IClaimsIdentity _actor;

            private string _label;

            private string _nameClaimType;

            private string _roleClaimType;

            private SecurityToken _bootstrapToken;

            /// <summary>
            /// Gets the name of the current user.
            /// </summary>
            /// <returns>
            /// The name of the user on whose behalf the code is running.
            /// </returns>
            public string Name
            {
                get
                {
                    return this._identity.Name;
                }
            }

            /// <summary>
            /// Gets the type of authentication used.
            /// </summary>
            /// <returns>
            /// The type of authentication used to identify the user.
            /// </returns>
            public string AuthenticationType
            {
                get
                {
                    return this._identity.AuthenticationType;
                }
            }

            /// <summary>
            /// Gets a value that indicates whether the user has been authenticated.
            /// </summary>
            /// <returns>
            /// true if the user was authenticated; otherwise, false.
            /// </returns>
            public bool IsAuthenticated
            {
                get
                {
                    return this._identity.IsAuthenticated;
                }
            }

            public DummyClaimsIdentity(IIdentity identity)
            {
                _identity = identity;
                _claims = new ClaimCollection(this);
            }

            #region Implementation of IClaimsIdentity

            public IClaimsIdentity Copy()
            {
                return (IClaimsIdentity)this.MemberwiseClone();
            }

            public ClaimCollection Claims
            {
                get
                {
                    return this._claims;
                }
            }

            public IClaimsIdentity Actor
            {
                get
                {
                    return this._actor;
                }
                set
                {
                    this._actor = value;
                }
            }

            public string Label
            {
                get
                {
                    return this._label;
                }
                set
                {
                    this._label = value;
                }
            }

            public string NameClaimType
            {
                get
                {
                    return this._nameClaimType;
                }
                set
                {
                    this._nameClaimType = value;
                }
            }

            public string RoleClaimType
            {
                get
                {
                    return this._roleClaimType;
                }
                set
                {
                    this._roleClaimType = value;
                }
            }

            public SecurityToken BootstrapToken
            {
                get
                {
                    return this._bootstrapToken;
                }
                set
                {
                    this._bootstrapToken = value;
                }
            }

            #endregion
        }
    }
    
}
