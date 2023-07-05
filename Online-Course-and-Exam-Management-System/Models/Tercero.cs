namespace Online_Course_and_Exam_Management_System.Models;

public partial class Tercero
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public int? Pais { get; set; }

    public string Correoelectronico { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string? Tipo { get; set; }

    public virtual ICollection<Examenpresentado> Examenpresentados { get; set; } = new List<Examenpresentado>();

    public virtual Pai? PaisNavigation { get; set; }

    public virtual ICollection<Terceroscurso> Terceroscursos { get; set; } = new List<Terceroscurso>();

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
