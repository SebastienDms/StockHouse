using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockHouse.Models;

namespace StockHouse.Controllers
{
    [Authorize]
    public class ProduitsTestController : Controller
    {
        private BdStockHouse db = new BdStockHouse();

        // GET: ProduitsTest
        public async Task<ActionResult> Index()
        {
            var produits = db.Produits.Include(p => p.Piece);
            return View(await produits.ToListAsync());
        }

        // GET: ProduitsTest/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = await db.Produits.FindAsync(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

        // GET: ProduitsTest/Create
        public ActionResult Create()
        {
            ViewBag.PieceId = new SelectList(db.Pieces, "Id", "Nom");
            return View();
        }

        // POST: ProduitsTest/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nom,Stock,Type,Marque,PieceId")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                db.Produits.Add(produit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PieceId = new SelectList(db.Pieces, "Id", "Nom", produit.PieceId);
            return View(produit);
        }

        // GET: ProduitsTest/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = await db.Produits.FindAsync(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            ViewBag.PieceId = new SelectList(db.Pieces, "Id", "Nom", produit.PieceId);
            return View(produit);
        }

        // POST: ProduitsTest/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nom,Stock,Type,Marque,PieceId")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PieceId = new SelectList(db.Pieces, "Id", "Nom", produit.PieceId);
            return View(produit);
        }

        // GET: ProduitsTest/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = await db.Produits.FindAsync(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

        // POST: ProduitsTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Produit produit = await db.Produits.FindAsync(id);
            db.Produits.Remove(produit);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
