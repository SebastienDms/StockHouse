using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
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

      var json = JsonConvert.SerializeObject(resList);

      return json;
    }

    [HttpGet]
    [Route("Json/GetProductById/{id}")]
    public async Task<string> GetProductById(int id)
    {
      var resProduit = await _requetesProduit.GetByIdAsync(id);

      var json = JsonConvert.SerializeObject(resProduit);

      return json;
    }

  }
}