using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockHouse.Models
{
    public class Piece : IModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<Produit> Produits { get; set; }
    }
}