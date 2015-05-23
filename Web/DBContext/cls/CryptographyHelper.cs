using System;
using System.Collections.Generic;

using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace t
{
    /// <summary>
    /// Some ultis methods
    /// </summary>
    public class CryptographyHelper
    {
        #region Certificate  
        public static List<X509Certificate2> GetCertificates()
        {
            List<X509Certificate2> l = new List<X509Certificate2>();
            X509Store oStore;
            oStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            oStore.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 x509 in oStore.Certificates)
            {
                string na = x509.SubjectName.Name.ToString();
                if (na != "")
                {
                    l.Add(x509);
                }
            }
            return l;
        }

        public static List<String> GetX509CertificatedNames()
        {
            List<string> l = new List<string>();
            X509Store oStore;
            oStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            oStore.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 x509 in oStore.Certificates)
            {
                string na = x509.SubjectName.Name.ToString();
                if (na != "")
                {
                    l.Add(na);
                }
            }
            return l;
        }
        public static X509Certificate2 GetStoreX509Certificate2(string name)
        {
            X509Certificate2 oX509;
            oX509 = null;
            X509Store oStore;
            oStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            oStore.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 x509 in oStore.Certificates)
            {
                if (x509.SubjectName.Name.ToString() == name)
                {
                    oX509 = x509;
                    break;
                }
            }
            return oX509;
        }
        public static X509Certificate2 GetStoreX509Certificate2BySerial(string strSerialNumber)
        {
            X509Certificate2 oX509;
            oX509 = null;
            X509Store oStore;
            oStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            oStore.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 x509 in oStore.Certificates)
            {
                if (x509.SerialNumber == strSerialNumber)
                {
                    oX509 = x509;
                    break;
                }
            }
            return oX509;
        }        
        #endregion

        #region HashAlgorithm
        public static HashAlgorithm GetHashAlgorithm(HashAlgorithmName name)
        {
            switch (name)
            {
                case HashAlgorithmName.MD5:
                    return new MD5CryptoServiceProvider();
                case HashAlgorithmName.SHA1:
                    return new SHA1Managed();
            }
            throw new Exception("Unknown hash algorithm!");
        }

        public static HashAlgorithm GetHashAlgorithm(X509Certificate2 certificate)
        {
            if (certificate.SignatureAlgorithm.FriendlyName.ToUpper().Contains("MD5"))
            {
                return GetHashAlgorithm(HashAlgorithmName.MD5);
            }
            if (certificate.SignatureAlgorithm.FriendlyName.ToUpper().Contains("SHA1"))
            {
                return GetHashAlgorithm(HashAlgorithmName.SHA1);
            }
            throw new Exception("Unknown hash algorithm!");
        }

        public static HashAlgorithm GetHashAlgorithm(string certificate)
        {
            X509Certificate2 cert = new X509Certificate2();
            cert.Import(certificate);
            return GetHashAlgorithm(cert);
        }

        public static string GetHashAlgorithmName(X509Certificate2 certificate)
        {
            if (certificate.SignatureAlgorithm.FriendlyName.ToUpper().Contains("MD5"))
            {
                return HashAlgorithmName.MD5.ToString();
            }
            if (certificate.SignatureAlgorithm.FriendlyName.ToUpper().Contains("SHA1"))
            {
                return HashAlgorithmName.SHA1.ToString();
            }
            throw new Exception("Unknown hash algorithm!");
        }
        #endregion

        #region Verify & Sign      
        public static String SignHash(string data, HashAlgorithmName algorithmName, X509Certificate2 cert, OutputDataType datatype)
        {
            byte[] bData, bHash, bResult;
            HashAlgorithm ha = GetHashAlgorithm(algorithmName);
            bData = UnicodeEncoding.UTF8.GetBytes(data);
            bHash = ha.ComputeHash(bData);
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PrivateKey;
            bResult = rsa.SignHash(bHash, CryptoConfig.MapNameToOID(algorithmName.ToString()));
            return ToString(bResult, datatype);
        }
        #endregion

        #region Convert
        public static string ToBase64String(byte[] r)
        {
            return Convert.ToBase64String(r);
        }

        public static string ToHexString(byte[] r)
        {
            string s = string.Empty;
            for (int i = 0; i < r.Length; i++)
            {
                s = s + r[i].ToString("X2");
            }
            return s;
        }
        public static string ToString(byte[] r, OutputDataType type)
        {
            switch (type)
            {
                case OutputDataType.Base64:
                    return ToBase64String(r);
                case OutputDataType.Hex:
                    return ToHexString(r);
            }
            throw new Exception("Unknown output type!");
        }
        #endregion
    }
}
