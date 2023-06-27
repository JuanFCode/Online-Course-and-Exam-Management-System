using System;
using System.Collections.Generic;

namespace Online_Course_and_Exam_Management_System.Models;

public partial class Examan
{
    public int Id { get; set; }

    public int? Curso { get; set; }

    public string? Modalidad { get; set; }

    public int? Maximopreguntas { get; set; }

    public DateTime? Tiempomaximo { get; set; }

    public int? Porcentajerespuestas { get; set; }

    public virtual Curso? CursoNavigation { get; set; }

    public virtual ICollection<Examenpresentado> Examenpresentados { get; set; } = new List<Examenpresentado>();

    public virtual ICollection<Preguntum> Pregunta { get; set; } = new List<Preguntum>();
}
