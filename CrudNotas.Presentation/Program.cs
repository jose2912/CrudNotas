using CrudNotas.CapaConexion;
using CrudNotas.DataLayer;
using CrudNotas.BusinessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registro de dependencias para las capas
builder.Services.AddSingleton<Conexion>();   // La conexión se mantiene única
builder.Services.AddTransient<NotaDAL>();    // DAL de notas
builder.Services.AddTransient<NotaBL>();     // BL de notas

builder.Services.AddTransient<CursoDAL>();   // DAL de cursos
builder.Services.AddTransient<CursoBL>();    // BL de cursos

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
