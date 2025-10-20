using Microsoft.EntityFrameworkCore;

namespace SchoolAPI.Models
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
    
    public DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>().HasData(
                new School
                {
                    Id = 1,
                    Name = "ENISo",
                    Sections = "IA, GTE, GMP, MECA, EI",
                    Director = "Najwa Sokkri",
                    Rating = 3.5,
                    WebSite = "https://eniso.rnu.tn/"
                });
            modelBuilder.Entity<School>().HasData(
                new School
                {
                    Id = 2,
                    Name = "ENIM",
                    Sections = "Textile, Mécanique, Electronique, Energitique",
                    Director = "Mohsen Abid",
                    Rating = 3.3,
                    WebSite = "https://enim.rnu.tn/"
                });
            modelBuilder.Entity<School>().HasData(
                new School
                {
                    Id = 3,
                    Name = "ENICAR",
                    Sections = "Infotronique, Informatique, Mecatronique",
                    Director = "Yosri Farhat",
                    Rating = 3.4,
                    
                }
                );

        }
    }
}
