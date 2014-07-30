using System;
using System.Security;
using UmbracoDiff.Extensions;

namespace UmbracoDiff.Services
{
    // adapted from: http://weblogs.asp.net/jongalloway/encrypting-passwords-in-a-net-app-config-file
    public class EncryptionService : IEncryptionService
    {
        static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Salt Is Not A Password");

        public string Encrypt(string source)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(source),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedData);
        }

        public string Decrypt(string encryptedSource)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedSource),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);

                var secureSource = ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
                var insecureSource = secureSource.ConvertToUnsecureString();

                return insecureSource;
            }
            catch
            {
                return string.Empty;
            }
        }

        private string ToInsecureString(SecureString source)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(source);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

        private SecureString ToSecureString(string source)
        {
            var secure = new SecureString();
            foreach (char c in source)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }
    }
}
