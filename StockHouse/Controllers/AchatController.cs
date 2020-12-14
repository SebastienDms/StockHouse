using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StockHouse.Models;
using StockHouse.Requetes;

namespace StockHouse.Controllers
{
    public class AchatController : Controller
    {
        private readonly Requete<Achat> _requetes = new Requete<Achat>();

        // GET: Achat
        [HttpGet]
        [Route("Achat/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Achat/Tous-les-achats")]
        public async Task<ActionResult> TousLesAchats()
        {
            List<Achat> listAchats = (List<Achat>)await _requetes.GetAllAsync();

            return View(listAchats);
        }

        [HttpGet]
        [Route("Achat/Ajouter-achat")]
        public ActionResult AjouterAchat()
        {
            return View();
        }

        [HttpPost]
        [Route("Achat/Ajouter-un-achat")]
        public async Task<ActionResult> AjouterUnAchat(Achat newAchat)
        {
            if (!ModelState.IsValid)
            {
                return View(newAchat);
            }
            else
            {
                await _requetes.AddAsync(newAchat);
                var id = await _requetes.Save();

                return RedirectToAction("Index", "Achat");
            }
        }

        [HttpGet]
        [Route("Achat/Chercher-un-achat")]
        public ActionResult ChercherUnAchat()
        {
            return View();
        }

        [HttpPost]
        [Route("Achat/Modifier-un-achat")]
        public async Task<ActionResult> ModifierUnAchat(Achat wantedAchat)
        {
            if (wantedAchat.Id == 0)
            {
                ModelState.AddModelError("Id", "Veuillez saisir un ID!");
                return View("ChercherUnAchat");
            }
            else
            {
                var searchPiece = await _requetes.GetByIdAsync(wantedAchat.Id);

                if (searchPiece == null)
                {
                    ModelState.AddModelError("Id", "Cet ID n'existe pas!");
                    return View("ChercherUnAchat");
                }
                return View(searchPiece);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ModifierAchat(Achat modifAchat)
        {
            if (!ModelState.IsValid)
            {
                return View(modifAchat);
            }
            else
            {

                var resultat = await _requetes.UpdateAsync(modifAchat);

                if (resultat == 0)
                {
                    ModelState.AddModelError("Nom", "Ce nom est le nom actuel!");
                    return View(modifAchat);
                }

                return RedirectToAction("Index", "Achat");
            }
            //int.TryParse(prixModif, NumberStyles.AllowDecimalPoint, new CultureInfo("fr-BE"), out int prixResult);
        }

        [HttpGet]
        [Route("Achat/Supprimer-un-achat")]
        public ActionResult SupprimerUnAchat()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SupprimerAchat(int idSupprimer)
        {
            await _requetes.DeleteAsync(idSupprimer);

            return RedirectToAction("Index", "Achat");
        }
    }
}