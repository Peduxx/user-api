using Application.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Password
{
    public sealed class PasswordProvider : IPasswordProvider
    {
        public byte[] GenerateSalt()
        {
            // Gera um salt aleatório usando RNGCryptoServiceProvider
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public string Generate(string password, byte[] salt)
        {
            // Concatena o salt com a senha
            byte[] saltedPassword = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();

            // Calcula o hash SHA-256
            byte[] hashBytes = SHA256.HashData(saltedPassword);
            return Convert.ToBase64String(hashBytes);
        }

        public bool Compare(string password, string storagedPassword, byte[] salt)
        {
            // Calculando o hash da senha fornecida com o mesmo salt
            string hashedPassword = Generate(password, salt);

            // Verificando se os hashes coincidem
            if (hashedPassword != storagedPassword)
                return false;

            return true;
        }
    }
}
