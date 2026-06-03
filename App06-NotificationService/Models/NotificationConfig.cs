namespace App06_NotificationService.Models;

public class NotificationConfig
{
    public int DefaultNumberOfNotifications { get; set; } = 5;
    public string NotificationStyle { get; set; } = "Compact"; // "Compact" or "Detailed"
}