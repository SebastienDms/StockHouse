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
        [Route("Piece/Ajouter-une-piece")]
        public ActionResult AjouterUnePiece()
        {
            return View();
        }
        [HttpPost]
        //[Route("Piece/Ajouter-piece")]
        public async Task<ActionResult> AjouterPiece(string nom)
        {
            Piece newPiece = new Piece();
            newPiece.Nom = nom;

            await _requetes.AddAsync(newPiece);
            var id = await _requetes.Save();

            return RedirectToAction("Index","Piece");
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
        //[Route("")]
        public ActionResult ModifierPiece(int idModif, string nomModif)
        {
            Piece modifPiece = new Piece {Id = idModif, Nom = nomModif};
            _requetes.UpdatePiece(modifPiece);

            return RedirectToAction("Index", "Piece");
        }
    }
}