using BussinessLayer.Interface;
using CommonLayer.Modal;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class UserBL : IUserBL

    {
        private readonly IUserRL iuserRL;

        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }


        public UserEntity Registartion(UserRegistrationModal userRegistrationModal)
        {
            try
            {
                return iuserRL.Registartion(userRegistrationModal);
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
                return iuserRL.Login(userLoginModal);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string ForgetPassword(string Email)
        {
            try
            {
                return iuserRL.ForgetPassword(Email);
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
                    return iuserRL.ResetLink(email, password, confirmPassword);
               
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
