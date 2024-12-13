using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.IAuthUserRepository
{
	public class IAuthUserRepositoryRemoveFromRoleAsync
	{
		public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
    }
}
