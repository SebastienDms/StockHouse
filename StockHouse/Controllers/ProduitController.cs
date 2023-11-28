﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using StockHouse.Models;
using StockHouse.Requetes;

namespace StockHouse.Controllers
{
  [Authorize]
  public class ProduitController : Controller
  {
    protected readonly BdStockHouse Bdd = new BdStockHouse();
    private readonly Requete<Produit> _requetes = new Requete<Produit>();
    private readonly Requete<Piece> _requetePiece = new Requete<Piece>();

    // GET: Produit
    [HttpGet]
    [Route("Produit/Index")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    [Route("Produit/Tous-les-produits")]
    public async Task<ActionResult> TousLesProduits()
    {
      List<Produit> listProduits = (List<Produit>)await _requetes.GetAllAsync();

      return View(listProduits);
    }

    [HttpGet]
    [Route("Produit/Ajouter-produit")]
    public async Task<ActionResult> AjouterProduit()
    {
      var pieces = (List<Piece>)await _requetePiece.GetAllAsync();

      ViewBag.PieceList = new SelectList(pieces, "Id", "Nom");

      return View();
    }

    [HttpPost]
    [Route("Produit/Ajouter-un-produit")]
    public async Task<ActionResult> AjouterUnProduit(Produit newProduit)
    {
      var pieces = (List<Piece>)await _requetePiece.GetAllAsync();

      ViewBag.PieceList = new SelectList(pieces, "Id", "Nom");

      if (!ModelState.IsValid)
      {
        return View(newProduit);
      }
      else
      {
        /********* /!\ Utiliser await et non le .Result /!\ ************/
        if (await _requetes.NameExist(newProduit))
        {
          ModelState.AddModelError("Nom", "Ce nom de produti existe déjà!");
          return View(newProduit);
        }
        else
        {
          await _requetes.AddAsync(newProduit);

          return RedirectToAction("Index", "Produit");
        }
      }
    }

    [HttpGet]
    [Route("Produit/Chercher-un-produit")]
    public ActionResult ChercherUnProduit()
    {
      return View();
    }

    [HttpPost]
    [Route("Produit/Modifier-un-produit")]
    public async Task<ActionResult> ModifierUnProduit(Produit wantedProduit)
    {
      var pieces = (List<Piece>)await _requetePiece.GetAllAsync();

      ViewBag.PieceList = new SelectList(pieces, "Id", "Nom");

      if (wantedProduit.Id == 0)
      {
        ModelState.AddModelError("Id", "Veuillez saisir un ID!");
        return View(wantedProduit);
      }
      else
      {
        var searchProduit = await _requetes.GetByIdAsync(wantedProduit.Id);

        if (searchProduit == null)
        {
          ModelState.AddModelError("Id", "Cet ID n'existe pas!");
          return View("ChercherUnProduit");
        }
        return View(searchProduit);
      }


    }

    [HttpPost]
    [Route("Produit/Modifier-produit")]
    public async Task<ActionResult> ModifierProduit(Produit modifProduit)
    {
      var pieces = (List<Piece>)await _requetePiece.GetAllAsync();

      ViewBag.PieceList = new SelectList(pieces, "Id", "Nom");

      if (!ModelState.IsValid)
      {
        return View(modifProduit);
      }
      else
      {

        var resultat = await _requetes.UpdateAsync(modifProduit);

        if (resultat == 0)
        {
          ModelState.AddModelError("Nom", "Ce nom est le nom actuel!");
          return View(modifProduit);
        }

        return RedirectToAction("Index", "Produit");
      }
    }

    [HttpGet]
    [Route("Produit/Supprimer-un-produit")]
    public ActionResult SupprimerUnProduit()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> SupprimerProduit(int idSupprimer)
    {
      await _requetes.DeleteAsync(idSupprimer);

      return RedirectToAction("Index", "Produit");
    }

    [HttpGet]
    [Route("Produit/Produit-par-piece-recherche")]
    public async Task<ActionResult> ProduitParPieceRecherche()
    {
      var pieces = (List<Piece>)await _requetePiece.GetAllAsync();

      ViewBag.PieceList = new SelectList(pieces, "Id", "Nom");

      return View();
    }
    [HttpPost]
    [Route("Produit/Produit-par-piece")]
    public async Task<ActionResult> ProduitParPiece(Produit idPiece)
    {
      var res = Bdd.Produits.Join(Bdd.Pieces.Where(p => p.Id == idPiece.PieceId),
                              pro => pro.PieceId,
                              pie => pie.Id,
                              (pro, pie) => new { ProduitNom = pro.Nom, PieceNom = pie.Nom }).ToList();

      List<Tuple<string, string>> list = new List<Tuple<string, string>>();

      foreach (var element in res)
      {
        list.Add(new Tuple<string, string>(element.ProduitNom, element.PieceNom));
      }

      ViewBag.ResList = list;

      return View();
    }

  }
}