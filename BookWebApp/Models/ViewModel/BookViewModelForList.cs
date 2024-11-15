using System.ComponentModel.DataAnnotations.Schema;

namespace BookWebApp.Models.ViewModel
{
    public class BookViewModelForList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Writer { get; set; }
        public decimal Price { get; set; }

    }
}
