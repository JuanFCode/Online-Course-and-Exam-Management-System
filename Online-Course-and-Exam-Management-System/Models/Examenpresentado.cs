using System;
using System.Collections.Generic;

namespace Online_Course_and_Exam_Management_System.Models;

public partial class Examenpresentado
{
    public int Id { get; set; }

    public int Tercero { get; set; }

    public int? Examen { get; set; }

    public DateTime? Fechainicio { get; set; }

    public DateTime? Fechafinal { get; set; }

    public int? Ultimapreguntarespondida { get; set; }

    public string? Estadoexamen { get; set; }

    public virtual Examan? ExamenNavigation { get; set; }

    public virtual ICollection<Respuestaexaman> Respuestaexamen { get; set; } = new List<Respuestaexaman>();

    public virtual Tercero TerceroNavigation { get; set; } = null!;
}
