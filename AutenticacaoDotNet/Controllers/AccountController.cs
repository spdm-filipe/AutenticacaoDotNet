
using AutenticacaoDotNet.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net;
using System.Security.Claims;

namespace AutenticacaoDotNet.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View(new Credencial());
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credencial credencial)
        {

            if (!ModelState.IsValid)
                return View();

            if (credencial.UserName == "admin" && credencial.Password == "123456")
            {
                // Creating the security context
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                   // new Claim("RH", "RH"),
                    new Claim("ManutencaoPredial", "true"),
                    new Claim("Holerite", "true"),
                    new Claim("EmploymentDate", "2025-01-01"),
                    //new Claim(ClaimTypes.Role, "RH"),
                    //new Claim(ClaimTypes.Role, "Admin1")
                };
                var acessos = BaseBancoMock.Dados.RetornaAcessos();

                foreach (var acesso in acessos)
                {
                   claims.Add(new Claim(ClaimTypes.Role, acesso));
                }

                var identity = new ClaimsIdentity(claims, "MeuCookieAuthenticacao");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = credencial.RememberMe
                };

                await HttpContext.SignInAsync("MeuCookieAuthenticacao", claimsPrincipal, authProperties);

                return RedirectToAction("Index", "Home");

            }
            return Json("Ok");
        }
    }
}
