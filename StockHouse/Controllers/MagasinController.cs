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
    [Authorize]
    public class MagasinController : Controller
    {
        private readonly Requete<Magasin> _requetes = new Requete<Magasin>();

        // GET: Magasin
        [HttpGet]
        [Route("Magasin/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Magasin/Tous-les-magasins")]
        public async Task<ActionResult> TousLesMagasins()
        {
            List<Magasin> listMagasins = (List<Magasin>)await _requetes.GetAllAsync();

            return View(listMagasins);
        }

        [HttpGet]
        [Route("Magasin/Ajouter-magasin")]
        public ActionResult AjouterMagasin()
        {
            return View();
        }

        [Authorize()]
        [HttpPost]
        [Route("Magasin/Ajouter-un-magasin")]
        public async Task<ActionResult> AjouterUnMagasin(Magasin newMagasin)
        {
            if (!ModelState.IsValid)
            {
                return View(newMagasin);
            }
            else
            {
                /********* /!\ Utiliser await et non le .Result /!\ ************/
                if (await _requetes.NameExist(newMagasin))
                {
                    ModelState.AddModelError("Nom", "Ce nom de magasin existe déjà!");
                    return View(newMagasin);
                }
                else
                {
                    await _requetes.AddAsync(newMagasin);
                    var id = await _requetes.Save();

                    return RedirectToAction("Index", "Magasin");
                }
            }
        }

        [HttpGet]
        [Route("Magasin/Chercher-un-magasin")]
        public ActionResult ChercherUnMagasin()
        {
            return View();
        }

        [HttpPost]
        [Route("Magasin/Modifier-un-magasin")]
        public async Task<ActionResult> ModifierUnMagasin(Magasin wantedMagasin)
        {
            if (wantedMagasin.Id == 0)
            {
                ModelState.AddModelError("Id", "Veuillez saisir un ID!");
                return View("ChercherUnMagasin");
            }
            else
            {
                var searchMagasin = await _requetes.GetByIdAsync(wantedMagasin.Id);

                if (searchMagasin == null)
                {
                    ModelState.AddModelError("Id", "Cet ID n'existe pas!");
                    return View("ChercherUnMagasin");
                }
                return View(searchMagasin);
            }
        }

        [HttpPost]
        [Route("Magasin/Modifier-magasin")]
        public async Task<ActionResult> ModifierMagasin(Magasin modifMagasin)
        {
            if (!ModelState.IsValid)
            {
                return View(modifMagasin);
            }
            else
            {

                var resultat = await _requetes.UpdateAsync(modifMagasin);

                if (resultat == 0)
                {
                    ModelState.AddModelError("Nom", "Ce nom est le nom actuel!");
                    return View(modifMagasin);
                }

                return RedirectToAction("Index", "Magasin");
            }
        }

        [HttpGet]
        [Route("Magasin/Supprimer-un-magasin")]
        public ActionResult SupprimerUnMagasin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SupprimerMagasin(int idSupprimer)
        {
            await _requetes.DeleteAsync(idSupprimer);

            return RedirectToAction("Index", "Magasin");
        }

    }
}