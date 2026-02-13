using System.ComponentModel.DataAnnotations;

namespace QuestionTracker.Api.Models;

public class UserQuestionProgress
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    public User? User { get; set; }

    [Required]
    public int LeetCodeQuestionId { get; set; }

    public LeetCodeQuestion? LeetCodeQuestion { get; set; }

    public bool IsCompleted { get; set; } = false;

    public string? Notes { get; set; }

    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
}
