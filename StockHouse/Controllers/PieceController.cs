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
    public class PieceController : Controller
    {
        private readonly Requete<Piece> _requetes = new Requete<Piece>();

        // GET: Piece
        [HttpGet]
        [Route("Piece/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Piece/Toutes-les-pieces")]
        public async Task<ActionResult> ToutesLesPieces()
        {
            List<Piece> listPieces = (List<Piece>) await _requetes.GetAllAsync();

            return View(listPieces);
        }

        [HttpGet]
        [Route("Piece/Ajouter-piece")]
        public ActionResult AjouterPiece()
        {
            return View();
        }

        [HttpPost]
        [Route("Piece/Ajouter-une-piece")]
        public async Task<ActionResult> AjouterUnePiece(Piece newPiece)
        {
            if (!ModelState.IsValid)
            {
                return View(newPiece);
            }
            else
            {
                /********* /!\ Utiliser await et non le .Result /!\ ************/
                if (await _requetes.NameExist(newPiece))
                {
                    ModelState.AddModelError("Nom", "Ce nom de pièce existe déjà!");
                    return View(newPiece);
                }
                else
                {
                    await _requetes.AddAsync(newPiece);
                    var id = await _requetes.Save();

                    return RedirectToAction("Index","Piece");
                }
            }
        }

        [HttpGet]
        [Route("Piece/Chercher-une-piece")]
        public ActionResult ChercherUnePiece()
        {
            return View();
        }

        [HttpPost]
        [Route("Piece/Modifier-une-piece")]
        public async Task<ActionResult> ModifierUnePiece(Piece wantedPiece)
        {
            if (wantedPiece.Id == 0)
            {
                ModelState.AddModelError("Id", "Veuillez saisir un ID!");
                return View("ChercherUnePiece");
            }
            else
            {
                var searchPiece = await _requetes.GetByIdAsync(wantedPiece.Id);

                if (searchPiece == null)
                {
                    ModelState.AddModelError("Id", "Cet ID n'existe pas!");
                    return View("ChercherUnePiece");
                }
                return View(searchPiece);
            }
        }

        [HttpPost]
        [Route("Piece/Modifier-piece")]
        public async Task<ActionResult> ModifierPiece(Piece modifPiece)
        {
            if (!ModelState.IsValid)
            {
                return View(modifPiece);
            }
            else
            {

                var resultat = await _requetes.UpdateAsync(modifPiece);

                if (resultat == 0)
                {
                    ModelState.AddModelError("Nom", "Ce nom est le nom actuel!");
                    return View(modifPiece);
                }

                return RedirectToAction("Index", "Piece");
            }
        }

        [HttpGet]
        [Route("Piece/Supprimer-une-piece")]
        public ActionResult SupprimerUnePiece()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SupprimerPiece(int idSupprimer)
        {
            await _requetes.DeleteAsync(idSupprimer);

            return RedirectToAction("Index", "Piece");
        }
    }
}