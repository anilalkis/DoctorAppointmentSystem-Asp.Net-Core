using DoctorAppointmentSystem.Data.Abstract;
using DoctorAppointmentSystem.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IDoctorDal), typeof(EfCoreDoctorDal));
builder.Services.AddScoped(typeof(IPatientDal), typeof(EfCorePatientDal));
builder.Services.AddScoped(typeof(IAppointmentDal), typeof(EfCoreAppointmentDal));
builder.Services.AddScoped(typeof(IScheduleDal), typeof(EfCoreScheduleDal));


var app = builder.Build();

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

app.UseAuthorization();


//app.UseEndpoints(endpoints =>
//{


//    app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Patinet}/{action=Index}/{id?}");
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
