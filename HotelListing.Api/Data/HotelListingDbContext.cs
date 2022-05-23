using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                    new Country
                    {
                        Id = 1,
                        Name = "Jamaica",
                        ShortName = "JM"
                    },
                    new Country
                    {
                        Id = 2,
                        Name = "Bahamas",
                        ShortName = "BS"
                    },
                    new Country
                    {
                        Id = 3,
                        Name = "Cayman Islabd",
                        ShortName = "CI"
                    }
                );

            modelBuilder.Entity<Hotel>().HasData(
              new Hotel
              {
                  Id = 1,
                  Name = "Santals Resort and Span",
                  Address = "Negril",
                  CountryId = 1,
                  Rating = 4.5
              },
               new Hotel
               {
                   Id = 2,
                   Name = "Comfort Suites",
                   Address = "George Town",
                   CountryId = 3,
                   Rating = 4.3
               },
            new Hotel
            {
                Id = 3,
                Name = "Grand Palldium",
                Address = "Nassua",
                CountryId = 3,
                Rating = 4
            }
          );
        }

    }
}
