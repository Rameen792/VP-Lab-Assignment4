using App08_ToDoWithDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace App08_ToDoWithDatabase.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}