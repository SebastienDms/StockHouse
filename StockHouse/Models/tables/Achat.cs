﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockHouse.Models
{
    public class Achat : IModel
    {
        public int Id { get; set; }
        public int Prix { get; set; }
        public int Quantité { get; set; }
        public DateTime DateAchat { get; set; }
        //[ForeignKey("Utilisateur")]
        public int UserId { get; set; }
        public User User { get; set; }
        //[ForeignKey("Magasin")]
        public int MagasinId { get; set; }
        public Magasin Magasin { get; set; }
        //[ForeignKey("Produit")]
        public int ProduitId { get; set; }
        public Produit Produit { get; set; }

    }
}