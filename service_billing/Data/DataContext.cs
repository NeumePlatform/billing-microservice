using System;

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
            optionsBuilder.UseSqlServer("Server=host.docker.internal,1433;Database=billing;User Id=sa;Password=Stefandeboer122599;TrustServerCertificate=true;");
        }

        public DbSet<Transaction> transactions { get; set; }
    }
}

