using System;
using System.Collections.Generic;
using System.Globalization;
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
        //[Route("Achat/Ajouter-une-achat")]
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
                //if (_requetes.NameAchatExist(newAchat))
                //{
                //    ModelState.AddModelError("Nom", "Ce nom de pièce existe déjà!");
                //    return View(newAchat);
                //}
                //else
                //{
                    //Achat newAchat = new Achat();
                    //newAchat.Nom = nom;
                    await _requetes.AddAsync(newAchat);
                    var id = await _requetes.Save();

                    return RedirectToAction("Index", "Achat");
                //}
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
        public async Task<ActionResult> ModifierUnAchat(string idcherche)
        {
            int Id;
            int.TryParse(idcherche, out Id);

            var searchAchat = await _requetes.GetByIdAsync(Id);

            return View(searchAchat);
        }

        [HttpPost]
        public async Task<ActionResult> ModifierAchat(int idModif, string prixModif)
        {
            int.TryParse(prixModif, NumberStyles.AllowDecimalPoint, new CultureInfo("fr-BE"), out int prixResult);
            Achat modifAchat = new Achat { Id = idModif, Prix = prixResult };
            await _requetes.UpdateAchat(modifAchat);

            return RedirectToAction("Index", "Achat");
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