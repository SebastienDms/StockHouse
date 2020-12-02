using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockHouse.Controllers
{
    public class PieceController : Controller
    {
        // GET: Piece
        [Route("Piece/Index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}