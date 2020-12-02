using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockHouse.Controllers
{
    public class AchatController : Controller
    {
        // GET: Achat
        [Route("Achat/Index")]

        public ActionResult Index()
        {
            return View();
        }
    }
}