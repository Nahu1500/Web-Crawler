namespace Web_Crawler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Keywords", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Keywords");
        }
    }
}
