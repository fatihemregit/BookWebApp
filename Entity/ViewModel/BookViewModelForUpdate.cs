
namespace Entity.ViewModel
{
    public class BookViewModelForUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Writer { get; set; }

		//virgülden sonra iki basamak ayarı yapılacak
		public decimal Price { get; set; }
    }
}
