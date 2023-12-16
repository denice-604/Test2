using Microsoft.EntityFrameworkCore;
using Test22.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Настройка связи с БД и контекстом 

builder.Services.AddDbContext<Test2Context>(options =>
options.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Test2; Integrated Security = True; "));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
