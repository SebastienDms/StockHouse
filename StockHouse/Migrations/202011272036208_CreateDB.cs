namespace StockHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prix = c.Int(nullable: false),
                        Quantité = c.Int(nullable: false),
                        DateAchat = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        MagasinId = c.Int(nullable: false),
                        ProduitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Magasins", t => t.MagasinId, cascadeDelete: true)
                .ForeignKey("dbo.Produits", t => t.ProduitId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MagasinId)
                .Index(t => t.ProduitId);
            
            CreateTable(
                "dbo.Magasins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Adresse = c.String(),
                        Ville = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Stock = c.Int(nullable: false),
                        Type = c.String(),
                        Marque = c.String(),
                        PieceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pieces", t => t.PieceId, cascadeDelete: true)
                .Index(t => t.PieceId);
            
            CreateTable(
                "dbo.Pieces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Achats", "UserId", "dbo.Users");
            DropForeignKey("dbo.Achats", "ProduitId", "dbo.Produits");
            DropForeignKey("dbo.Produits", "PieceId", "dbo.Pieces");
            DropForeignKey("dbo.Achats", "MagasinId", "dbo.Magasins");
            DropIndex("dbo.Produits", new[] { "PieceId" });
            DropIndex("dbo.Achats", new[] { "ProduitId" });
            DropIndex("dbo.Achats", new[] { "MagasinId" });
            DropIndex("dbo.Achats", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Pieces");
            DropTable("dbo.Produits");
            DropTable("dbo.Magasins");
            DropTable("dbo.Achats");
        }
    }
}
