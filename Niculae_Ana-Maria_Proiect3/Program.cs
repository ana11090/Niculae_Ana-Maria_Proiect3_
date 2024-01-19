using Microsoft.EntityFrameworkCore;
using Niculae_Ana_Maria_Proiect3.Data;
using Niculae_Ana_Maria_Proiect3.Hubs;
using Microsoft.AspNetCore.Identity;
using Niculae_Ana_Maria_Proiect3.Data.Niculae_Ana_Maria_Proiect3.Data;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityContext>();
//builder.Services.AddDbContext<IdentityContext>(options =>

//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSignalR();
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

})
.AddEntityFrameworkStores<IdentityContext>();
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("DirectorPolicy", policy =>
//        policy.RequireClaim("Functie", "Director"));

//    options.AddPolicy("HRPolicy", policy =>
//        policy.RequireClaim("Functie", "HR"));

//    Define other policies for Designer, Analist, etc.
//});
builder.Services.AddAuthorization(opts => {
    opts.AddPolicy("DirectorPolicy", policy => {
        policy.RequireClaim("Functie", "Director");
    });

    opts.AddPolicy("HRPolicy", policy => {
        policy.RequireClaim("Functie", "HR");
    });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DbInitializer.Initialize(services);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/Chat");
app.MapRazorPages();
app.Run();
