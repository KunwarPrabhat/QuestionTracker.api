using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionTracker.Api.Data;
using QuestionTracker.Api.Models;
using System.Security.Claims;

namespace QuestionTracker.Api.Controllers;

[ApiController]
[Route("api/leetcode-questions")]
[Authorize]
public class LeetCodeQuestionsController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public LeetCodeQuestionsController(ApplicationDbContext db)
    {
        _db = db;
    }

    // =========================
    // GET SQL 50
    // GET: api/leetcode-questions/sql
    // =========================
    [HttpGet("sql")]
    public async Task<ActionResult> GetSql50()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var questions = await _db.LeetCodeQuestions
            .Where(q => q.Category == "SQL")
            .OrderBy(q => q.Id)
            .Select(q => new
            {
                q.Id,
                q.Title,
                q.Slug,
                q.LeetCodeUrl,
                q.Difficulty,
                q.Category,
                q.IsFree,
                q.CreatedAt,
                userProgress = q.UserProgress!
                    .Where(up => up.UserId == userId)
                    .Select(up => new { up.Id, up.UserId, up.LeetCodeQuestionId, up.IsCompleted, up.Notes, up.LastUpdatedAt })
            })
            .ToListAsync();

        return Ok(questions);
    }

    public class SaveSolutionDto
    {
        public string? solution { get; set; }
    }

    // POST: api/leetcode-questions/{id}/solution
    [HttpPost("{id}/solution")]
    public async Task<ActionResult> SaveSolution(int id, [FromBody] SaveSolutionDto dto)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var question = await _db.LeetCodeQuestions.FindAsync(id);
        if (question == null) return NotFound();

        var progress = await _db.UserQuestionProgresses
            .FirstOrDefaultAsync(p => p.UserId == userId && p.LeetCodeQuestionId == id);

        if (progress == null)
        {
            progress = new UserQuestionProgress
            {
                UserId = userId,
                LeetCodeQuestionId = id,
                IsCompleted = true,
                Notes = dto?.solution ?? string.Empty,
                LastUpdatedAt = DateTime.UtcNow
            };
            _db.UserQuestionProgresses.Add(progress);
        }
        else
        {
            progress.Notes = dto?.solution ?? progress.Notes;
            progress.IsCompleted = true;
            progress.LastUpdatedAt = DateTime.UtcNow;
            _db.UserQuestionProgresses.Update(progress);
        }

        await _db.SaveChangesAsync();

        // Return the question including only this user's progress entry
        var updated = await _db.LeetCodeQuestions
            .Where(q => q.Id == id)
            .Select(q => new
            {
                q.Id,
                q.Title,
                q.Slug,
                q.LeetCodeUrl,
                q.Difficulty,
                q.Category,
                q.IsFree,
                q.CreatedAt,
                userProgress = q.UserProgress!
                    .Where(up => up.UserId == userId)
                    .Select(up => new { up.Id, up.UserId, up.LeetCodeQuestionId, up.IsCompleted, up.Notes, up.LastUpdatedAt })
            })
            .FirstOrDefaultAsync();

        return Ok(updated);
    }

    // DELETE: api/leetcode-questions/{id}/solution
    [HttpDelete("{id}/solution")]
    public async Task<ActionResult> DeleteSolution(int id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var progress = await _db.UserQuestionProgresses
            .FirstOrDefaultAsync(p => p.UserId == userId && p.LeetCodeQuestionId == id);

        if (progress == null)
            return NotFound("Solution not found");

        _db.UserQuestionProgresses.Remove(progress);
        await _db.SaveChangesAsync();

        return Ok(new { message = "Solution deleted" });
    }
}
