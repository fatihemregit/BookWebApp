using BookWebApp.Data.Config;
using BookWebApp.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace BookWebApp.Data.Context
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<BookDto> BookDtos{ get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookDtoConfig());
        }
    }
}
