namespace PocketMoney.Util
{
    public interface IUrlResolver
    {
        string AreaRoot { get; }

        bool IsLocal(string url);

        string Resolve(string url);
    }
}