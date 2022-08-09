using CommonLayer.Modal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration iconfiguration;

        private readonly FundooContext fundooContext;

        public UserRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }

        public UserEntity Registartion(UserRegistrationModal userRegistrationModal)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModal.FirstName;
                userEntity.LastName = userRegistrationModal.LastName;
                userEntity.Email = userRegistrationModal.Email;
                userEntity.Password = EncryptPassword(userRegistrationModal.Password);

                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();

                if(result != 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string Login(UserLoginModal userLoginModal)
        {
            try
            {
                var LoginResult = this.fundooContext.UserTable.Where(user => user.Email == userLoginModal.Email).FirstOrDefault();
                if (LoginResult != null && Decryption(LoginResult.Password) == userLoginModal.Password)
                {
                    var token = GenerateSecurityToken(LoginResult.Email, LoginResult.UserId);
                    return token;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateSecurityToken(string email, long userID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.iconfiguration[("JWT:key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userID", userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public string ForgetPassword(string Email)
        {
            try
            {
                var emailCheck = fundooContext.UserTable.FirstOrDefault(x => x.Email == Email);
                if (emailCheck != null)
                {
                    var Token = GenerateSecurityToken(emailCheck.Email, emailCheck.UserId);
                    MSMQmodal mSMQmodal = new MSMQmodal();
                    mSMQmodal.sendData2Queue(Token);
                    return Token.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetLink(string email, string password, string confirmPassword)
        {
            try
            {
                if(password.Equals(confirmPassword))
                {
                    var emailCheck = fundooContext.UserTable.FirstOrDefault(x => x.Email == email);
                    emailCheck.Password = password;

                    fundooContext.SaveChanges();
                    return true;

                }
                else 
                { 
                    return false; 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string EncryptPassword(string password)
        {
            string Key = "secret@key#123ddsewrvFResd";
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        public static string Decryption(string encryptedPass)
        {
            string Key = "secret@key#123ddsewrvFResd";
            if (string.IsNullOrEmpty(encryptedPass))
            {
                return "";
            }
            var encodeBytes = Convert.FromBase64String(encryptedPass);
            var result = Encoding.UTF8.GetString(encodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}

