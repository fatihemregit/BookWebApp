namespace Entity.IAuthUserRepository
{
	public class IAuthUserRepositoryFindByEmailAsync
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }


        public IList<string> Roles { get; set; }//belki lazım olabilir


	}


}
