namespace CrudNotas.EntityLayer
{
    public class Nota
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        // Relación con Curso
        public int CursoId { get; set; }
        public Curso? Curso { get; set; }
    }
}
