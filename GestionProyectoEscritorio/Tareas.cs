using System.Collections.Generic;

public class Tareas
{
    public string NombreTarea { get; set; }
    public List<string> Subtareas { get; set; }

    public Tareas() { }

    public Tareas(string nombreTarea, List<string> subtareas)
    {
        NombreTarea = nombreTarea;
        Subtareas = subtareas ?? new List<string>(); // Si Subtareas es null, inicializa como lista vacía
    }
}
