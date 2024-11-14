using System.ComponentModel.DataAnnotations;

namespace BookWebApp.Models.ViewModel
{
    public class BookViewModelForUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Writer { get; set; }
        public decimal Price { get; set; }
    }
}
