namespace TM.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitTMDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catalogs",
                c => new
                    {
                        CatalogId = c.Int(nullable: false, identity: true),
                        CatalogName = c.String(nullable: false, maxLength: 30),
                        CatalogOrder = c.Int(nullable: false),
                        CatalogCode = c.String(nullable: false, maxLength: 50),
                        IsMenu = c.Boolean(nullable: false),
                        MenuControllerName = c.String(maxLength: 50),
                        IconClass = c.String(maxLength: 50),
                        ParentCatalogId = c.Int(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.CatalogId)
                .ForeignKey("dbo.Catalogs", t => t.ParentCatalogId)
                .Index(t => t.CatalogCode, unique: true)
                .Index(t => t.ParentCatalogId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 30),
                        Account = c.String(nullable: false, maxLength: 20),
                        Password = c.String(),
                        Email = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Account, unique: true);
            
            CreateTable(
                "dbo.RoleCatalogs",
                c => new
                    {
                        Role_RoleId = c.Int(nullable: false),
                        Catalog_CatalogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_RoleId, t.Catalog_CatalogId })
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Catalogs", t => t.Catalog_CatalogId, cascadeDelete: true)
                .Index(t => t.Role_RoleId)
                .Index(t => t.Catalog_CatalogId);
            
            CreateTable(
                "dbo.UserCatalogs",
                c => new
                    {
                        User_UserId = c.String(nullable: false, maxLength: 128),
                        Catalog_CatalogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Catalog_CatalogId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Catalogs", t => t.Catalog_CatalogId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Catalog_CatalogId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_UserId = c.String(nullable: false, maxLength: 128),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Role_RoleId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Role_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.UserCatalogs", "Catalog_CatalogId", "dbo.Catalogs");
            DropForeignKey("dbo.UserCatalogs", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RoleCatalogs", "Catalog_CatalogId", "dbo.Catalogs");
            DropForeignKey("dbo.RoleCatalogs", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.Catalogs", "ParentCatalogId", "dbo.Catalogs");
            DropIndex("dbo.UserRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.UserRoles", new[] { "User_UserId" });
            DropIndex("dbo.UserCatalogs", new[] { "Catalog_CatalogId" });
            DropIndex("dbo.UserCatalogs", new[] { "User_UserId" });
            DropIndex("dbo.RoleCatalogs", new[] { "Catalog_CatalogId" });
            DropIndex("dbo.RoleCatalogs", new[] { "Role_RoleId" });
            DropIndex("dbo.Users", new[] { "Account" });
            DropIndex("dbo.Catalogs", new[] { "ParentCatalogId" });
            DropIndex("dbo.Catalogs", new[] { "CatalogCode" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserCatalogs");
            DropTable("dbo.RoleCatalogs");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Catalogs");
        }
    }
}
