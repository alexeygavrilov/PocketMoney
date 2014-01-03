// -----------------------------------------------------------------------
// <copyright file="IEmailAddress.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PocketMoney.Util.Messaging
{
    public interface IEmailAddress
    {
        string DisplayName { get; }
        string Address { get; }
        string Host { get; }
        string Subdomain { get; }
        string User { get; }
    }
}