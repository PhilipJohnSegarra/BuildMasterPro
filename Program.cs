using BuildMasterPro.Components;
using BuildMasterPro.Components.Account;
using BuildMasterPro.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using BuildMasterPro.Components.Layout;
using BuildMasterPro.Services;
using Blazored.LocalStorage;
using MongoDB.Driver;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAntiforgery();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddSyncfusionBlazor();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF1cWGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEBjWn5acXVRTmFfUkB1Vw==");
builder.Services.AddRazorPages();
builder.Services.AddSignalR(e => {
    e.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10MB
});
builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        options.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10MB
    });
builder.Services.AddSyncfusionBlazor();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ProjectTaskService>();
builder.Services.AddSingleton<MongoService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<ChannelService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProjectUserService>();
builder.Services.AddScoped<TaskUserService>();
builder.Services.AddScoped<TaskActivityService>();
builder.Services.AddScoped<ResourceService>();
builder.Services.AddScoped<EquipmentService>();
builder.Services.AddScoped<TaskActivityImagesService>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddSingleton<BlobStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
