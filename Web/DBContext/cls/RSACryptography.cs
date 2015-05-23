using System;
using System.Collections.Generic;

using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace t
{
    /// <summary>
    /// Presentaion a RSA cryptography helper base on certificate
    /// </summary>
    public class RSACryptography
    {
        X509Certificate2 cert;

        #region Contructor
        /// <summary>
        /// Create new RSACryptography from base64 string
        /// </summary>
        /// <param name="certificated"></param>
        public RSACryptography(string certificated)
        {
            if (String.IsNullOrEmpty(certificated))
                throw new CryptographicException("Thông tư chứng thư cần khác empty và null.");
            byte[] b = Convert.FromBase64String(certificated);
            cert = new X509Certificate2();
            cert.Import(b);
            ValidateCert();
        }
        /// <summary>
        /// Create new RSACryptography from X509Certificate2 
        /// </summary>
        /// <param name="certificated"></param>
        public RSACryptography(X509Certificate2 certificated)
        {
            if (certificated == null)
                throw new CryptographicException("Thông tư chứng thư cần khác empty và null.");
            cert = certificated;
            //ValidateCert();
        }

        /// <summary>
        /// Create new RSACryptography from byte array
        /// </summary>
        /// <param name="certificated"></param>
        public RSACryptography(byte[] certificated)
        {
            if (certificated == null)
                throw new CryptographicException("Thông tư chứng thư cần khác empty và null.");
            cert = new X509Certificate2();
            cert.Import(certificated);
            ValidateCert();
        }
        #endregion

        #region HashAlgorithm
        /// <summary>
        /// Get HashAlgorithm name which is specified in Certification
        /// </summary>
        /// <returns>return hash algorithm which is specify in certificate</returns>
        private string GetHashAlgorithmName()
        {
            if (cert.SignatureAlgorithm.FriendlyName.ToUpper().Contains("MD5"))
            {
                return HashAlgorithmName.MD5.ToString();
            }
            if (cert.SignatureAlgorithm.FriendlyName.ToUpper().Contains("SHA1"))
            {
                return HashAlgorithmName.SHA1.ToString();
            }
            return String.Empty;
        }
        /// <summary>
        /// Get HashAlgorithm with specify name
        /// </summary>
        /// <param name="name">name of hash algorithm</param>
        /// <returns></returns>
        private HashAlgorithm GetHashAlgorithm(HashAlgorithmName name)
        {
            switch (name)
            {
                case HashAlgorithmName.MD5:
                    return new MD5CryptoServiceProvider();
                case HashAlgorithmName.SHA1:
                    return new SHA1Managed();
                case HashAlgorithmName.SHA256:
                    return new SHA256Managed();
                case HashAlgorithmName.SHA384:
                    return new SHA384Managed();
                case HashAlgorithmName.SHA512:
                    return new SHA512Managed();               
            }
            throw new CryptographicException("Unknown hash algorithm!");
        }
        /// <summary>
        /// Get HashAlgorithm which is supported in cerificate
        /// </summary>
        /// <returns></returns>
        private HashAlgorithm GetHashAlgorithm()
        {
            if (cert.SignatureAlgorithm.FriendlyName.ToUpper().Contains("MD5"))
            {
                return GetHashAlgorithm(HashAlgorithmName.MD5);
            }
            if (cert.SignatureAlgorithm.FriendlyName.ToUpper().Contains("SHA1"))
            {
                return GetHashAlgorithm(HashAlgorithmName.SHA1);
            }
            if (cert.SignatureAlgorithm.FriendlyName.ToUpper().Contains("SHA256"))
            {
                return GetHashAlgorithm(HashAlgorithmName.SHA256);
            }
            if (cert.SignatureAlgorithm.FriendlyName.ToUpper().Contains("SHA384"))
            {
                return GetHashAlgorithm(HashAlgorithmName.SHA384);
            }
            if (cert.SignatureAlgorithm.FriendlyName.ToUpper().Contains("SHA512"))
            {
                return GetHashAlgorithm(HashAlgorithmName.SHA512);
            }
            throw new CryptographicException("Unknown hash algorithm!");
        }
        #endregion

        #region Sign & Verify
        /// <summary>
        /// Create hash data from UTF8 string and return signed data in base64 string 
        /// </summary>
        /// <param name="data">UTF8 data</param>
        /// <returns>Base 64 string</returns>
        public string SignHash(string data)
        {
            byte[] bData = UnicodeEncoding.UTF8.GetBytes(data);
            HashAlgorithm h = this.GetHashAlgorithm();
            byte[] ret = SignHash(h.ComputeHash(bData));
            return Convert.ToBase64String(ret);
        }
        /// <summary>
        /// Return signed hash data
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>Return signed hash data</returns>
        public byte[] SignHash(byte[] data)
        {
            if (!cert.HasPrivateKey)
            {
                throw new CryptographicException("Không có khóa bí mật trong Certificate!");
            }
            RSACryptoServiceProvider rsa = cert.PrivateKey as RSACryptoServiceProvider;
            return rsa.SignHash(data, CryptoConfig.MapNameToOID(this.GetHashAlgorithmName()));
        }

      
        #endregion

        #region Certificate information
        /// <summary>
        /// Serial number of usb token
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return cert.SerialNumber;
            }
        }

        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer
        {
            get
            {
                return cert.Issuer;
            }
        }
        /// <summary>
        /// Return the time which certificate is no longer valid
        /// </summary>
        public DateTime NotAfter
        {
            get
            {
                return cert.NotAfter;
            }
        }
        /// <summary>
        ///  Return the time which certificate becomes valid
        /// </summary>
        public DateTime NotBefore
        {
            get
            {
                return cert.NotBefore;
            }
        }
        /// <summary>
        /// Return base64 string present rawdata of certification
        /// </summary>
        public string RawData
        {
            get
            {
                return Convert.ToBase64String(cert.RawData);
            }
        }
        /// <summary>
        /// Validate certificate
        /// </summary>
        private void ValidateCert()
        {
            DateTime now = DateTime.Now;
            if (now > NotAfter || now < NotBefore)
                throw new CryptographicException("Certificate không còn giá trị để sử dụng");
            string serialNumber = this.SerialNumber;
            //TODO:Call method of sevices provider with serial number id in usb token
        }
        #endregion
    }
}
