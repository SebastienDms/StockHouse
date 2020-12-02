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
        [Route("Piece/Index")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("Piece/Toutes-les-pieces")]
        public async Task<ActionResult> ToutesLesPieces()
        {
            List<Piece> listPieces = (List<Piece>) await _requetes.GetAllAsync();

            return View(listPieces);
        }
    }
}