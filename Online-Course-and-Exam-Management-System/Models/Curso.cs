using System;
using System.Collections.Generic;

namespace Online_Course_and_Exam_Management_System.Models;

public partial class Curso
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Fabricante { get; set; }

    public DateTime? Fechadevencimiento { get; set; }

    public string? Estado { get; set; }

    public decimal Costo { get; set; }

    public DateTime? Duracion { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Examan> Examen { get; set; } = new List<Examan>();

    public virtual ICollection<Preguntabanco> Preguntabancos { get; set; } = new List<Preguntabanco>();

    public virtual ICollection<Terceroscurso> Terceroscursos { get; set; } = new List<Terceroscurso>();

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
