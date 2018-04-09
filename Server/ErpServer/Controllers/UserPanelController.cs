using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace ErpServer.Controllers
{
    public class UserPanelController : Controller
    {

        IStringLocalizer Localizer;

        string UserName { get; set; }
        string Password { get; set; }
        static public bool Error = false;
        static public string ErrorMessage { get; set; }

        public UserPanelController(IStringLocalizer<UserPanelController> localizer)
        {
            Localizer = localizer;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterNewUser()
        {
            List<string> values = new List<string>();
            try
            {
                //Login
                UserName = Request.Form["UserName"];
                //Password
                Password = Request.Form["Password"];
                if (UserName == string.Empty)
                    throw new Exception(Localizer["NoUserNameError"].Value);
                if (Password == string.Empty)
                    throw new Exception(Localizer["NoPasswordError"].Value);
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                    password: Password,
                                    salt: salt,
                                    prf: KeyDerivationPrf.HMACSHA1,
                                    iterationCount: 10000,
                                    numBytesRequested: 256 / 8));

                    
                
                Error = false;
            }
            catch (InvalidCastException ex)
            {
                Error = true;
                ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                Error = true;
                ErrorMessage = ex.Message;
            }
            finally
            {
            }

            return View("Register");

        }
    }
}