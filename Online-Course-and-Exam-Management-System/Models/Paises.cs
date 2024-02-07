namespace Online_Course_and_Exam_Management_System.Models;

public partial class Paises
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Terceros> Terceros { get; set; } = new List<Terceros>();
}
