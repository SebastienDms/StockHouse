using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockHouse.Models
{
    public class Produit : IModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Stock { get; set; }
        public string Type { get; set; }
        public string Marque { get; set; }
        [ForeignKey("Piece")]
        public int PieceId { get; set; }
        public Piece Piece { get; set; }

        public ICollection<Achat> Achats { get; set; }
    }
}