using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Item> Item { get; set; }
    public DbSet<Backpack> Backpack { get; set; }
    public DbSet<Character> Character { get; set; }
    public DbSet<Title> Title { get; set; }
    public DbSet<CharacterTitle> CharacterTitle { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Item>().HasData(new List<Item>
            {
                new Item {
                    IdItem = 1,
                    Name = "I1",
                    Weight = 3
                },
                new Item {
                    IdItem = 2,
                    Name = "I2",
                    Weight = 4

                }
            });

            modelBuilder.Entity<Title>().HasData(new List<Title>
            {
                new Title {
                    IdTitle = 1,
                    Name = "t1"
                },
                new Title {
                    IdTitle = 2,
                    Name = "t2"

                }
            });

            modelBuilder.Entity<Character>().HasData(new List<Character>
            {
                new Character
                {
                    IdCharacter = 1,
                    FirstName = "c1",
                    LastName               = "cl1",
                    CurrentWeight = 3,
                    MaxWeight = 6
                },
                new Character
                {
                    IdCharacter = 2,
                    FirstName = "c2",
                    LastName               = "cl2",
                    CurrentWeight = 2,
                    MaxWeight = 7
                },
                new Character
                {
                    IdCharacter = 3,
                    FirstName = "c3",
                    LastName               = "cl3",
                    CurrentWeight = 1,
                    MaxWeight = 20
                }
            });

            modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>
            {
                new CharacterTitle
                {
                    CharacterId = 1,
                    TitleId = 1,
                    AcquiredId = DateOnly.Parse("2024-05-28")
                },
                new CharacterTitle
                {
                    CharacterId = 2,
                    AcquiredId = DateOnly.Parse("2024-05-31"),
                    TitleId = 1
                    
                },
                new CharacterTitle
                {
                    CharacterId = 3,
                    TitleId = 2,
                    AcquiredId = DateOnly.Parse("2023-06-01")
                }
            });

            modelBuilder.Entity<Backpack>().HasData(new List<Backpack>
            {
                new Backpack
                {
                    CharacterId = 1,
                    ItemId = 1,
                    Amount = 3
                },
                new Backpack
                {
                    CharacterId = 1,
                    ItemId = 2,
                    Amount = 4
                },
                new Backpack
                {
                    CharacterId = 2,
                    ItemId = 1,
                    Amount = 5
                },
                new Backpack
                {
                    CharacterId = 3,
                    ItemId = 2,
                    Amount = 8
                }
            });
    }
}