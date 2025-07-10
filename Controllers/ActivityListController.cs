using Microsoft.AspNetCore.Mvc;
using metas.Models;
using metas.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace metas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityListController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActivityListController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLists()
        {
            var lists = await _context.ActivityLists
                .Include(l => l.Tasks)
                .Select(l => new
                {
                    l.Id,
                    l.Name,
                    l.CreatedAt,
                    TotalTasks = l.Tasks.Count,
                    CompletedTasks = l.Tasks.Count(t => t.Status == "Completada"),
                    ProgressPercentage = l.Tasks.Count == 0 ? 0 : (int)((double)l.Tasks.Count(t => t.Status == "Completada") / l.Tasks.Count * 100),
                    Tasks = l.Tasks.Select(t => new {
                        t.Id,
                        t.Title,
                        t.Status,
                        t.CreatedAt,
                        t.IsImportant
                    })
                })
                .ToListAsync();

            return Ok(lists);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActivityList list)
        {
            if (string.IsNullOrWhiteSpace(list.Name) || list.Name.Length > 80)
                return BadRequest("Nombre inválido.");

            var exists = await _context.ActivityLists.AnyAsync(l => l.Name == list.Name);
            if (exists)
                return Conflict("Ya existe una meta con ese nombre.");

            list.CreatedAt = DateTime.Now;
            list.Tasks = new List<TaskItem>(); 
            _context.ActivityLists.Add(list);
            await _context.SaveChangesAsync();

            return Ok(list);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var activityList = await _context.ActivityLists
                    .Include(a => a.Tasks) // importante si usas delete en cascada
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (activityList == null)
                    return NotFound(); // 404

                _context.ActivityLists.Remove(activityList);
                await _context.SaveChangesAsync();

                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                // Muy útil para ver qué exactamente está fallando
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


    }
}
