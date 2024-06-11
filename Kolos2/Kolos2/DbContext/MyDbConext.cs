using Kolokwium2.Models;
using Kolos2.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolos2.DbContext;

public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<Item> Items { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Backpack>()
            .HasKey(b => new { b.CharacterId, b.ItemId });

        modelBuilder.Entity<CharacterTitle>()
            .HasKey(ct => new { ct.CharacterId, ct.TitleId });

        modelBuilder.Entity<Item>().HasData(
            new Item { Id = 1, Name = "Sword", Weight = 10 },
            new Item { Id = 2, Name = "Shield", Weight = 15 },
            new Item { Id = 3, Name = "Potion", Weight = 1 }
        );

        modelBuilder.Entity<Character>().HasData(
            new Character { Id = 1, FirstName = "John", LastName = "Doe", CurrentWeight = 20, MaxWeight = 100 },
            new Character { Id = 2, FirstName = "Jane", LastName = "Doe", CurrentWeight = 10, MaxWeight = 80 }
        );

        modelBuilder.Entity<Title>().HasData(
            new Title { Id = 1, Name = "Warrior" },
            new Title { Id = 2, Name = "Mage" }
        );

        modelBuilder.Entity<Backpack>().HasData(
            new Backpack { CharacterId = 1, ItemId = 1, Amount = 1 },
            new Backpack { CharacterId = 1, ItemId = 3, Amount = 5 },
            new Backpack { CharacterId = 2, ItemId = 2, Amount = 1 }
        );

        modelBuilder.Entity<CharacterTitle>().HasData(
            new CharacterTitle { CharacterId = 1, TitleId = 1, AcquiredAt = DateTime.Now },
            new CharacterTitle { CharacterId = 2, TitleId = 2, AcquiredAt = DateTime.Now }
        );
    }
}