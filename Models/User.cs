using System.ComponentModel.DataAnnotations;

namespace QuestionTracker.Api.Models;

public class User 
{
    public int Id { get; set; }

    [Required]
    public string Email { get; set; } = string.Empty;

    // For now plain text (later we hash + auth properly)
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public List<Question> Questions { get; set; } = new();

    public bool IsEmailVerified { get; set; } = false;

    public string? EmailVerificationCode { get; set; }

    public DateTime? EmailVerificationExpiry { get; set; }

}
