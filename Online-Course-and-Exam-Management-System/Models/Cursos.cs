namespace Online_Course_and_Exam_Management_System.Models;

public partial class Cursos
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Fabricante { get; set; }

    public DateTime? Fechadevencimiento { get; set; }

    public string? Estado { get; set; }

    public decimal Costo { get; set; }

    public DateTime? Duracion { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Examenes> Examen { get; set; } = new List<Examenes>();

    public virtual ICollection<Preguntabanco> Preguntabancos { get; set; } = new List<Preguntabanco>();

    public virtual ICollection<Terceroscursos> Terceroscursos { get; set; } = new List<Terceroscursos>();

    public virtual ICollection<Transacciones> Transaccions { get; set; } = new List<Transacciones>();
}
