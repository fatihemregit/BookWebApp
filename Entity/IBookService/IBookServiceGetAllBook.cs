using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.IBookService
{
	public class IBookServiceGetAllBook
	{
        public int Id { get; set; }

        public string Name { get; set; }
		public string Writer { get; set; }
		public decimal Price { get; set; }
	}
}
