using Microsoft.EntityFrameworkCore;
using ProjectWithReact.DAL.Entities;

namespace ProjectWithReact.DAL.EF
{
    public sealed partial class DirectoryContext : DbContext
    {
	    public DirectoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Contacts> Contacts { get; set; }

	   protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("contacts_pkey");

                entity.ToTable("contacts");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Patronymic).HasColumnName("patronymic");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.Surname).HasColumnName("surname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
