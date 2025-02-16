using System.Collections.Generic;
using System;

public class Proyecto
{
    public string NombreProyecto { get; set; }
    public List<Tareas> Tareas { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public List<string> Usuarios { get; set; }

    public Proyecto() { }
}
