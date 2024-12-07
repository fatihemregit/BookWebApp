using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.IBookRepository
{
	public class EditOneBookById
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Writer { get; set; }

		public decimal Price { get; set; }
	}
}
