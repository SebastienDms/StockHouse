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
    public class ProduitController : Controller
    {
        private readonly Requete<Produit> _requetes = new Requete<Produit>();

        // GET: Produit
        [HttpGet]
        [Route("Produit/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Produit/Tous-les-produits")]
        public async Task<ActionResult> TousLesProduits()
        {
            List<Produit> listProduits = (List<Produit>)await _requetes.GetAllAsync();

            return View(listProduits);
        }

        [HttpGet]
        [Route("Produit/Ajouter-produit")]
        public ActionResult AjouterProduit()
        {
            return View();
        }

        [HttpPost]
        [Route("Produit/Ajouter-un-produit")]
        public async Task<ActionResult> AjouterUnProduit(Produit newProduit)
        {
            if (!ModelState.IsValid)
            {
                return View(newProduit);
            }
            else
            {
                /********* /!\ Utiliser await et non le .Result /!\ ************/
                if (await _requetes.NameExist(newProduit))
                {
                    ModelState.AddModelError("Nom", "Ce nom de produti existe déjà!");
                    return View(newProduit);
                }
                else
                {
                    await _requetes.AddAsync(newProduit);
                    var id = await _requetes.Save();

                    return RedirectToAction("Index", "Produit");
                }
            }
        }

        [HttpGet]
        [Route("Produit/Chercher-un-produit")]
        public ActionResult ChercherUnProduit()
        {
            return View();
        }

        [HttpPost]
        [Route("Produit/Modifier-un-produit")]
        public async Task<ActionResult> ModifierUnProduit(Produit wantedProduit)
        {
            if (wantedProduit.Id == 0)
            {
                ModelState.AddModelError("Id", "Veuillez saisir un ID!");
                return View("ModifierUnProduit");
            }
            else
            {
                var searchProduit = await _requetes.GetByIdAsync(wantedProduit.Id);

                if (searchProduit == null)
                {
                    ModelState.AddModelError("Id", "Cet ID n'existe pas!");
                    return View("ModifierUnProduit");
                }
                return View(searchProduit);
            }


        }

        [HttpPost]
        [Route("Produit/Modifier-produit")]
        public async Task<ActionResult> ModifierProduit(Produit modifProduit)
        {
            if (!ModelState.IsValid)
            {
                return View(modifProduit);
            }
            else
            {

                var resultat = await _requetes.UpdateAsync(modifProduit);

                if (resultat == 0)
                {
                    ModelState.AddModelError("Nom", "Ce nom est le nom actuel!");
                    return View(modifProduit);
                }

                return RedirectToAction("Index", "Produit");
            }
        }

        [HttpGet]
        [Route("Produit/Supprimer-un-produit")]
        public ActionResult SupprimerUnProduit()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SupprimerProduit(int idSupprimer)
        {
            await _requetes.DeleteAsync(idSupprimer);

            return RedirectToAction("Index", "Produit");
        }

    }
}