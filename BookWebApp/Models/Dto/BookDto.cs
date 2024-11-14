﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BookWebApp.Models.Dto
{


    [Table("Book")]
    public class BookDto
    {

        public int Id { get; set; }


        public string Name { get; set; }

        public string Writer { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }


        /*
         Daha sonrasında kategori,Yazar(obje olarak),Isbn numarası eklenebilir
         */
    }
}
