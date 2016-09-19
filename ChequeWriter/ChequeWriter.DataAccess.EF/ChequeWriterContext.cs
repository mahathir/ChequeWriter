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

#if DEBUG
            Database.SetInitializer(new DropCreateSeedDatabaseIfModelChanges());
#else
            Database.SetInitializer<ChequeWriterContext>(new CreateDatabaseIfNotExists<ChequeWriterContext>());
#endif
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
