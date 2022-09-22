using Microsoft.EntityFrameworkCore;
using Tourist.Domain;

namespace Tourist.Infrastructure;
public class TouristDbContext : DbContext
{
    public TouristDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity => 
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.HasData(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Инекс Олгица",
                    Address = "bb"
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Хотел Тино",
                    Address = "кеј Македонија бр. 56"
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Хотел Лебед",
                    Address = "кеј Македонија бр. 87"
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Хотел Гарден",
                    Address = "кеј Македонија бр. 89"
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Хотел Милениум Палас",
                    Address = "кеј Македонија бр. 80"
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Вила Жика",
                    Address = "кеј Македонија бр. 66"
                }
            );
        });
    }
}