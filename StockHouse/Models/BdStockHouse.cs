using System.Data.Entity;

namespace StockHouse.Models
{
    public class BdStockHouse : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Magasin> Magasins { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Achat> Achats { get; set; }

        public BdStockHouse()
            :base("name=BdStockHouse")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*    Configuration Fluent API One to Many tables: Produit, Magasin et User tabel de liaison: Achat      */
            // Config Magasin->Achat
            modelBuilder.Entity<Achat>()
                .HasRequired<Magasin>(s => s.Magasin)
                .WithMany(g => g.Achats)
                .HasForeignKey<int>(s => s.MagasinId);

            // Config User->Achat
            modelBuilder.Entity<Achat>()
                .HasRequired<User>(s => s.User)
                .WithMany(g => g.Achats)
                .HasForeignKey(s => s.UserId);

            // Config Produit->Achat
            modelBuilder.Entity<Achat>()
                .HasRequired<Produit>(s => s.Produit)
                .WithMany(g => g.Achats)
                .HasForeignKey(s => s.ProduitId);
        }
    }
}