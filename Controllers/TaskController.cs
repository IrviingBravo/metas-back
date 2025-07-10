using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using metas.Data;
using metas.Models;
using System;
using System.Threading.Tasks;
using metas.Models.Dtos;

namespace metas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        // POST api/taskitem
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("El título es obligatorio.");

            var task = new TaskItem
            {
                Title = dto.Title,
                ActivityListId = dto.ActivityListId,
                CreatedAt = DateTime.Now,
                Status = "Abierta",
                IsImportant = false
            };

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        // PUT api/taskitem/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItem updatedTask)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
                return NotFound();

            task.Title = updatedTask.Title ?? task.Title;
            task.Status = updatedTask.Status ?? task.Status;
            task.IsImportant = updatedTask.IsImportant;

            await _context.SaveChangesAsync();

            return Ok(task);
        }

        // DELETE api/taskitem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH api/taskitem/{id}/toggle-complete
        [HttpPatch("{id}/toggle-complete")]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
                return NotFound();

            task.Status = task.Status == "Completada" ? "Abierta" : "Completada";
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        // PATCH api/taskitem/{id}/toggle-important
        [HttpPatch("{id}/toggle-important")]
        public async Task<IActionResult> ToggleImportant(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
                return NotFound();

            task.IsImportant = !task.IsImportant;
            await _context.SaveChangesAsync();

            return Ok(task);
        }
    }
}
