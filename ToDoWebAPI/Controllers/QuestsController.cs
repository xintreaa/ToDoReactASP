using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ToDoWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestsController : ControllerBase
{
    private readonly ToDoDbContext _context; // Це контекст бази даних

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Quest>>> GetQuests()
    {
        return await _context.Quests.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Quest>> GetQuest(int id)
    {
        var quest = await _context.Quests.FindAsync(id);
        if (quest == null) return NotFound();
        return quest;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuest(int id, Quest quest)
    {
        // quest - це новий об'єкт, який ми отримали від клієнта
        if (id != quest.Id) return BadRequest();
        var existingQuest = await _context.Quests.FindAsync(id);
        if (existingQuest == null) return NotFound();

        existingQuest.Name = quest.Name;
        existingQuest.Description = quest.Description;
        existingQuest.IsComplete = quest.IsComplete;
        existingQuest.DueDate = quest.DueDate;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Quests.Any(e => e.Id == id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Quest>> CreateQuest(Quest quest)
    {
        // Встановлюємо дату створення
        quest.DueDate = DateTime.UtcNow;

        _context.Quests.Add(quest);
        await _context.SaveChangesAsync();

        // Повертаємо створений об'єкт із кодом 201 і URL для доступу до нього
        return CreatedAtAction(nameof(GetQuest), new { id = quest.Id }, quest);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuest(int id)
    {
        // Шукаємо об'єкт за ідентифікатором
        var quest = await _context.Quests.FindAsync(id);

        if (quest == null) return NotFound();

        _context.Quests.Remove(quest);
        await _context.SaveChangesAsync();
        // Повертаємо код 204 (успішне видалення)
        return NoContent();
    }
}