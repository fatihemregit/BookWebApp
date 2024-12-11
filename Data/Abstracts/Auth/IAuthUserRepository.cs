using Entity.Auth;
using Entity.IAuthUserRepository;
using Microsoft.AspNetCore.Identity;

namespace Data.Abstracts.Auth
{
    public interface IAuthUserRepository
	{

		Task<List<IAuthUserRepositoryGetAllUsersAsync>?> GetAllUsersAsync();
		Task<IAuthUserRepositoryFindByEmailAsync?> FindByEmailAsync(string email);
		Task<IAuthUserRepositoryFindByNameAsync?> FindByNameAsync(string userName);

		Task<bool> IsInRoleAsync(IAuthUserRepositoryIsInRoleAsync user,string roleName);

		Task<IList<string>?> GetRolesAsync(IAuthUserRepositoryGetRolesAsync user);

		Task<IdentityResult> CreateAsync(IAuthUserRepositoryCreateAsync user, string password);
		Task<IdentityResult> DeleteAsync(IAuthUserRepositoryDeleteAsync user);
		Task AddToRoleAsync(IAuthUserRepositoryAddToRoleAsync user,string roleName);

		Task RemoveFromRoleAsync(IAuthUserRepositoryRemoveFromRoleAsync user, string roleName);

		Task<List<IAuthUserRepositoryGetUsersInRoleAsync>?> GetUsersInRoleAsync(string roleName);


    }


}