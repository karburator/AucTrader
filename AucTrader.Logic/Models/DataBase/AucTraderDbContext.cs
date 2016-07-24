using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace AucTrader.Logic.Models.DataBase
{
    public class AucTraderDbContext : DbContext
    {
        public AucTraderDbContext() : base("AucTraderDbContext")
        {
        }

        public DbSet<Position> Positions { get; set; }
        public DbSet<AppLog> AppLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}