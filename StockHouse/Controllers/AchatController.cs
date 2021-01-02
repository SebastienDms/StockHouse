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
        private BdStockHouse db = new BdStockHouse();

        private readonly Requete<Achat> _requetesA = new Requete<Achat>();
        private readonly Requete<Produit> _requetesP = new Requete<Produit>();
        private readonly Requete<Magasin> _requetesM = new Requete<Magasin>();
        private readonly Requete<User> _requetesU = new Requete<User>();


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
            List<Achat> listAchats = (List<Achat>)await _requetesA.GetAllAsync();

            return View(listAchats);
        }

        [HttpGet]
        [Route("Achat/Ajouter-achat")]
        public ActionResult AjouterAchat()
        {
            ViewBag.ProduitsList = new SelectList(db.Produits, "Id", "Nom");
            ViewBag.MagasinList = new SelectList(db.Magasins, "Id", "Nom");
            ViewBag.UserList = new SelectList(db.Users, "Id", "Nom");

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
                var prod = await _requetesP.GetByIdAsync(newAchat.ProduitId);

                prod.Stock += newAchat.Quantité;

                var res = await _requetesP.UpdateAsync(prod);

                if (res==0)
                {
                    
                }

                await _requetesA.AddAsync(newAchat);

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
                var searchPiece = await _requetesA.GetByIdAsync(wantedAchat.Id);

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

                var resultat = await _requetesA.UpdateAsync(modifAchat);

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
            await _requetesA.DeleteAsync(idSupprimer);

            return RedirectToAction("Index", "Achat");
        }
    }
}