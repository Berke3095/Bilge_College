using BilgeCollege.DAL.Context;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Context
builder.Services.AddDbContext<CollegeContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<User, UserRole>().AddEntityFrameworkStores<CollegeContext>();

// Injections
builder.Services.AddScoped(typeof(I_Repository<>), typeof(Repository<>));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

    endpoints.MapControllerRoute(
           name: "default",
           pattern: "{controller=Home}/{action=Index}/{id?}"
         );
});

app.Run();
