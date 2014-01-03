using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Claims;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization;
using PocketMoney.Util.Serialization;

namespace PocketMoney.Data.Security
{
    public enum ClaimsIdentitySerializerType
    {
        Xml,
        Binary
    }

    public interface IClaimsIdentitySerializer
    {
        string Serialize(IEnumerable<ClaimsIdentity> idenityClaims);
        IEnumerable<ClaimsIdentity> Deserialize(string serializedData);
    }

    public static class ClaimsIdentityExtensions
    {
        public const string NAMESPACE = "http://tempuri.org/";


        private static readonly IEnumerable<Type> KnownTypes = new[] { typeof(Claim), typeof(ClaimsIdentity) };



        private class XmlIdentitySerializer : IClaimsIdentitySerializer
        {
            //TODO: DataContractSerializer takes too much space
            private static readonly ISerializer Dcs = new SerializerXml();
            #region Nested type: SerializerXml

            private class SerializerXml : ISerializer
            {
                private static readonly DataContractSerializer Dcs = new DataContractSerializer(typeof(ClaimsIdentity),
                                                                                                KnownTypes, int.MaxValue,
                                                                                                false, true, null);

                #region Implementation of ISerializer

                public void Serialize(Stream stream, object o)
                {
                    Dcs.WriteObject(stream, o);
                }

                public object Deserialize(Stream stream)
                {
                    return Dcs.ReadObject(stream);
                }

                #endregion
            }

            #endregion

            #region Implementation of IClaimsIdentitySerializer
            const string RIGHTS = "Rights";
            public string Serialize(IEnumerable<ClaimsIdentity> idenityClaims)
            {
                XElement element = Serialize(idenityClaims, RIGHTS);
                using (var writer = new StringWriter())
                {
                    element.Save(writer);
                    return writer.ToString();
                }
            }

            private static XElement Serialize(IEnumerable<ClaimsIdentity> claimSets, string rootName)
            {
                XNamespace ns = XNamespace.Get(NAMESPACE);
                return new XElement(ns + rootName,
                                    from cs in claimSets
                                    select Serialize(cs));
            }

            public IEnumerable<ClaimsIdentity> Deserialize(string data)
            {
                using (var ms = new StringReader(data))
                {
                    XElement xe = XElement.Load(ms);
                    foreach (XElement child in xe.Elements())
                        yield return Deserialize(child);
                }
            }

            private static ClaimsIdentity Deserialize(XElement node)
            {
                using (var ms = new MemoryStream())
                {
                    node.Save(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return (ClaimsIdentity)Dcs.Deserialize(ms);
                }
            }

            private static XElement Serialize(ClaimsIdentity set)
            {
                var ms = new MemoryStream();
                Dcs.Serialize(ms, set);
                ms.Seek(0, SeekOrigin.Begin);
                return XElement.Load(new XmlTextReader(ms));
            }

            #endregion
        }

        private class BinaryIdentitySerializer : IClaimsIdentitySerializer
        {
            const int VERSION = 1;
            const long RESERVED = 0;
            #region Implementation of IClaimsIdentitySerializer
            public string Serialize(IEnumerable<ClaimsIdentity> idenityClaims)
            {
                using (var ms = new MemoryStream())
                {
                    using (var binaryWriter = new BinaryWriter(ms))
                    {
                        //Write version
                        binaryWriter.Write(VERSION);
                        //Write reserved
                        binaryWriter.Write(RESERVED);
                        //Write number of claims
                        binaryWriter.Write(idenityClaims.Count());
                        foreach (var claimsIdentity in idenityClaims)
                        {
                            binaryWriter.Write(claimsIdentity.Claims.Count);
                            foreach (var claim in claimsIdentity.Claims)
                            {
                                binaryWriter.Write(claim.ClaimType);
                                binaryWriter.Write(claim.Value);
                                binaryWriter.Write(claim.ValueType);
                                binaryWriter.Write(claim.Issuer);
                            }
                        }
                        binaryWriter.Flush();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }

            public IEnumerable<ClaimsIdentity> Deserialize(string serializedData)
            {
                var result = new List<ClaimsIdentity>();
                if (serializedData == null)
                    throw new ArgumentNullException("serializedData");
                using (var ms = new MemoryStream(Convert.FromBase64String(serializedData)))
                {
                    using (var reader = new BinaryReader(ms))
                    {
                        var version = reader.ReadInt32();
                        if (version != VERSION)
                            throw new SerializationException("ClaimsIdentity can't be deserialized");
                        var reserved = reader.ReadInt64();
                        var idenityClaimsCount = reader.ReadInt32();
                        for (int i = 0; i < idenityClaimsCount; i++)
                        {
                            var claims = new List<Claim>();
                            var claimCount = reader.ReadInt32();
                            for (int j = 0; j < claimCount; j++)
                            {
                                var claimType = reader.ReadString();
                                var claimValue = reader.ReadString();
                                var claimValueType = reader.ReadString();
                                var claimIssuer = reader.ReadString();
                                claims.Add(new Claim(claimType, claimValue, claimValueType, claimIssuer));
                            }
                            result.Add(new ClaimsIdentity(claims));
                        }
                        return result;
                    }
                }
            }

            #endregion
        }




        public static string Serialize(this IEnumerable<ClaimsIdentity> claimSets)
        {
            return new BinaryIdentitySerializer().Serialize(claimSets);
        }

        public static IEnumerable<ClaimsIdentity> Deserialize(string securityDescriptor)
        {
            return new BinaryIdentitySerializer().Deserialize(securityDescriptor);
        }
    }
}
