using CommonLayer.Modal;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity Registartion(UserRegistrationModal userRegistrationModal);

        public string Login(UserLoginModal userLoginModal);

        public string ForgetPassword(string Email);
    }
}
