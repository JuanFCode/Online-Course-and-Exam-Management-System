namespace Online_Course_and_Exam_Management_System.Models;

public partial class Terceroscursos
{
    public int Id { get; set; }

    public int? Transaccion { get; set; }

    public int? Tercero { get; set; }

    public int? Curso { get; set; }

    public DateTime? Fechaactivacion { get; set; }

    public DateTime? Fechafinal { get; set; }

    public virtual Cursos? CursoNavigation { get; set; }

    public virtual Terceros? TerceroNavigation { get; set; }

    public virtual Transacciones? TransaccionNavigation { get; set; }
}
