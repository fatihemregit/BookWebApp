using BookWebApp.Data.Config;
using BookWebApp.Models.Auth;
using BookWebApp.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace BookWebApp.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {

        public DbSet<BookDto> BookDtos{ get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookDtoConfig());
            //safe delete olan verileri getirmiyorum
            modelBuilder.Entity<BookDto>().HasQueryFilter(bd => !bd.isDeleted);

            base.OnModelCreating(modelBuilder);

        }
    }
}
