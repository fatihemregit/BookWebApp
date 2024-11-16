using System.ComponentModel.DataAnnotations;

namespace BookWebApp.Models.Auth
{
    public class RemoveRoleFromUser
    {


        [Display(Name = "Role Name")]
        public Guid RoleId { get; set; }


        [Display(Name = "User Name")]
        public Guid UserId { get; set; }

    }
}
