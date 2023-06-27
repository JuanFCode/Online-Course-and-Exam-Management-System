using System;
using System.Collections.Generic;

namespace Online_Course_and_Exam_Management_System.Models;

public partial class Preguntum
{
    public int Id { get; set; }

    public int? Examen { get; set; }

    public int? Preguntabanco { get; set; }

    public int? Valortotal { get; set; }

    public virtual Examan? ExamenNavigation { get; set; }

    public virtual Preguntabanco? PreguntabancoNavigation { get; set; }

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();

    public virtual ICollection<Respuestaexaman> Respuestaexamen { get; set; } = new List<Respuestaexaman>();
}
