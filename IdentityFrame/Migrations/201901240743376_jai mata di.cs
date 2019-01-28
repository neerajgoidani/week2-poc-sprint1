namespace IdentityFrame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jaimatadi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RolesId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RolesId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.EmployeeRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Studios",
                c => new
                    {
                        StudioId = c.Int(nullable: false, identity: true),
                        StudioName = c.String(),
                        StudioDescription = c.String(),
                        TechDirector = c.String(),
                    })
                .PrimaryKey(t => t.StudioId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Role = c.String(),
                        Age = c.Int(nullable: false),
                        Salary = c.Double(nullable: false),
                        StudioName = c.String(),
                        StudioId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Studios", t => t.StudioId, cascadeDelete: true)
                .Index(t => t.StudioId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.EmployeeClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EmployeeLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Employee", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "StudioId", "dbo.Studios");
            DropForeignKey("dbo.EmployeeRole", "UserId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeLogin", "UserId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeClaims", "UserId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeRole", "RoleId", "dbo.Roles");
            DropIndex("dbo.EmployeeLogin", new[] { "UserId" });
            DropIndex("dbo.EmployeeClaims", new[] { "UserId" });
            DropIndex("dbo.Employee", "UserNameIndex");
            DropIndex("dbo.Employee", new[] { "StudioId" });
            DropIndex("dbo.EmployeeRole", new[] { "RoleId" });
            DropIndex("dbo.EmployeeRole", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropTable("dbo.EmployeeLogin");
            DropTable("dbo.EmployeeClaims");
            DropTable("dbo.Employee");
            DropTable("dbo.Studios");
            DropTable("dbo.EmployeeRole");
            DropTable("dbo.Roles");
        }
    }
}
