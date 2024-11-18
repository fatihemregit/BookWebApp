using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookWebApp.Models.Auth
{
    public class SetRoleViewModel
    {
        public bool State { get; set; }
		[Display(Name = "Role Name")]
		public string RoleName { get; set; }
    }
}
