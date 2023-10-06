using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;
using Preschool.Services;
using Preschool.Services.EntitiesServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options
    .UseLazyLoadingProxies()
    .UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()// in order to make the rols in the website you have to add this
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();





//Add Services
builder.Services.AddScoped<IChildService, ChildService>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IAttendanceService,AttendanceService >();
builder.Services.AddScoped<IAssetsService,AssetsService>();
builder.Services.AddScoped<IExpenseSevice, ExpenseService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IStorgeChildService, StorgeChildService>();
builder.Services.AddScoped<IStorgeAssetService, StorgeAssetService>();
builder.Services.AddScoped<IStorgeAttendanceService, StorgeAttendanceService>();
builder.Services.AddScoped<IStorgeClassroomService, StorgeClassroomService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
