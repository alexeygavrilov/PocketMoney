using PocketMoney.Data;

namespace PocketMoney.Data.Security
{
    using System;

    public interface ISecurityDescriptorBuilder
    {
        /// <summary>
        /// This is the main method which needs to be used to grant permissions to the object. Use StandardRoles class to specify roles of user in the receiving(authorized) company.
        /// </summary>
        /// <param name="role">Authorized role</param>
        /// <param name="rights">Permissions</param>
        void Add(IRole role, ObjectPermissions rights);
        [Obsolete("Please use this method ONLY if user does not have profile on PocketMoney.  Otherwise please use Add(IRole role, ObjectPermissions rights) method")]
        void Add(string emailAddress, ObjectPermissions rights);
        string Build();
    }
}