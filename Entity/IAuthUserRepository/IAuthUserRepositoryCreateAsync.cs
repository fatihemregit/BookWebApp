namespace Entity.IAuthUserRepository
{
	public class IAuthUserRepositoryCreateAsync
	{
		public string UserName { get; set; }
		public string Email { get; set; }

		public string SecurityStamp { get; set; }
		public string PasswordHash { get; set; }
	}


}
