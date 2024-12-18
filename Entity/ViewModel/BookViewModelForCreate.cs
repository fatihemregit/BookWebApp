using System.ComponentModel.DataAnnotations;

namespace Entity.ViewModel
{
    public class BookViewModelForCreate
    {
        public string Name { get; set; }

        public string Writer { get; set; }

        [Required(ErrorMessage = "Fiyat alanı boş bırakılamaz.")]
        [RegularExpression(@"^\d+,\d{1,2}$", ErrorMessage = "Fiyat virgülden sonra en fazla 2 basamak içermelidir.")]
        [Range(0.01, 999999.99, ErrorMessage = "Fiyat 0.01 ile 999999.99 arasında olmalıdır.")]
        public decimal Price { get; set; }
    }
}
