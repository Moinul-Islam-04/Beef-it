using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using BCrypt.Net;   

namespace Beef__it.Database
{
    public class UserAuthenticationService
    {
        private readonly SQLiteAsyncConnection _database;

        public UserAuthenticationService()
        {
            _database = ConnectionFactory.GetConnection();
        }
        // Create (Already implemented in previous examples)
        public async Task<bool> RegisterUserAsync(UserDto userDto, string password)
        {
            // Check if username already exists
            var existingUser = await _database.Table<User>()
                .FirstOrDefaultAsync(u => u.Username == userDto.Username);

            if (existingUser != null)
            {
                return false; // Username already exists
            }

            // Hash the password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            // Convert DTO to User entity
            var newUser = userDto.ToEntity(passwordHash);

            // Insert the new user
            int rowsAffected = await _database.InsertAsync(newUser);
            return rowsAffected > 0;
        }

        // Read (Retrieve User Information)
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _database.Table<User>()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        // Update - Change Password
        public async Task<bool> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            try
            {
                // Find the user
                var user = await GetUserByUsernameAsync(username);

                if (user == null)
                {
                    return false; // User not found
                }

                // Verify current password
                if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                {
                    return false; // Current password is incorrect
                }

                // Hash the new password
                string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

                // Update the password
                user.PasswordHash = newPasswordHash;
                int rowsAffected = await _database.UpdateAsync(user);

                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Forgot Password - Reset Password
        public async Task<string> GeneratePasswordResetTokenAsync(string username)
        {
            var user = await GetUserByUsernameAsync(username);

            if (user == null)
            {
                return null; // User not found
            }

            // Generate a time-limited reset token
            string resetToken = GenerateResetToken();

            // Store the reset token with an expiration time
            user.PasswordResetToken = resetToken;
            user.PasswordResetTokenExpiration = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour

            await _database.UpdateAsync(user);

            return resetToken;
        }

        public async Task<bool> ResetPasswordAsync(string username, string resetToken, string newPassword)
        {
            var user = await GetUserByUsernameAsync(username);

            if (user == null)
            {
                return false; // User not found
            }

            // Check if reset token is valid and not expired
            if (user.PasswordResetToken != resetToken ||
                user.PasswordResetTokenExpiration < DateTime.UtcNow)
            {
                return false; // Invalid or expired token
            }

            // Hash the new password
            string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            // Update the password and clear the reset token
            user.PasswordHash = newPasswordHash;
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiration = null;

            int rowsAffected = await _database.UpdateAsync(user);

            return rowsAffected > 0;
        }

        // Delete - Remove User Account
        public async Task<bool> DeleteUserAccountAsync(string username, string password)
        {
            try
            {
                // Find the user
                var user = await GetUserByUsernameAsync(username);

                if (user == null)
                {
                    return false; // User not found
                }

                // Verify password before deletion
                if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return false; // Incorrect password
                }

                // Delete the user
                int rowsAffected = await _database.DeleteAsync(user);

                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Helper method to generate a reset token
        private string GenerateResetToken()
        {
            return Guid.NewGuid().ToString("N");
        }
    }

}
