using System;
using System.Collections.Generic;

namespace Online_Course_and_Exam_Management_System.Models;

public partial class Transaccion
{
    public int Id { get; set; }

    public int Tercero { get; set; }

    public int? Curso { get; set; }

    public DateTime? Fechacompra { get; set; }

    public string? Metodopago { get; set; }

    public string? Datallesadicionales { get; set; }

    public int? Cupos { get; set; }

    public string? Codigo { get; set; }

    public virtual Curso? CursoNavigation { get; set; }

    public virtual Tercero TerceroNavigation { get; set; } = null!;

    public virtual ICollection<Terceroscurso> Terceroscursos { get; set; } = new List<Terceroscurso>();
}
