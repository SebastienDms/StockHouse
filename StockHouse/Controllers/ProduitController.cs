using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockHouse.Controllers
{
    public class ProduitController : Controller
    {
        // GET: Produit
        [Route("Produit/Index")]

        public ActionResult Index()
        {
            return View();
        }
    }
}