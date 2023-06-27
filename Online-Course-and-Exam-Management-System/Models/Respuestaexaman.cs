using System;
using System.Collections.Generic;

namespace Online_Course_and_Exam_Management_System.Models;

public partial class Respuestaexaman
{
    public int Id { get; set; }

    public int? Examenpresentado { get; set; }

    public int? Preguntas { get; set; }

    public int? Respuestas { get; set; }

    public DateTime? Tiemporespuesta { get; set; }

    public int? Marcada { get; set; }

    public virtual Examenpresentado? ExamenpresentadoNavigation { get; set; }

    public virtual Preguntum? PreguntasNavigation { get; set; }

    public virtual Respuestum? RespuestasNavigation { get; set; }
}
