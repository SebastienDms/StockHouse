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
        //[Route("Produit/Ajouter-un-produit")]
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
                //if (_requetes.NameProduitExist(newProduit))
                //{
                //    ModelState.AddModelError("Nom", "Ce nom de pièce existe déjà!");
                //    return View(newProduit);
                //}
                //else
                //{
                    //Produit newProduit = new Produit();
                    //newProduit.Nom = nom;
                    await _requetes.AddAsync(newProduit);
                    var id = await _requetes.Save();

                    return RedirectToAction("Index", "Produit");
                //}
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
        public async Task<ActionResult> ModifierUnProduit(string idcherche)
        {
            int Id;
            int.TryParse(idcherche, out Id);

            var searchProduit = await _requetes.GetByIdAsync(Id);

            return View(searchProduit);
        }

        [HttpPost]
        public async Task<ActionResult> ModifierProduit(int idModif, string nomModif)
        {
            Produit modifProduit = new Produit { Id = idModif, Nom = nomModif };
            await _requetes.UpdateProduit(modifProduit);

            return RedirectToAction("Index", "Produit");
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