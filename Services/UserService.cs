using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BuildMasterPro.Data;

namespace BuildMasterPro.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(string email, string password)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IList<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.DeleteAsync(user);
        }
    }
}
