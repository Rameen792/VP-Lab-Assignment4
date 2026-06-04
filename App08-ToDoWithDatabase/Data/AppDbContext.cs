using App08_ToDoList.Models;
using Microsoft.EntityFrameworkCore;

namespace App08_ToDoList.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}