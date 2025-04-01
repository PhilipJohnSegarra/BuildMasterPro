using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BuildMasterPro.Data;
using BuildMasterPro.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace BuildMasterPro.Services
{
    public class UserService : RepositoryBased<ApplicationUser>
    {
        IDbContextFactory<ApplicationDbContext> _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        public ApplicationUser? CurrentUser { get; set; }
        private ApplicationUser _cachedUser = new();
        public UserService(IDbContextFactory<ApplicationDbContext> db, UserManager<ApplicationUser> userManager, ProtectedSessionStorage sessionStorage, AuthenticationStateProvider authStateProvider) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _sessionStorage = sessionStorage;
            _authStateProvider = authStateProvider;

            Task.Run(async () => await SetCurrentUserAsync());
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            var result = await GetAllAsync(i => i.IsDeleted.Equals(false));
            return result;
        }

        public async Task<ApplicationUser> Get(string id)
        {
            var result = await GetAsync(o => o.Id == id);
            return result;
        }

        public async Task<ApplicationUser> Add(ApplicationUser user)
        {
            var result = await AddAsync(user);
            return result;
        }

        public async Task Update(ApplicationUser oldEntity, ApplicationUser newEntity)
        {
            await UpdateAsync(oldEntity, newEntity);
        }
        public async Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }
        public async Task SetCurrentUserAsync()
        {
            
            //var serializedProject = JsonSerializer.Serialize(user);
            //await _sessionStorage.SetAsync("CurrentUser", serializedProject);

            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var User = authState.User;
            if (User.Identity.IsAuthenticated)
            {
                _cachedUser = await _userManager.GetUserAsync(User);
                var serializedUser = JsonSerializer.Serialize(_cachedUser);
                await _sessionStorage.SetAsync("CurrentUser", serializedUser);
            }

        }
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            //var authState = await _authStateProvider.GetAuthenticationStateAsync();
            //var User = authState.User;

            //if (User.Identity.IsAuthenticated)
            //{
            //    _cachedUser = await _userManager.GetUserAsync(User);

            //}
            //return _cachedUser;

            var result = await _sessionStorage.GetAsync<string>("CurrentUser");
            if (result.Success && !string.IsNullOrEmpty(result.Value))
            {
                // 🔹 Deserialize back to a Project object
                CurrentUser = JsonSerializer.Deserialize<ApplicationUser>(result.Value);
                return CurrentUser;
            }
            return new ApplicationUser();
        }
    }
}
