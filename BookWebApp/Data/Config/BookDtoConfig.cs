using BookWebApp.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWebApp.Data.Config
{
    public class BookDtoConfig : IEntityTypeConfiguration<BookDto>
    {
        public void Configure(EntityTypeBuilder<BookDto> builder)
        {
            //migrate edildiğinde hazır data gelmesini sağlama
            builder.HasData(
                new BookDto { Id = 1, Name = "Sefiller", Writer = "Victor Hugo", Price = 100 },
                new BookDto { Id = 2, Name = "Suç ve Ceza", Writer = "Fyodor Dostoevsky", Price = 150 },
                new BookDto { Id = 3, Name = "Karamazov Kardeşler", Writer = "Fyodor Dostoevsky", Price = 150 },
                new BookDto { Id = 4, Name = "Tutunamayanlar", Writer = "Oğuz Atay", Price = 150 },
                new BookDto { Id = 5, Name = "Sinekli Bakkal", Writer = "Halide Edip Adıvar", Price = 150 }
               );

        }
    }
}
