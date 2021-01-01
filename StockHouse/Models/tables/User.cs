using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StockHouse.Models.tables;

namespace StockHouse.Models
{
    public class User : IModelNom
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vous devez saisir votre nom.")]
        public string Nom { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "Vous devez saisir votre adresse mail.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Adresse e-mail non valide.")]
        /* solution pour index 'unique'*/
        [Column(TypeName = "VARCHAR")]
        [StringLength(80)]
        [Index(IsUnique = true)]
        public string AdresseMail { get; set; }
        [Required(ErrorMessage = "Vous devez saisir un mot de passe.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&=/])[A-Za-z\d@$!%*?&=/]{8,}$",
            ErrorMessage = "Le mot de passe doit être de minimum 8 caractères et contenir au moins une majuscule, un chiffre et un caractère spécial.")]
        public string MotDePasse { get; set; }

        public ICollection<Achat> Achats { get; set; }
    }
}