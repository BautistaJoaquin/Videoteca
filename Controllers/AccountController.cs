using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge_NET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace challenge_NET.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContext _context;

        public AccountController(AppDBContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public IActionResult Login()
        {
           return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userdetails = await _context.Userdetails.SingleOrDefaultAsync(m => m.Email == model.Email && m.Password ==model.Password);
                if(userdetails == null)
                {
                    ModelState.AddModelError("Password","Inicio de Sesion Invalido.");
                    return View("Login");
                }
                HttpContext.Session.SetString("userId",userdetails.Name);
            }
            else
            {
                return View("Login");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // var check = _context.Userdetails.FirstOrDefault(s => s.Email == model.Email);
                // if (check == null)

                Userdetails user = new Userdetails
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                   
                };

                _context.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error en Registro");
                return View();
            }
            // return RedirectToAction("Index", "Home");
            return View(model);

        }

        
        public  IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

   


        
    }
}
