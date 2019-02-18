using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityModels
{
    public partial class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tbikes> Tbikes { get; set; }
        public virtual DbSet<Tmodel> Tmodel { get; set; }
        public virtual DbSet<Tstores> Tstores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Store;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Tbikes>(entity =>
            {
                entity.ToTable("TBikes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Tbikes)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBikes_TModel");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Tbikes)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_TBikes_TStores");
            });

            modelBuilder.Entity<Tmodel>(entity =>
            {
                entity.ToTable("TModel");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Desc).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tstores>(entity =>
            {
                entity.ToTable("TStores");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });
        }
    }
}
