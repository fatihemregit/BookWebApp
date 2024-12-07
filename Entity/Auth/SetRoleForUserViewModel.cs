using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entity.Auth
{
    public class SetRoleForUserViewModel
    {
        public bool State { get; set; }
		[Display(Name = "Role Name")]
		public string RoleName { get; set; }
    }
}
