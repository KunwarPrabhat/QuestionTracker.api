using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionTracker.Api.Data;
using QuestionTracker.Api.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace QuestionTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuestionsController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public QuestionsController(ApplicationDbContext db)
    {
        _db = db;
    }

    // =========================
    // GET ALL QUESTIONS
    // GET: api/questions
    // =========================
    [HttpGet]
    public async Task<ActionResult<List<Question>>> GetAll()
    {
        int userId = GetCurrentUserId();

        return await _db.Questions
            .Where(q => q.UserId == userId)
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();
    }

    // =========================
    // GET SINGLE QUESTION
    // GET: api/questions/5
    // =========================
    [HttpGet("{id}")]
    public async Task<ActionResult<Question>> GetById(int id)
    {
        var question = await _db.Questions.FindAsync(id);

        if (question == null)
        {
            return NotFound();
        }

        return Ok(question);
    }

    // =========================
    // CREATE QUESTION
    // POST: api/questions
    // =========================
    [HttpPost]
    public async Task<ActionResult<Question>> Create(Question question)
    {
        question.UserId = GetCurrentUserId();
        question.CreatedAt = DateTime.UtcNow;

        _db.Questions.Add(question);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = question.Id }, question);
    }


    // =========================
    // UPDATE QUESTION
    // PUT: api/questions/5
    // =========================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Question updated)
    {
        if (id != updated.Id)
        {
            return BadRequest("ID mismatch");
        }

        var existing = await _db.Questions.FindAsync(id);

        if (existing == null)
        {
            return NotFound();
        }

        // Update allowed fields
        existing.Title = updated.Title;
        existing.LeetCodeUrl = updated.LeetCodeUrl;
        existing.Notes = updated.Notes;
        existing.IsCompleted = updated.IsCompleted;

        await _db.SaveChangesAsync();

        return NoContent();
    }

    // =========================
    // DELETE QUESTION
    // DELETE: api/questions/5
    // =========================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var question = await _db.Questions.FindAsync(id);

        if (question == null)
        {
            return NotFound();
        }

        _db.Questions.Remove(question);
        await _db.SaveChangesAsync();

        return NoContent();
    }
    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        return int.Parse(userIdClaim!.Value);
    }


}
