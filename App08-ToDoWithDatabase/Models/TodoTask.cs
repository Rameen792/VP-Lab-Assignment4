using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App08_ToDoWithDatabase.Models;

[Table("todo_tasks")]
public class TodoTask
{
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [Required]
    [MaxLength(500)]
    public string Title { get; set; } = string.Empty;

    [Column("is_completed")]
    public bool IsCompleted { get; set; } = false;

    [Column("priority")]
    public string Priority { get; set; } = "Medium"; // Low / Medium / High

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}