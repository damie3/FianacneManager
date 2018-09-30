namespace FinanceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.AccountId)
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
                "dbo.TransactionCategory",
                c => new
                    {
                        TransactionCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.TransactionCategoryId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false, precision: 0),
                        Description = c.String(unicode: false),
                        Amount = c.Double(nullable: false),
                        Account_AccountId = c.Int(),
                        TransactionCategory_TransactionCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Account", t => t.Account_AccountId)
                .ForeignKey("dbo.TransactionCategory", t => t.TransactionCategory_TransactionCategoryId)
                .Index(t => t.Account_AccountId)
                .Index(t => t.TransactionCategory_TransactionCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "TransactionCategory_TransactionCategoryId", "dbo.TransactionCategory");
            DropForeignKey("dbo.Transaction", "Account_AccountId", "dbo.Account");
            DropIndex("dbo.Transaction", new[] { "TransactionCategory_TransactionCategoryId" });
            DropIndex("dbo.Transaction", new[] { "Account_AccountId" });
            DropIndex("dbo.TransactionCategory", new[] { "Name" });
            DropIndex("dbo.Period", new[] { "Name" });
            DropIndex("dbo.Account", new[] { "Name" });
            DropTable("dbo.Transaction");
            DropTable("dbo.TransactionCategory");
            DropTable("dbo.Period");
            DropTable("dbo.Account");
        }
    }
}
