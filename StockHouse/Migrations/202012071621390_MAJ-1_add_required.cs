namespace StockHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJ1_add_required : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AdresseMail", c => c.String(nullable: false));
            AddColumn("dbo.Users", "MotDePasse", c => c.String(nullable: false));
            AlterColumn("dbo.Magasins", "Nom", c => c.String(nullable: false));
            AlterColumn("dbo.Magasins", "Adresse", c => c.String(nullable: false));
            AlterColumn("dbo.Magasins", "Ville", c => c.String(nullable: false));
            AlterColumn("dbo.Produits", "Nom", c => c.String(nullable: false));
            AlterColumn("dbo.Pieces", "Nom", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Nom", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Nom", c => c.String());
            AlterColumn("dbo.Pieces", "Nom", c => c.String());
            AlterColumn("dbo.Produits", "Nom", c => c.String());
            AlterColumn("dbo.Magasins", "Ville", c => c.String());
            AlterColumn("dbo.Magasins", "Adresse", c => c.String());
            AlterColumn("dbo.Magasins", "Nom", c => c.String());
            DropColumn("dbo.Users", "MotDePasse");
            DropColumn("dbo.Users", "AdresseMail");
        }
    }
}
