using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace metas.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Abierta"; // "Abierta" o "Completada"

        public bool IsImportant { get; set; } = false;

        // Relación con ActivityList
        [ForeignKey("ActivityList")]
        public int ActivityListId { get; set; }

        public ActivityList? ActivityList { get; set; }
    }
}
