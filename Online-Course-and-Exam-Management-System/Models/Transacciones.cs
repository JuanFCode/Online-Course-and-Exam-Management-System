namespace Online_Course_and_Exam_Management_System.Models;

public partial class Transacciones
{
    public int Id { get; set; }

    public int? Tercero { get; set; }

    public int? Curso { get; set; }

    public DateTime? Fechacompra { get; set; }

    public string? Metodopago { get; set; }

    public string? Datallesadicionales { get; set; }

    public int? Cupos { get; set; }

    public string? Codigo { get; set; }

    public virtual Cursos? CursoNavigation { get; set; }

    public virtual Terceros? TerceroNavigation { get; set; }

    public virtual ICollection<Terceroscursos> Terceroscursos { get; set; } = new List<Terceroscursos>();
}
