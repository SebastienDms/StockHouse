using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StockHouse.Models;
using StockHouse.Requetes;

namespace StockHouse.Controllers
{
    public class UserController : Controller
    {
        private readonly Requete<User> _requetes = new Requete<User>();
        //private readonly Cryptage _cryptage = new Cryptage();

        // GET: User
        [HttpGet]
        [Route("User/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("User/Tous-les-utilisateurs")]
        public async Task<ActionResult> TousLesUsers()
        {
            List<User> listUsers = (List<User>)await _requetes.GetAllAsync();

            return View(listUsers);
        }

        [HttpGet]
        [Route("User/Ajouter-utilisateur")]
        public ActionResult AjouterUser()
        {
            return View();
        }

        [HttpPost]
        [Route("User/Ajouter-un-utilisateur")]
        public async Task<ActionResult> AjouterUnUser(User newUser)
        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }
            else
            {
                try
                {
                    await _requetes.AddAsync(newUser);
                    var id = await _requetes.Save();
                }
                catch (DbUpdateException e)
                when (e.InnerException?.InnerException is SqlException sqlEx &&
                      (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    ModelState.AddModelError("AdresseMail", "Cette adresse e-mail existe déjà!");
                    return View(newUser);
                }

                return RedirectToAction("Index", "User");
            }
        }
        [HttpGet]
        [Route("Register-page")]
        public ActionResult RegisterPage()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterUser(User registerUser)
        {
            if (!ModelState.IsValid)
            {
                return View(registerUser);
            }
            else
            {
                try
                {
                    /* Membre ou modérateur ou admin */
                    /* Par défaut Membre */
                    registerUser.Role = "Membre";
                    /* cryptage du mot de passe */
                    registerUser.MotDePasse = Cryptage.EncryptStringToBytes_Aes(registerUser.MotDePasse);
                    await _requetes.AddAsync(registerUser);
                }
                catch (DbUpdateException e)
                    when (e.InnerException?.InnerException is SqlException sqlEx &&
                          (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    ModelState.AddModelError("AdresseMail", "Cette adresse e-mail existe déjà!");
                    return View(registerUser);
                }

                return RedirectToAction("Index", "User");
            }
        }
        [HttpGet]
        [Route("User/Chercher-un-utilisateur")]
        public ActionResult ChercherUnUser()
        {
            return View();
        }

        [HttpPost]
        [Route("User/Modifier-un-utilisateur")]
        public async Task<ActionResult> ModifierUnUser(User wantedUser)
        {
            if (wantedUser.Id == 0)
            {
                ModelState.AddModelError("Id", "Veuillez saisir un ID!");
                return View("ChercherUnUser");
            }
            else
            {
                var searchPiece = await _requetes.GetByIdAsync(wantedUser.Id);

                if (searchPiece == null)
                {
                    ModelState.AddModelError("Id", "Cet ID n'existe pas!");
                    return View("ChercherUnUser");
                }
                return View(searchPiece);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ModifierUser(User modifUser)
        {
            if (!ModelState.IsValid)
            {
                return View(modifUser);
            }
            else
            {

                var resultat = await _requetes.UpdateAsync(modifUser);

                if (resultat == 0)
                {
                    ModelState.AddModelError("Nom", "Ce nom est le nom actuel!");
                    return View(modifUser);
                }

                return RedirectToAction("Index", "User");
            }
        }

        [HttpGet]
        [Route("User/Supprimer-un-utilisateur")]
        public ActionResult SupprimerUnUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SupprimerUser(int idSupprimer)
        {
            await _requetes.DeleteAsync(idSupprimer);

            return RedirectToAction("Index", "User");
        }
    }
}