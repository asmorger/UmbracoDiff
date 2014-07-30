using System.Security;

namespace UmbracoDiff.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string source);

        string Decrypt(string encryptedSource);
    }
}
