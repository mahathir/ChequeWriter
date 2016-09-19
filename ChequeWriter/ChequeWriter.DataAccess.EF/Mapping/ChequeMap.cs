using ChequeWriter.DTO.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ChequeWriter.DataAccess.EF.Mapping
{
    public class ChequeMap : EntityTypeConfiguration<Cheque>
    {
        public ChequeMap()
        {
            // Primary Key
            this.HasKey(t => t.ChequeID);

            // Properties
            this.Property(t => t.ChequeNo)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.Memo)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Cheque");
            this.Property(t => t.ChequeID).HasColumnName("ChequeID");
            this.Property(t => t.ChequeNo).HasColumnName("ChequeNo");
            this.Property(t => t.PostingDate).HasColumnName("PostingDate");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.PayeeID).HasColumnName("PayeeID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Memo).HasColumnName("Memo");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.Cheques)
                .HasForeignKey(d => d.CustomerID)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.Payee)
                .WithMany(t => t.Cheques)
                .HasForeignKey(d => d.PayeeID)
                .WillCascadeOnDelete(false);

        }
    }
}
