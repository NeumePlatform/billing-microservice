using System;
using TransactionModel;

namespace service_billing.Data
{
	public class DataContext : DbContext
	{
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) :base(options)
		{

		}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=172.19.0.3,1433;Database=billing;User Id=sa;Password=Stefandeboer122599;");
            //optionsBuilder.UseSqlServer("Server=localhost,1433;Database=billing;User Id=sa;Password=Stefandeboer122599;TrustServerCertificate=true;");
        }

        public DbSet<TransactionModel.Transaction> transactions { get; set; }
    }
}

