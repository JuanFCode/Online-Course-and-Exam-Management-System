namespace Online_Course_and_Exam_Management_System.Models;

public partial class Respuestasexamenes
{
    public int Id { get; set; }

    public int? Examenpresentado { get; set; }

    public int? Preguntas { get; set; }

    public int? Respuestas { get; set; }

    public DateTime? Tiemporespuesta { get; set; }

    public int? Marcada { get; set; }

    public virtual Examenespresentados? ExamenpresentadoNavigation { get; set; }

    public virtual Preguntas? PreguntasNavigation { get; set; }

    public virtual Respuestas? RespuestasNavigation { get; set; }
}
