using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vak_Terkep.Interfaces;
using Vak_Terkep.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text;
using Vak_Terkep.Implementations;
using System;

namespace Vak_Terkep.Controllers
{
    public class AccountsController : Controller
    {
        private IEncryptService encryptService;
        private IUserInterface userInterface;
        private IAuthenticationServices authenticationService;
        private IHttpContextAccessor contextAccessor;

        public AccountsController(IEncryptService encryptService, IUserInterface userInterface, IAuthenticationServices authenticationService, IHttpContextAccessor contextAccessor)
        {
            this.encryptService = encryptService;
            this.userInterface = userInterface;
            this.authenticationService = authenticationService;
            this.contextAccessor = contextAccessor;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        

        [Route("Accounts/GetAccounts")]
        public IActionResult GetAccounts()
        {
            var accounts = userInterface.GetAll().ToList();
            return Json(accounts);
        }

        public IActionResult Register()
        {
            return View("SignIn","Map");
        }

        
        public IActionResult LogOut()
        { 
            authenticationService.LogOut();
            contextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("SignIn","Map");
        }
        public IActionResult TryLogIn(Account account)
        {

            if(account.emailAddress == null || account.userPassword == null)
            {
                TempData["Message"] = "blank";
                return RedirectToAction("SignIn", "Map");
            }
            bool isLoggedIn = authenticationService.TryLogIn(account.emailAddress, account.userPassword);
            if(isLoggedIn == false)
            {
                TempData["Message"] = "error";
                return RedirectToAction("SignIn","Map");
            }
            return RedirectToAction("Start", "Map");


        }


        public IActionResult RegisterUser(Account accounts)
        {
            if (accounts.emailAddress == null || accounts.userPassword == null || accounts.userName == null)
            {
                TempData["Message"] = "null";
                return RedirectToAction("SignUp", "Map");
            }
            if (!accounts.emailAddress.Contains("@"))
            {
                TempData["Message"] = "emailnoat";
                return RedirectToAction("SignUp", "Map");
            }
            if (!accounts.emailAddress.Contains(".com") && !accounts.emailAddress.Contains(".hu"))
            {
                TempData["Message"] = "emailnocomhu";
                return RedirectToAction("SignUp", "Map");
            }
            
            
            var existingAccount = userInterface.GetAll().FirstOrDefault(a => a.emailAddress == accounts.emailAddress);
            if (existingAccount != null)
            {
                TempData["Message"] = "email";
                return RedirectToAction("SignUp", "Map"); 
            }
            bool SzamContain = false;
            char[] szamok = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach(char c in szamok) 
            {
                if (accounts.userPassword.Contains(c)) {
                    SzamContain = true;
                }
            }


            if (accounts.userPassword.Length < 8 || SzamContain==false )
            {
                TempData["Message"] = "tosmall";
                return RedirectToAction("SignUp", "Map"); 

            } 



            accounts.userPassword = encryptService.HashPassword(accounts.userPassword);
            userInterface.Add(accounts);

            return RedirectToAction("SignIn", "Map");
        }

        [HttpPost]
        [Route("Accounts/UploadProfilePicture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "Nincs kiválasztva fájl." });
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = "/uploads/" + fileName;

            var userName = HttpContext.Session.GetString("userName");
            if (string.IsNullOrEmpty(userName))
            {
                return Json(new { success = false, message = "User not found." });
            }

            using (var context = new VakTerkepDbContext())
            {
                var user = context.Accounts.FirstOrDefault(u => u.userName == userName);
                if (user != null)
                {
                    user.ProfilePicturePath = imageUrl;
                    context.SaveChanges();
                }
            }

            HttpContext.Session.SetString("profilePictureUrl", imageUrl);

            return Json(new { success = true, imageUrl });
        }


    }

}