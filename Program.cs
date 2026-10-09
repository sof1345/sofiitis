using Microsoft.EntityFrameworkCore;
using HeroesWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// REGISTRO DEL CONTEXTO (Esto es lo que te faltaba):
builder.Services.AddDbContext<HeroesContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HeroesDb")
        ?? throw new InvalidOperationException("Falta la conexión HeroesDb.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;
    response.ContentType = "text/html; charset=utf-8";
    var codigo = response.StatusCode;
    await response.WriteAsync($"""
        <!DOCTYPE html>
        <html lang="es">
        <head>
            <meta charset="utf-8" />
            <title>{codigo}</title>
        </head>
        <body style="font-family: Segoe UI, sans-serif; margin: 2rem;">
            <h1>{codigo}</h1>
            <p>No se encontró el héroe.</p>
        </body>
        </html>
        """);
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();