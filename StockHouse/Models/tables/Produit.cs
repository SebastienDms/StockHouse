using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StockHouse.Models.tables;

namespace StockHouse.Models
{
    public class Produit : IModelNom
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vous devez saisir le nom du produit.")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Vous devez saisir le stock.")]
        public int Stock { get; set; }
        public string Type { get; set; }
        public string Marque { get; set; }
        [ForeignKey("Piece")]
        [Required(ErrorMessage = "Vous devez choisir une pièce de la maison.")]
        public int PieceId { get; set; }
        public Piece Piece { get; set; }

        public ICollection<Achat> Achats { get; set; }
    }
}