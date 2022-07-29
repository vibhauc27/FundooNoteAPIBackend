using CommonLayer.Modal;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity Registartion(UserRegistrationModal userRegistrationModal);

        public string Login(UserLoginModal userLoginModal);
    }
}
