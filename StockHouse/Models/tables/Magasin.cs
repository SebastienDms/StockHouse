using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StockHouse.Models.tables;

namespace StockHouse.Models
{
  public class Magasin : IModelNom
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "Vous devez saisir un nom.")]
    public string Nom { get; set; }
    [Required(ErrorMessage = "Vous devez saisir l'adresse du magasin.")]
    public string Adresse { get; set; }
    [Required(ErrorMessage = "Vous devez saisir la ville du magasin.")]
    public string Ville { get; set; }

    public ICollection<Achat> Achats { get; set; }
  }
}