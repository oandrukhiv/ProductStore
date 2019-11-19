using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductStore.Entities;
using ProductStore.Entities.Models;
using ProductStore.Services.Interfaces;
using ProductStore.Services.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.Services.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationContext _context;
        public IdentityService(ApplicationContext context, IOptionsMonitor<JwtSettings> subOptionsAccessor)
        {
            _context = context;
            _jwtSettings = subOptionsAccessor.CurrentValue;
        }
        public async Task<AuthenticationResult> RegisterAsync(string firstName,
            string password, string lastName, string email, string cellNumber)
        {
            var existingUser = _context.Users.FirstOrDefault(c => c.Email == email); 

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    ErrorMessage = new[] { "User with this email already exist" }
                };
            }
            var passwordHash = HashPassword(password);

            var newUser = new User
            {
                FirstName = firstName,
                Password = passwordHash,
                LastName = lastName,
                Email = email,
                CellNumber = cellNumber,
                Role = "user"
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            var tocketHandler = new JwtSecurityTokenHandler();
            var token = GetToken(newUser, tocketHandler);

            return new AuthenticationResult
            {
                Success = true,
                Token = tocketHandler.WriteToken(token)
            };
        }
        public async Task<AuthenticationResult> LoginAsync(string password, string email)
        {
            var savedPasswordHash =  _context.Users.FirstOrDefault(c => c.Email == email).Password;

            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            User existingUser = null;
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return new AuthenticationResult
                    {
                        ErrorMessage = new[] { "Incorrect password!" }
                    };
                }
                else 
                {
                    existingUser = _context.Users.FirstOrDefault(c => c.Email == email);
                }
            }

            var tocketHandler = new JwtSecurityTokenHandler();
            var token = GetToken(existingUser, tocketHandler);

            return new AuthenticationResult
            {
                Success = true,
                Token = tocketHandler.WriteToken(token)
            };
        }
        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            var savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }
        private SecurityToken GetToken(User user, JwtSecurityTokenHandler tocketHandler)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);            
            var tockenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim (JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim (JwtRegisteredClaimNames.Email, user.Email),
                    new Claim (ClaimsIdentity.DefaultNameClaimType, user.LastName),
                    new Claim (ClaimsIdentity.DefaultRoleClaimType, user.Role)
                }),               
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tocketHandler.CreateToken(tockenDescriptor);
            return token;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Set<User>().FindAsync(email);
        }
    }
}
