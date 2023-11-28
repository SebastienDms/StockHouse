using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StockHouse.Models.tables;

namespace StockHouse.Models
{
  public class Piece : IModelNom
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "Vous devez saisir le nom.")]
    public string Nom { get; set; }
    public ICollection<Produit> Produits { get; set; }
  }
}