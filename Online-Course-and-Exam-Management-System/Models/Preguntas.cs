namespace Online_Course_and_Exam_Management_System.Models;

public partial class Preguntas
{
    public int Id { get; set; }

    public int? Examen { get; set; }

    public int? Preguntabanco { get; set; }

    public int? Valortotal { get; set; }

    public virtual Examenes? ExamenNavigation { get; set; }

    public virtual Preguntasbancos? PreguntabancoNavigation { get; set; }

    public virtual ICollection<Respuestas> Respuesta { get; set; } = new List<Respuestas>();

    public virtual ICollection<Respuestasexamenes> Respuestaexamen { get; set; } = new List<Respuestasexamenes>();
}
