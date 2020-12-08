using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockHouse.Models
{
    public class Achat : IModelBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vous devez saisir le prix.")]
        public int Prix { get; set; }
        [Required(ErrorMessage = "Vous devez saisir la quantité achetée.")]
        public int Quantité { get; set; }
        [Required(ErrorMessage = "Vous devez saisir la date de l'achat.")]
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