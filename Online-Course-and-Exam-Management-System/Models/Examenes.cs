namespace Online_Course_and_Exam_Management_System.Models;

public partial class Examenes
{
    public int Id { get; set; }

    public int? Curso { get; set; }

    public string? Modalidad { get; set; }

    public int? Maximopreguntas { get; set; }

    public DateTime? Tiempomaximo { get; set; }

    public int? Porcentajerespuestas { get; set; }

    public virtual Cursos? CursoNavigation { get; set; }

    public virtual ICollection<Examenespresentados> Examenpresentados { get; set; } = new List<Examenespresentados>();

    public virtual ICollection<Preguntas> Pregunta { get; set; } = new List<Preguntas>();
}
