namespace StockHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "AdresseMail", c => c.String(nullable: false, maxLength: 80, unicode: false));
            CreateIndex("dbo.Users", "AdresseMail", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "AdresseMail" });
            AlterColumn("dbo.Users", "AdresseMail", c => c.String(nullable: false));
        }
    }
}
