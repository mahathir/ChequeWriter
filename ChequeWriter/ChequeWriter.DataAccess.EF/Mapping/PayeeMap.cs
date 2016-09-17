using ChequeWriter.DTO.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ChequeWriter.DataAccess.EF.Mapping
{
    public class PayeeMap : EntityTypeConfiguration<Payee>
    {
        public PayeeMap()
        {
            // Primary Key
            this.HasKey(t => t.PayeeID);

            // Properties
            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Payee");
            this.Property(t => t.PayeeID).HasColumnName("PayeeID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.Payees)
                .HasForeignKey(d => d.CustomerID);

        }
    }
}
