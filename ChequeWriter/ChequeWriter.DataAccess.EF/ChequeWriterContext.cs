using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ChequeWriter.DTO.Models;
using ChequeWriter.DataAccess.EF.Mapping;

namespace ChequeWriter.DataAccess.EF
{
    public partial class ChequeWriterContext : DbContext
    {
        static ChequeWriterContext()
        {
            Database.SetInitializer<ChequeWriterContext>(null);
        }

        public ChequeWriterContext()
            : base("Name=ChequeWriterContext")
        {
        }

        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payee> Payees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ChequeMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new PayeeMap());
        }
    }
}
