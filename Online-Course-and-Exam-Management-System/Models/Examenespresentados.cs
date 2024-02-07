namespace Online_Course_and_Exam_Management_System.Models;

public partial class Examenespresentados
{
    public int Id { get; set; }

    public int? Tercero { get; set; }

    public int? Examen { get; set; }

    public DateTime? Fechainicio { get; set; }

    public DateTime? Fechafinal { get; set; }

    public int? Ultimapreguntarespondida { get; set; }

    public string? Estadoexamen { get; set; }

    public virtual Examenes? ExamenNavigation { get; set; }

    public virtual ICollection<Respuestasexamenes> Respuestaexamen { get; set; } = new List<Respuestasexamenes>();

    public virtual Terceros? TerceroNavigation { get; set; }
}
