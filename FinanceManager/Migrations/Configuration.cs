namespace FinanceManager.Migrations
{
    using FinanceManager.Migrations.Helper;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FinanceManager.Models.Context.FinanceManagerContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new CustomMySqlMigrationSqlGenerator());
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FinanceManager.Models.Context.FinanceManagerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
