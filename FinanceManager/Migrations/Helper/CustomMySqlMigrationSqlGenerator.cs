using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Text;
using System.Web;
using MySql.Data.EntityFramework;
namespace FinanceManager.Migrations.Helper
{
    public class CustomMySqlMigrationSqlGenerator : MySqlMigrationSqlGenerator
    {
        protected override MigrationStatement Generate(CreateIndexOperation op)
        {
            StringBuilder stringBuilder = new StringBuilder().Append("CREATE ");
            if (op.IsUnique)
                stringBuilder.Append("UNIQUE ");
            object obj1;
            op.AnonymousArguments.TryGetValue("Sort", out obj1);
            string sortOrder = obj1 == null || !(obj1.ToString() == "Ascending") ? "DESC" : "ASC";
            stringBuilder.AppendFormat("index  `{0}` on `{1}` (", (object)op.Name, (object)this.TrimSchemaPrefix(op.Table));
            stringBuilder.Append(string.Join(",", op.Columns.Select<string, string>((Func<string, string>)(c => "`" + c + "` " + sortOrder))) + ") ");
            
            return new MigrationStatement()
            {
                Sql = stringBuilder.ToString()
            };
        }
        private string TrimSchemaPrefix(string table)
        {
            if (table.StartsWith("dbo.") || table.Contains("dbo."))
                return table.Replace("dbo.", "");
            return table;
        }
    }
}