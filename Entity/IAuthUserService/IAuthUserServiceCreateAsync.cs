namespace Entity.IAuthUserService
{
	public class IAuthUserServiceCreateAsync
	{
		public string UserName { get; set; }
		public string Email { get; set; }

		public string SecurityStamp { get; set; }
		public string PasswordHash { get; set; }
	}

}
