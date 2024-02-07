namespace Online_Course_and_Exam_Management_System.Models;

public partial class Preguntabanco
{
    public int Id { get; set; }

    public int? Curso { get; set; }

    public string? Tema { get; set; }

    public string? Enunciado { get; set; }

    public string? Explicacion { get; set; }

    public virtual Cursos? CursoNavigation { get; set; }

    public virtual ICollection<Preguntas> Pregunta { get; set; } = new List<Preguntas>();
}
