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
    public class MagasinController : Controller
    {
        private readonly Requete<Magasin> _requetes = new Requete<Magasin>();

        // GET: Magasin
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
        //[Route("Magasin/Ajouter-un-magasin")]
        public ActionResult AjouterMagasin()
        {
            return View();
        }

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
                //if (_requetes.NameMagasinExist(newMagasin))
                //{
                //    ModelState.AddModelError("Nom", "Ce nom de pièce existe déjà!");
                //    return View(newMagasin);
                //}
                //else
                //{
                    //Magasin newMagasin = new Magasin();
                    //newMagasin.Nom = nom;
                    await _requetes.AddAsync(newMagasin);
                    var id = await _requetes.Save();

                    return RedirectToAction("Index", "Magasin");
                //}
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
        public async Task<ActionResult> ModifierUnMagasin(string idcherche)
        {
            int Id;
            int.TryParse(idcherche, out Id);

            var searchMagasin = await _requetes.GetByIdAsync(Id);

            return View(searchMagasin);
        }

        [HttpPost]
        public async Task<ActionResult> ModifierMagasin(int idModif, string nomModif)
        {
            Magasin modifMagasin = new Magasin { Id = idModif, Nom = nomModif };
            await _requetes.UpdateMagasin(modifMagasin);

            return RedirectToAction("Index", "Magasin");
        }

        [HttpGet]
        [Route("Magasin/Supprimer-un-magasin")]
        public async Task<ActionResult> SupprimerUnMagasin()
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