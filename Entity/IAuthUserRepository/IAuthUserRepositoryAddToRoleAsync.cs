namespace Entity.IAuthUserRepository
{
	public class IAuthUserRepositoryAddToRoleAsync
	{
		public Guid Id { get; set; }

		public string SecurityStamp { get; set; }
		public string PasswordHash { get; set; }
	}
}