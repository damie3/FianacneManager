namespace FinanceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Description = c.String(maxLength: 30, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.AccountId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.BudgetItem",
                c => new
                    {
                        BudgetItemId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 30, storeType: "nvarchar"),
                        Amount = c.Double(nullable: false),
                        Category_CategoryId = c.Int(nullable: false),
                        Period_PeriodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BudgetItemId)
                .ForeignKey("dbo.Category", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Period", t => t.Period_PeriodId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Period_PeriodId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Description = c.String(maxLength: 30, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.CategoryId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Period",
                c => new
                    {
                        PeriodId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        PeriodStart = c.DateTime(nullable: false, precision: 0),
                        PeriodEnd = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.PeriodId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false, precision: 0),
                        Description = c.String(maxLength: 30, storeType: "nvarchar"),
                        Amount = c.Double(nullable: false),
                        Account_AccountId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(nullable: false),
                        Period_PeriodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Account", t => t.Account_AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Period", t => t.Period_PeriodId, cascadeDelete: true)
                .Index(t => t.Account_AccountId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Period_PeriodId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "Period_PeriodId", "dbo.Period");
            DropForeignKey("dbo.Transaction", "Category_CategoryId", "dbo.Category");
            DropForeignKey("dbo.Transaction", "Account_AccountId", "dbo.Account");
            DropForeignKey("dbo.BudgetItem", "Period_PeriodId", "dbo.Period");
            DropForeignKey("dbo.BudgetItem", "Category_CategoryId", "dbo.Category");
            DropIndex("dbo.Transaction", new[] { "Period_PeriodId" });
            DropIndex("dbo.Transaction", new[] { "Category_CategoryId" });
            DropIndex("dbo.Transaction", new[] { "Account_AccountId" });
            DropIndex("dbo.Period", new[] { "Name" });
            DropIndex("dbo.Category", new[] { "Name" });
            DropIndex("dbo.BudgetItem", new[] { "Period_PeriodId" });
            DropIndex("dbo.BudgetItem", new[] { "Category_CategoryId" });
            DropIndex("dbo.Account", new[] { "Name" });
            DropTable("dbo.Transaction");
            DropTable("dbo.Period");
            DropTable("dbo.Category");
            DropTable("dbo.BudgetItem");
            DropTable("dbo.Account");
        }
    }
}
