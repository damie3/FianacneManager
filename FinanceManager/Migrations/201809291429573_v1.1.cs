namespace FinanceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transaction", "Period_PeriodId", c => c.Int());
            CreateIndex("dbo.Transaction", "Period_PeriodId");
            AddForeignKey("dbo.Transaction", "Period_PeriodId", "dbo.Period", "PeriodId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "Period_PeriodId", "dbo.Period");
            DropIndex("dbo.Transaction", new[] { "Period_PeriodId" });
            DropColumn("dbo.Transaction", "Period_PeriodId");
        }
    }
}
