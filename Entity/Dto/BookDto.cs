using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    [Table("Book")]
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Writer { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public bool isDeleted { get; set; } = false;

        /*
         Daha sonrasında kategori,Yazar(obje olarak),Isbn numarası eklenebilir
        buradaki yapmış olacağın değişikleri gerekiyor sa viewmodel de yapmayı unutma
         */
    }
}
