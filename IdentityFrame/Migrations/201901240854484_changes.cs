namespace IdentityFrame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Studios", newName: "Studio");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Studio", newName: "Studios");
        }
    }
}
