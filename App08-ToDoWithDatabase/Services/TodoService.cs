using App08_ToDoWithDatabase.Data;
using App08_ToDoWithDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace App08_ToDoWithDatabase.Services;

public class TodoService
{
    private readonly AppDbContext _db;

    public TodoService(AppDbContext db)
    {
        _db = db;
    }

    // READ ALL
    public async Task<List<TodoTask>> GetAllAsync()
    {
        return await _db.TodoTasks
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    // CREATE
    public async Task<TodoTask> CreateAsync(string title, string priority = "Medium")
    {
        var task = new TodoTask
        {
            Title = title.Trim(),
            Priority = priority,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.TodoTasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    // UPDATE COMPLETION
    public async Task ToggleCompleteAsync(int id)
    {
        var task = await _db.TodoTasks.FindAsync(id);
        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
            task.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }

    // UPDATE TITLE
    public async Task UpdateTitleAsync(int id, string newTitle)
    {
        var task = await _db.TodoTasks.FindAsync(id);
        if (task != null)
        {
            task.Title = newTitle.Trim();
            task.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }

    // DELETE
    public async Task DeleteAsync(int id)
    {
        var task = await _db.TodoTasks.FindAsync(id);
        if (task != null)
        {
            _db.TodoTasks.Remove(task);
            await _db.SaveChangesAsync();
        }
    }

    // STATS
    public async Task<(int Total, int Completed, int Pending)> GetStatsAsync()
    {
        var total = await _db.TodoTasks.CountAsync();
        var completed = await _db.TodoTasks.CountAsync(t => t.IsCompleted);
        return (total, completed, total - completed);
    }
}