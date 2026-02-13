using System.ComponentModel.DataAnnotations;

namespace QuestionTracker.Api.Models;

public class LeetCodeQuestion
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Slug { get; set; } = string.Empty;

    [Required]
    public string LeetCodeUrl { get; set; } = string.Empty;

    [Required]
    public string Difficulty { get; set; } = "Easy";

    [Required]
    public string Category { get; set; } = "SQL";

    public bool IsFree { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<UserQuestionProgress>? UserProgress { get; set; }
}
