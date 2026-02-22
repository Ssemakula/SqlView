using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SqlView.Utilities
{
    /// <summary>
    /// Provides password encryption/decryption using Windows DPAPI
    /// 
    /// SECURITY DESIGN DECISION:
    /// - Passwords are encrypted to the current Windows user account
    /// - Passwords CANNOT be decrypted after Windows reinstall (by design)
    /// - This prevents unauthorized export/sharing of database credentials
    /// - Users must manually re-enter passwords after reinstall
    /// - This is intentional security behavior, not a bug
    /// </summary>
    public static class PasswordEncryption
    {
        /// <summary>
        /// Encrypts a password using Windows DPAPI.
        /// The encrypted password can only be decrypted by the same Windows user account.
        /// </summary>
        /// <param name="password">Plain text password to encrypt</param>
        /// <returns>Base64-encoded encrypted password</returns>
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            try
            {
                // Convert plain text password to byte array
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(password);

                // Encrypt using Windows DPAPI
                // DataProtectionScope.CurrentUser means only this Windows user can decrypt
                byte[] encryptedBytes = ProtectedData.Protect(
                    plainTextBytes,           // Data to encrypt
                    null,                     // Optional entropy (extra secret)
                    DataProtectionScope.CurrentUser); // Scope to current Windows user

                // Convert encrypted bytes to Base64 string for storage
                return Convert.ToBase64String(encryptedBytes);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to encrypt password: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Decrypts a password that was encrypted with EncryptPassword.
        /// Can only be decrypted by the same Windows user who encrypted it.
        /// </summary>
        /// <param name="encryptedPassword">Base64-encoded encrypted password</param>
        /// <returns>Plain text password</returns>
        public static string DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
                return string.Empty;

            try
            {
                // Convert Base64 string back to byte array
                byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);

                // Decrypt using Windows DPAPI
                byte[] decryptedBytes = ProtectedData.Unprotect(
                    encryptedBytes,           // Data to decrypt
                    null,                     // Optional entropy (must match encryption)
                    DataProtectionScope.CurrentUser); // Must match encryption scope

                // Convert decrypted bytes back to string
                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to decrypt password: {ex.Message}", ex);
            }
        }
    }
}
