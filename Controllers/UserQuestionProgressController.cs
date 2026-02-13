using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionTracker.Api.Data;
using QuestionTracker.Api.Models;
using System.Security.Claims;

namespace QuestionTracker.Api.Controllers;

[ApiController]
[Route("api/user-question-progress")]
[Authorize]
public class UserQuestionProgressController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public UserQuestionProgressController(ApplicationDbContext db)
    {
        _db = db;
    }

    // =========================
    // TOGGLE COMPLETE
    // POST: api/user-question-progress/{leetCodeQuestionId}/toggle-complete
    // =========================
    [HttpPost("{leetCodeQuestionId}/toggle-complete")]
    public async Task<IActionResult> ToggleComplete(int leetCodeQuestionId)
    {
        int userId = GetCurrentUserId();

        var progress = await _db.UserQuestionProgresses
            .FirstOrDefaultAsync(p =>
                p.UserId == userId &&
                p.LeetCodeQuestionId == leetCodeQuestionId);

        if (progress == null)
        {
            progress = new UserQuestionProgress
            {
                UserId = userId,
                LeetCodeQuestionId = leetCodeQuestionId,
                IsCompleted = true
            };

            _db.UserQuestionProgresses.Add(progress);
        }
        else
        {
            progress.IsCompleted = !progress.IsCompleted;
            progress.LastUpdatedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();

        return NoContent();
    }

    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        return int.Parse(userIdClaim!.Value);
    }
}
