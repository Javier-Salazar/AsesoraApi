using AsesoraApi.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AsesoraApi.Utilities
{
    public class HashPassword
    {
        public static HashedPassword Hash(String password)
        {
            byte[] salt = new byte[182 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Dervie a 256-bit subkey (use HMACSHA1 with 10,000 iteration)
            String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return new HashedPassword()
            {
                password = hashed,
                Salt = Convert.ToBase64String(salt)
            };
        }

        public static bool CheckHash(String attemptedPassword, String hash, String salt)
        {
            String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: attemptedPassword,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hash == hashed;
        }

        public static byte[] GetHash(String password, String salt)
        {
            byte[] unhashedBytes = Encoding.Unicode.GetBytes(String.Concat(salt, password));
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hashedBytes = sha256.ComputeHash(unhashedBytes);
            return hashedBytes;
        }
    }
}
