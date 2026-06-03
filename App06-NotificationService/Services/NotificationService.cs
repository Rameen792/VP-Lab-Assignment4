using App06_NotificationService.Models;

namespace App06_NotificationService.Services;

public class NotificationService
{
    private readonly NotificationConfig _config;

    private static readonly string[] _icons = { "🔔", "📢", "⚡", "🛡️", "💬", "📊", "🚀", "🔥", "✅", "⚠️" };

    private static readonly string[] _titles = {
        "System Update Available",
        "New Message Received",
        "Security Alert Triggered",
        "Deployment Completed",
        "Report Generated",
        "User Login Detected",
        "Backup Successful",
        "Performance Warning",
        "Task Assigned to You",
        "Subscription Renewed"
    };

    private static readonly string[] _details = {
        "Version 4.2.1 is ready to install. Schedule a maintenance window.",
        "You have an unread message from the DevOps team.",
        "Unusual login attempt detected from IP 192.168.1.45.",
        "Production build v3.1.0 deployed successfully to cloud.",
        "Monthly analytics report is ready for download.",
        "Admin user logged in from a new device.",
        "All 3 backup jobs completed with no errors.",
        "CPU usage exceeded 85% threshold for 5 minutes.",
        "Review pull request #204 before end of sprint.",
        "Your Pro plan has been auto-renewed for another year."
    };

    public NotificationService(NotificationConfig config)
    {
        _config = config;
    }

    public async Task<List<NotificationItem>> GetNotificationsAsync(int? numberOfNotifications = null)
    {
        await Task.Delay(300); // simulate async fetch

        int count = numberOfNotifications ?? _config.DefaultNumberOfNotifications;
        count = Math.Clamp(count, 1, 10);

        var random = new Random();
        var items = new List<NotificationItem>();

        for (int i = 0; i < count; i++)
        {
            int idx = random.Next(_titles.Length);
            items.Add(new NotificationItem
            {
                Id = i + 1,
                Icon = _icons[idx],
                Title = _titles[idx],
                Detail = _details[idx],
                TimeAgo = $"{random.Next(1, 59)} min ago",
                IsRead = random.Next(2) == 0,
                Priority = (NotificationPriority)random.Next(3)
            });
        }

        return items;
    }
}

public class NotificationItem
{
    public int Id { get; set; }
    public string Icon { get; set; } = "";
    public string Title { get; set; } = "";
    public string Detail { get; set; } = "";
    public string TimeAgo { get; set; } = "";
    public bool IsRead { get; set; }
    public NotificationPriority Priority { get; set; }
}

public enum NotificationPriority { Low, Medium, High }