using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using proOfficeTask.Helpers;

namespace proOfficeTask.Models
{
    public partial class ProOfficeTaskDBContext : DbContext
    {
        private readonly IOptions<ConnectionStrings> _connString;

        public ProOfficeTaskDBContext()
        {
        }

        public ProOfficeTaskDBContext(DbContextOptions<ProOfficeTaskDBContext> options, IOptions<ConnectionStrings> connString)
            : base(options)
        {
            _connString = connString;
        }

        public virtual DbSet<TblDocument> TblDocument { get; set; }
        public virtual DbSet<TblDocumentData> TblDocumentData { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProOfficeTaskDB;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(_connString.Value.DefaultConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblDocument>(entity =>
            {
                entity.HasKey(e => e.IdDoc);

                entity.ToTable("tblDocument");

                entity.Property(e => e.IdDoc).ValueGeneratedNever();

                entity.Property(e => e.Downloaded).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblDocument)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDocument_tblProduct");
            });

            modelBuilder.Entity<TblDocumentData>(entity =>
            {
                entity.HasKey(e => e.IdDocument);

                entity.ToTable("tblDocumentData");

                entity.Property(e => e.IdDocument).ValueGeneratedNever();

                entity.HasOne(d => d.IdDocumentNavigation)
                    .WithOne(p => p.TblDocumentData)
                    .HasForeignKey<TblDocumentData>(d => d.IdDocument)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDocumentData_tblDocument");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("tblProduct");

                entity.Property(e => e.IdProduct).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.SupplierName).HasMaxLength(250);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });
        }
    }
}
