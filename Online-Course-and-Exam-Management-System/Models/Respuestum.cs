namespace Online_Course_and_Exam_Management_System.Models;

public partial class Respuestum
{
    public int Id { get; set; }

    public int? Respuesta { get; set; }

    public int? Pregunta { get; set; }

    public int? Porcentaje { get; set; }

    public virtual Preguntum? PreguntaNavigation { get; set; }

    public virtual ICollection<Respuestaexaman> Respuestaexamen { get; set; } = new List<Respuestaexaman>();
}
