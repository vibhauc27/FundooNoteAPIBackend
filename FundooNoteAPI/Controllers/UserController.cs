using BussinessLayer.Interface;
using CommonLayer.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace FundooNoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;

        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModal userRegistrationModal)
        {
            try
            {
                var result = iuserBL.Registartion(userRegistrationModal);
                if (result != null)
                {
                    return Ok(new {success = true, message= "Registration successful", data = result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration unsuccessful" });

                }
            }
            catch(System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser(UserLoginModal userLoginModal)
        {
            try
            {
                var result = iuserBL.Login(userLoginModal);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login unsuccessful" });

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
               var result = iuserBL.ForgetPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Email sent successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset email not sent unsuccessful" });

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetLink")]
        public IActionResult ResetLink(string password, string confirmPassword)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = iuserBL.ResetLink(Email, password, confirmPassword);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset Password Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Password not sent" });

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
