using BTW.Domain.Entities;
using BTW.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BTW.Application.Context;

public class AppDbContext : DbContext
{
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<DocumentLog> DocumentLogs => Set<DocumentLog>();
    public DbSet<DocumentStatusHistory> DocumentStatusHistories { get; set; }
    public DbSet<DocumentItem> DocumentItems => Set<DocumentItem>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(x => x.Cufe);

            entity.Property(x => x.LegalNumber)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(x => x.Type)
                .HasConversion<int>();

            entity.Property(x => x.Status)
                .HasConversion<int>();

            entity.OwnsMany(x => x.Items, items =>
            {
                items.WithOwner().HasForeignKey("DocumentId");

                items.Property<int>("Id");
                items.HasKey("Id");

                items.Property(x => x.Description).IsRequired();
                items.Property(x => x.Quantity);
                items.Property(x => x.UnitValue);
                items.Property(x => x.Subtotal);
            });
        });
    }
}
