using System.ComponentModel.DataAnnotations;

namespace BookWebApp.Models.Auth
{
	public class CreateRoleViewModel
	{

		[Display(Name ="Role Name")]
        public string Name { get; set; }


    }
}
