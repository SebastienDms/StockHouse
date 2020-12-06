﻿using System.Collections.Generic;

namespace StockHouse.Models
{
    public class Magasin : IModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }

        public ICollection<Achat> Achats { get; set; }
    }
}