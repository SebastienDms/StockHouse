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
                if (_requetes.NamePieceExist(newPiece))
                {
                    ModelState.AddModelError("Nom", "Ce nom de pièce existe déjà!");
                    return View(newPiece);
                }
                else
                {
                    //Piece newPiece = new Piece();
                    //newPiece.Nom = nom;
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
        public async Task<ActionResult> ModifierUnePiece(string idcherche)
        {
            int Id;
            int.TryParse(idcherche, out Id);

            var searchPiece = await _requetes.GetByIdAsync(Id);

            return View(searchPiece);
        }

        [HttpPost]
        public ActionResult ModifierPiece(int idModif, string nomModif)
        {
            Piece modifPiece = new Piece {Id = idModif, Nom = nomModif};
            _requetes.UpdatePiece(modifPiece);

            return RedirectToAction("Index", "Piece");
        }

        [HttpGet]
        [Route("Piece/Supprimer-une-piece")]
        public async Task<ActionResult> SupprimerUnePiece()
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