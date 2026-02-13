using Microsoft.EntityFrameworkCore;
using QuestionTracker.Api.Models;

namespace QuestionTracker.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Question> Questions { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<LeetCodeQuestion> LeetCodeQuestions { get; set; }
    public DbSet<UserQuestionProgress> UserQuestionProgresses { get; set; }

}
