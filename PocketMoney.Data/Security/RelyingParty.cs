
using System;
using System.Security.Cryptography.X509Certificates;


namespace PocketMoney.Data.Security
{
    public class RelyingParty
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Uri Realm { get; set; }
        public Uri ReplyTo { get; set; }
        public X509Certificate2 EncryptingCertificate { get; set; }
        public byte[] SymmetricSigningKey { get; set; }
    }
}
