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

namespace StockHouse.Controllers
{
    public class UserController : Controller
    {
        private readonly Requete<User> _requetes = new Requete<User>();

        // GET: User
        [Route("User/Index")]

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("User/Tous-les-users")]
        public async Task<ActionResult> TousLesUsers()
        {
            List<User> listUsers = (List<User>)await _requetes.GetAllAsync();

            return View(listUsers);
        }

        [HttpGet]
        [Route("User/Ajouter-user")]
        //[Route("User/Ajouter-un-user")]
        public ActionResult AjouterUser()
        {
            return View();
        }

        [HttpPost]
        [Route("User/Ajouter-un-user")]
        public async Task<ActionResult> AjouterUnUser(User newUser)
        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }
            else
            {
                int test;
                try
                {
                    await _requetes.AddAsync(newUser);
                    var id = await _requetes.Save();
                }
                catch (DbUpdateException e)
                when (e.InnerException?.InnerException is SqlException sqlEx &&
                      (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    ModelState.AddModelError("Nom", "Ce nom de pièce existe déjà!");
                    return View(newUser);
                }

                return RedirectToAction("Index", "User");
            }
        }

        [HttpGet]
        [Route("User/Chercher-un-user")]
        public ActionResult ChercherUnUser()
        {
            return View();
        }

        [HttpPost]
        [Route("User/Modifier-un-user")]
        public async Task<ActionResult> ModifierUnUser(string idcherche)
        {
            int Id;
            int.TryParse(idcherche, out Id);

            var searchUser = await _requetes.GetByIdAsync(Id);

            return View(searchUser);
        }

        [HttpPost]
        public async Task<ActionResult> ModifierUser(int idModif, string nomModif)
        {
            User modifUser = new User { Id = idModif, Nom = nomModif };
            await _requetes.UpdateUser(modifUser);

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        [Route("User/Supprimer-un-user")]
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