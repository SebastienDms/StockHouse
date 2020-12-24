using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using StockHouse.Models;
using StockHouse.Requetes;

namespace StockHouse.Controllers
{
    public class JsonController : Controller
    {
        private readonly Requete<Produit> _requetesProduit = new Requete<Produit>();

        // GET: Json
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        [Route("Json/GetAllProduct")]
        public async Task<string> GetAllProduct()
        {
            var resList = await _requetesProduit.GetAllAsync();

            var json = new JavaScriptSerializer().Serialize(resList);

            return json;
        }

        [HttpGet]
        [Route("Json/GetProductById/{id}")]
        public async Task<string> GetProductById(int id)
        {
            var resProduit = await _requetesProduit.GetByIdAsync(id);

            var json = new JavaScriptSerializer().Serialize(resProduit);

            return json;
        }

    }
}