using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudNotas.EntityLayer
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        // Propiedad calculada desde el SP
        public int CantidadNotas { get; set; }

        // Relación inversa
        public List<Nota> Notas { get; set; } = new List<Nota>();
    }
}
