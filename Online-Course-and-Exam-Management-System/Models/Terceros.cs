namespace Online_Course_and_Exam_Management_System.Models;

public partial class Terceros
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public int? Pais { get; set; }

    public string Correoelectronico { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string? Tipo { get; set; }

    public virtual ICollection<Examenespresentados> Examenpresentados { get; set; } = new List<Examenespresentados>();

    public virtual Paises? PaisNavigation { get; set; }

    public virtual ICollection<Terceroscursos> Terceroscursos { get; set; } = new List<Terceroscursos>();

    public virtual ICollection<Transacciones> Transaccions { get; set; } = new List<Transacciones>();
}
