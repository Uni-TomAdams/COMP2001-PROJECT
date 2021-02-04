using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace COMP2001_Authentication_Application.Helpers
{
    public class HashingHelper
    {
        private static readonly string FixedSalt = "uE/McVkR4Qltg0XhkHgtJQ==";

        /// <summary>
        ///     This hashing function uses the PBKDF2 (RFC 2898) algorithm using SHA256 hashing function based on 10000 iterations
        ///     the storage format will also hold the salt and iteration length. $"{iterations}.{salt}.{hash}". This is an identity framework
        ///     hashing method.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            byte[] PasswordByte = Encoding.ASCII.GetBytes(password);
            byte[] SaltByte =  Encoding.ASCII.GetBytes(FixedSalt);

            using (var algorithm = new Rfc2898DeriveBytes(PasswordByte, SaltByte, 10000, HashAlgorithmName.SHA256))
            {
                // Key is 32 bytes.
                var key = Convert.ToBase64String(algorithm.GetBytes(32));
                // Salt is 16 bytes
                var salt = FixedSalt;

                // Return hashing password format
                return $"{10000}.{salt}.{key}";
            }
        }

        /// <summary>
        ///     This verification method will decode the database password into its 3 core parts. It will then
        ///     check this against the passed in password hash, and do a byte sequence check.
        /// </summary>
        /// <param name="hash">Database password</param>
        /// <param name="password">Passed in password</param>
        /// <returns></returns>
        public static bool VerifyHash(string hash, string password)
        {
            // Split hash into its 3 parts
            var parts = hash.Split('.', 3);

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(FixedSalt);
            var key = Convert.FromBase64String(parts[2]);

            // Verify request password against database password hashes
            using (var algorithm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(32);

                var verified = keyToCheck.SequenceEqual(key);

                return verified;
            }
        }

        public static string GetSaltFromHashFormat(string hash)
        {
            // Split hash format into its 3 parts
            var parts = hash.Split('.', 3);

            // Grab the salt
            string salt = parts[1];

            // Return salt
            return salt;
        }
    }
}
