using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CarManagement.Constants;
using CarManagement.Data;
using CarManagement.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarManagement.COntrollers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Login()
        {
            if (this.HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Car");

            return View("~/Views/Auth/Login.cshtml");
        }

        public ActionResult Register()
        {
            if (this.HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Car");

            return View("~/Views/Auth/Register.cshtml");
        }

        public async Task<ActionResult> Logout()
        {
            CreateLog(
                this.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value, 
                DateTime.Now, 
                new { status = "logout" }, 
                "Logout");
            _context.SaveChanges();

            await HttpContext.SignOutAsync(); 
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost("api/signin")]
        public async Task<ActionResult> SignIn([FromBody] SignInRequest signInRequest)
        {
            DateTime dtmNow = DateTime.Now;

            if (!UserExists(signInRequest.szEmail))
            {
                var msgNotExist = new { statusCode = "404", message = "Email is not exist" };
                CreateLog(signInRequest.szEmail, dtmNow, msgNotExist, "Login");
                _context.SaveChanges();
                return Json(msgNotExist);
            }

            byte[] passwordHash = GeneratePasswordHash(signInRequest);

            bool bValid = UserValid(signInRequest.szEmail, passwordHash);
            if (bValid)
            {
                var msgSuccess = new { statusCode = "200", message = "Success" };
                CreateLog(signInRequest.szEmail, dtmNow, msgSuccess, "Login");
                _context.SaveChanges();

                var szFullName = _context.User.Where(x => x.szEmail == signInRequest.szEmail).FirstOrDefault().szFullName;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, szFullName),
                    new Claim(ClaimTypes.Email, signInRequest.szEmail)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return Json(msgSuccess);
            }

            var msgWrongPassword = new { statusCode = "404", message = "Wrong password" };
            CreateLog(signInRequest.szEmail, dtmNow, msgWrongPassword, "Login");
            _context.SaveChanges();
            return Json(msgWrongPassword);
        }

        private static byte[] GeneratePasswordHash(SignInRequest signInRequest)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.ASCII.GetBytes(signInRequest.szPassword);
            var passwordHash = md5.ComputeHash(bytes);
            return passwordHash;
        }

        [HttpPost("api/signup")]
        public ActionResult SignUp([FromBody] RegisterModel registerModel)
        {
            DateTime dtmNow = DateTime.Now;
            if (UserExists(registerModel.szEmail))
            {
                var msg = new { statusCode = "404", message = "Email is already exist" };
                CreateLog(registerModel.szEmail, dtmNow, msg, "Register");
                _context.SaveChangesAsync();
                return Json(msg);
            }

            if (registerModel.szPassword != registerModel.szConfirmPassword)
            {
                var msg = new { statusCode = "404", message = "Confirm password is not match" };
                CreateLog(registerModel.szEmail, dtmNow, msg, "Register");
                _context.SaveChangesAsync();
                return Json(msg);
            }

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.ASCII.GetBytes(registerModel.szPassword);
            var passwordHash = md5.ComputeHash(bytes);

            User user = new User
            {
                szEmail = registerModel.szEmail,
                szFullName = registerModel.szFullName,
                PasswordHash = passwordHash,
                dtmCreated = dtmNow,
                dtmUpdated = dtmNow
            };
            _context.User.Add(user);
            _context.SaveChangesAsync();

            //change password hash for logging
            user.PasswordHash = Encoding.ASCII.GetBytes("password is very secret, better not save it in log history."+DateTime.Now.ToString());

            CreateLog(registerModel.szEmail, dtmNow, user, "Register");
            _context.SaveChangesAsync();
            return Json(new { statusCode = "200", message = "Success" });
        }

        private void CreateLog(string szEmail, DateTime dtmNow, Object data, string szAction)
        {
            Log log = new Log
            {
                szUri = AuthConstants.URI,
                szEmail = szEmail,
                szAction = szAction,
                dtmCreated = dtmNow,
                gdHistoryId = Guid.NewGuid(),
                szData = JsonConvert.SerializeObject(data)
            };
            _context.Log.Add(log);
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.szEmail == id);
        }

        private bool UserValid(string szEmail, byte[] passwordHash)
        {
            return _context.User.Any(e => e.szEmail == szEmail && e.PasswordHash == passwordHash);
        }
    }
}