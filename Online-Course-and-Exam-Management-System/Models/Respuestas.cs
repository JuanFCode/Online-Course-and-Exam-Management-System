namespace Online_Course_and_Exam_Management_System.Models;

public partial class Respuestas
{
    public int Id { get; set; }

    public int? Respuesta { get; set; }

    public int? Pregunta { get; set; }

    public int? Porcentaje { get; set; }

    public virtual Preguntas? PreguntaNavigation { get; set; }

    public virtual ICollection<Respuestasexamenes> Respuestaexamen { get; set; } = new List<Respuestasexamenes>();
}
