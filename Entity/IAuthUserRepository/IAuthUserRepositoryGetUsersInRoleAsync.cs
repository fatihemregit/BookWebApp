﻿namespace Entity.IAuthUserRepository
{
    public class IAuthUserRepositoryGetUsersInRoleAsync
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

		public string SecurityStamp { get; set; }
		public string PasswordHash { get; set; }
	}
}