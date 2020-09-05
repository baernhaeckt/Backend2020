using System;
using System.Globalization;
using System.Security.Cryptography;
using Backend.Infrastructure.Abstraction.Security;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Backend.Infrastructure.Security
{
    public class HmacSha512PasswordStorage : IPasswordStorage
    {
        // These constants may be changed without breaking existing hashes.
        public const int SaltBytes = 24;

        public const int HashBytes = 18;

        public const int Pbkdf2Iterations = 64000;

        public const KeyDerivationPrf KeyDerivationPrf = Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA512;

        // These constants define the encoding and may not be changed.
        public const int HashSections = 5;

        public const int HashAlgorithmIndex = 0;

        public const int IterationIndex = 1;

        public const int HashSizeIndex = 2;

        public const int SaltIndex = 3;

        public const int Pbkdf2Index = 4;

        public string Create(string password)
        {
            // Generate a random salt
            var salt = new byte[SaltBytes];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = Generate(password, salt, Pbkdf2Iterations, KeyDerivationPrf, HashBytes);

            // format: algorithm:iterations:hashSize:salt:hash
            string parts =
                KeyDerivationPrf.ToString("F") +
                ":" +
                Pbkdf2Iterations +
                ":" +
                hash.Length +
                ":" +
                Convert.ToBase64String(salt) +
                ":" +
                Convert.ToBase64String(hash);
            return parts;
        }

        public bool Match(string password, string goodHash)
        {
            char[] delimiter = { ':' };
            string[] split = goodHash.Split(delimiter);

            if (split.Length != HashSections)
            {
                throw new InvalidHashException("Fields are missing from the password hash.");
            }

            var keyDerivationPrf = (KeyDerivationPrf)Enum.Parse(typeof(KeyDerivationPrf), split[HashAlgorithmIndex]);

            int iterations;
            try
            {
                iterations = int.Parse(split[IterationIndex], CultureInfo.InvariantCulture);
            }
            catch (ArgumentNullException ex)
            {
                throw new CannotPerformOperationException("Invalid argument given to Int32.Parse", ex);
            }
            catch (FormatException ex)
            {
                throw new InvalidHashException("Could not parse the iteration count as an integer.", ex);
            }
            catch (OverflowException ex)
            {
                throw new InvalidHashException("The iteration count is too large to be represented.", ex);
            }

            if (iterations < 1)
            {
                throw new InvalidHashException("Invalid number of iterations. Must be >= 1.");
            }

            byte[] salt;
            try
            {
                salt = Convert.FromBase64String(split[SaltIndex]);
            }
            catch (ArgumentNullException ex)
            {
                throw new CannotPerformOperationException("Invalid argument given to Convert.FromBase64String", ex);
            }
            catch (FormatException ex)
            {
                throw new InvalidHashException("Base64 decoding of salt failed.", ex);
            }

            byte[] hash;
            try
            {
                hash = Convert.FromBase64String(split[Pbkdf2Index]);
            }
            catch (ArgumentNullException ex)
            {
                throw new CannotPerformOperationException("Invalid argument given to Convert.FromBase64String", ex);
            }
            catch (FormatException ex)
            {
                throw new InvalidHashException("Base64 decoding of pbkdf2 output failed.", ex);
            }

            int storedHashSize;
            try
            {
                storedHashSize = int.Parse(split[HashSizeIndex], CultureInfo.InvariantCulture);
            }
            catch (ArgumentNullException ex)
            {
                throw new CannotPerformOperationException("Invalid argument given to Int32.Parse", ex);
            }
            catch (FormatException ex)
            {
                throw new InvalidHashException("Could not parse the hash size as an integer.", ex);
            }
            catch (OverflowException ex)
            {
                throw new InvalidHashException("The hash size is too large to be represented.", ex);
            }

            if (storedHashSize != hash.Length)
            {
                throw new InvalidHashException("Hash length doesn't match stored hash length.");
            }

            byte[] testHash = Generate(password, salt, iterations, keyDerivationPrf, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            return diff == 0;
        }

        private static byte[] Generate(string password, byte[] salt, int iterations, KeyDerivationPrf keyDerivationPrf, int outputBytes) =>
            KeyDerivation.Pbkdf2(
                password,
                salt,
                keyDerivationPrf,
                iterations,
                outputBytes);
    }
}