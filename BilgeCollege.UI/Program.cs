using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Services.Concretes;
using BilgeCollege.DAL.Context;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

// Context
builder.Services.AddDbContext<CollegeContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<User, UserRole>().AddEntityFrameworkStores<CollegeContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

// Injections
builder.Services.AddScoped(typeof(I_Repository<>), typeof(Repository<>));

builder.Services.AddScoped<I_AltTopicServiceManager, AltTopicServiceManager>();
builder.Services.AddScoped<I_ClassroomServiceManager, ClassroomServiceManager>();
builder.Services.AddScoped<I_GuardianServiceManager, GuardianServiceManager>();
builder.Services.AddScoped<I_MainTopicServiceManager, MainTopicServiceManager>();
builder.Services.AddScoped<I_StudentServiceManager, StudentServiceManager>();
builder.Services.AddScoped<I_TeacherServiceManager, TeacherServiceManager>();
builder.Services.AddScoped<I_DayScheduleServiceManager, DayScheduleServiceManager>();

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
