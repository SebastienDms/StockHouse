using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StockHouse.Models;
using StockHouse.Requetes;
using System.Web.Security;

namespace StockHouse.Controllers
{
    public class AccountController : Controller
    {
        private readonly Requete<User> _requetes = new Requete<User>();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        //[Route("Account/Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[Route("Account/Log-in")]
        public async Task<ActionResult> Login(User loginUser)
        {
            var isValid = await _requetes.VerifLogin(loginUser);
            
            if (isValid)
            {
                var logedUser = await _requetes.GetUserByMailAsync(loginUser.AdresseMail);
                FormsAuthentication.SetAuthCookie(logedUser.Nom,false);
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("","Adresse e-mail et/ou mot de passe invalide(s)...");
                return View();
            }
        }
        [HttpGet]
        //[Route("Account/Signup")]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        //[Route("Account/Sign-up")]
        public async Task<ActionResult> Signup(User newUser)
        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }
            else
            {
                try
                {
                    /* Membre ou modérateur ou admin */
                    /* Par défaut Membre */
                    newUser.Role = "Membre";
                    /* cryptage du mot de passe */
                    newUser.MotDePasse = Cryptage.EncryptStringToBytes_Aes(newUser.MotDePasse);

                    var res = await _requetes.AddAsync(newUser);
                    if (!res)
                    {
                        ModelState.AddModelError("AdresseMail", "Cette adresse e-mail existe déjà!");
                        return View(newUser);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}