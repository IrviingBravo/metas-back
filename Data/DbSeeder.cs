using System;
using System.Linq;
using metas.Models;
using System.Collections.Generic;

namespace metas.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.ActivityLists.Any())
            {
                var lista1 = new ActivityList
                {
                    Name = "Lista de Proyecto Laravel",
                    CreatedAt = DateTime.Now,
                    Tasks = new List<TaskItem>
                    {
                        new TaskItem { Title = "Diseñar estructura", Status = "Completada" },
                        new TaskItem { Title = "Conectar API .NET", Status = "Completada" },
                        new TaskItem { Title = "Crear vista en Laravel", Status = "Pendiente" }
                    }
                };

                var lista2 = new ActivityList
                {
                    Name = "Lista Personal",
                    CreatedAt = DateTime.Now,
                    Tasks = new List<TaskItem>
                    {
                        new TaskItem { Title = "Ir al súper", Status = "Pendiente" },
                        new TaskItem { Title = "Estudiar .NET", Status = "Completada" },
                        new TaskItem { Title = "Revisar tareas Laravel", Status = "Pendiente" }
                    }
                };

                context.ActivityLists.AddRange(lista1, lista2);
                context.SaveChanges();
            }
        }
    }
}
