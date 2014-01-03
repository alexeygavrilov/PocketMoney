using PocketMoney.Data;

namespace PocketMoney.Data.Security
{
    public interface IAuthorization
    {
        ISecurityDescriptorBuilder Create(IUser currentUser, ObjectPermissions currentUserRights);
        ISecurityDescriptorBuilder Create(IFamily currentCompany, ObjectPermissions currentCompanyRights);

        /// <summary>
        /// Authorizes or denies requested access to the object
        /// </summary>
        /// <param name="accessRequested">Flags for requested permission</param>
        /// <param name="target">Target object</param>
        /// <exception cref="SecurityException">Throws security exception</exception>
        bool IsAuthorized(ObjectPermissions accessRequested, ISecuredObject target);

        /// <summary>
        /// Gets the access permission for the object
        /// </summary>
        /// <param name="target">Target object</param>
        /// <returns>The access permissions</returns>
        ObjectPermissions GetPermissions(ISecuredObject target);
    }
}