using App08_ToDoWithDatabase.Components;
using App08_ToDoWithDatabase.Data;
using App08_ToDoWithDatabase.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Razor + Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add PostgreSQL via Npgsql EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register TodoService as scoped
builder.Services.AddScoped<TodoService>();

var app = builder.Build();

// Auto-create tables if they don't exist (optional, comment out if using SQL script)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();