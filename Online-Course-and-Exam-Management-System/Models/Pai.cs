namespace Online_Course_and_Exam_Management_System.Models;

public partial class Pai
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Tercero> Terceros { get; set; } = new List<Tercero>();
}
