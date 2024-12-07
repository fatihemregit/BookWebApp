using System.ComponentModel.DataAnnotations;

namespace Entity.Auth
{
	public class CreateRoleViewModel
	{

		[Display(Name ="Role Name")]
        public string Name { get; set; }


    }
}
