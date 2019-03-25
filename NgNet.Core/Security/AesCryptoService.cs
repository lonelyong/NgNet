using System.Security.Cryptography;

namespace NgNet.Security
{
    #region <class - AesEncrypt>
    public class AesCryptoService
    {
        private Aes aes;

        public AesCryptoService()
        {
            aes = Aes.Create();

        }
    }
    #endregion
}
