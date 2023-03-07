using Microsoft.EntityFrameworkCore;
using ProyectoERP.Data;
using ProyectoERP.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("SqlERP");
builder.Services.AddTransient<IRepo, RepositoryClientesPotencialesSql>();
builder.Services.AddDbContext<ErpContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();