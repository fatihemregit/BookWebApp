﻿namespace Entity.IAuthUserService
{
	public class IAuthUserServiceGetAllUsersAsync
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }

		public IList<string> Roles { get; set; }//belki lazım olabilir

	}

}
