using Data.EfCore.Config;
using Entity.Auth;
using Entity.Dto;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EfCore.Context
{
	public class ApplicationDbContext:IdentityDbContext<AppUser,AppRole,Guid>
	{
		public DbSet<BookDto> BookDtos { get; set; }

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
