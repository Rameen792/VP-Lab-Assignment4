using App08_ToDoWithDatabase.Components;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
    using var db = factory.CreateDbContext();
    try
    {
        // EnsureCreated() silently does nothing when the DB already exists
        // (even if the Todos table is missing). Use raw SQL to guarantee
        // the table is always present regardless of prior DB state.
        db.Database.ExecuteSqlRaw("""
            CREATE TABLE IF NOT EXISTS "Todos" (
                "Id"          SERIAL        PRIMARY KEY,
                "Title"       TEXT          NOT NULL,
                "IsCompleted" BOOLEAN       NOT NULL DEFAULT FALSE,
                "CreatedAt"   TIMESTAMPTZ   NOT NULL DEFAULT NOW()
            );
            """);
        Console.WriteLine("✅ Database connected and table verified!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ DB Error: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();