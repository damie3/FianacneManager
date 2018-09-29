namespace FinanceManager.Migrations
{
    using FinanceManager.Migrations.Helper;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FinanceManager.Models.Context.TransactionsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new CustomMySqlMigrationSqlGenerator());
    }

        protected override void Seed(FinanceManager.Models.Context.TransactionsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}