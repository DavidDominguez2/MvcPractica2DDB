using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcPractica2DDB.Models;
using MvcPractica2DDB.Repositories;
using System.Security.Claims;

namespace MvcPractica2DDB.Controllers {
    public class ManagedController : Controller {

        private RepositoryLibros repo;

        public ManagedController(RepositoryLibros repo) {
            this.repo = repo;
        }

        public IActionResult LogIn() {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password) {
            email = "brocode@gmail.com";
            password = "12345";
            Usuario user = await this.repo.LoginUserAsync(email, password);

            if (user != null) {
                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name,
                    ClaimTypes.Role
                );

                Claim claimName = new Claim(ClaimTypes.Name, email);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString());
                Claim claimImage = new Claim("Image", user.Foto);
                Claim claimNombre = new Claim("Nombre", user.Nombre);
                Claim claimApellido = new Claim("Apellido", user.Apellidos);

                identity.AddClaim(claimName);
                identity.AddClaim(claimId);
                identity.AddClaim(claimImage);
                identity.AddClaim(claimNombre);
                identity.AddClaim(claimApellido);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                //Redirigir 
                return RedirectToAction("Index", "Libros");
            } else {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }

        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Libros");
        }
    }
}
