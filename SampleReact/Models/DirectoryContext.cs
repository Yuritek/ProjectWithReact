using Microsoft.EntityFrameworkCore;

namespace SampleReact.Models
{
    public partial class DirectoryContext : DbContext
    {
	   public DirectoryContext()
	   {
	   }

	   public DirectoryContext(DbContextOptions<DirectoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contacts> Contacts { get; set; }

	   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	   {
		  if (!optionsBuilder.IsConfigured)
		  {
			 optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Directory;Username=postgres;Password=postgres");
		  }
	   }

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
