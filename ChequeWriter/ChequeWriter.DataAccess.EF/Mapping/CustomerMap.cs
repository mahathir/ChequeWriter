using ChequeWriter.DTO.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ChequeWriter.DataAccess.EF.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerID);

            // Properties
            this.Property(t => t.CustomerNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.ContactNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Customer");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.CustomerNo).HasColumnName("CustomerNo");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.ContactNo).HasColumnName("ContactNo");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
