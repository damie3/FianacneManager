namespace FinanceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                "dbo.TransactionPeriod",
                c => new
                    {
                        TransactionPeriodId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        PeriodStart = c.DateTime(nullable: false, precision: 0),
                        PeriodEnd = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.TransactionPeriodId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false, precision: 0),
                        Description = c.String(unicode: false),
                        Amount = c.Double(nullable: false),
                        Period_TransactionPeriodId = c.Int(),
                        TransactionCategory_TransactionCategoryId = c.Int(),
                        TransactionSource_TransactionSourceId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.TransactionPeriod", t => t.Period_TransactionPeriodId)
                .ForeignKey("dbo.TransactionCategory", t => t.TransactionCategory_TransactionCategoryId)
                .ForeignKey("dbo.TransactionSource", t => t.TransactionSource_TransactionSourceId)
                .Index(t => t.Period_TransactionPeriodId)
                .Index(t => t.TransactionCategory_TransactionCategoryId)
                .Index(t => t.TransactionSource_TransactionSourceId);
            
            CreateTable(
                "dbo.TransactionSource",
                c => new
                    {
                        TransactionSourceId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.TransactionSourceId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "TransactionSource_TransactionSourceId", "dbo.TransactionSource");
            DropForeignKey("dbo.Transaction", "TransactionCategory_TransactionCategoryId", "dbo.TransactionCategory");
            DropForeignKey("dbo.Transaction", "Period_TransactionPeriodId", "dbo.TransactionPeriod");
            DropIndex("dbo.TransactionSource", new[] { "Name" });
            DropIndex("dbo.Transaction", new[] { "TransactionSource_TransactionSourceId" });
            DropIndex("dbo.Transaction", new[] { "TransactionCategory_TransactionCategoryId" });
            DropIndex("dbo.Transaction", new[] { "Period_TransactionPeriodId" });
            DropIndex("dbo.TransactionPeriod", new[] { "Name" });
            DropIndex("dbo.TransactionCategory", new[] { "Name" });
            DropTable("dbo.TransactionSource");
            DropTable("dbo.Transaction");
            DropTable("dbo.TransactionPeriod");
            DropTable("dbo.TransactionCategory");
        }
    }
}
