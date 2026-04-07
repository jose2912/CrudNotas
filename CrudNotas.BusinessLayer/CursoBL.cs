using CrudNotas.EntityLayer;
using CrudNotas.DataLayer;
using System.Collections.Generic;

namespace CrudNotas.BusinessLayer
{
    public class CursoBL
    {
        private readonly CursoDAL _cursoDAL;

        public CursoBL(CursoDAL cursoDAL)
        {
            _cursoDAL = cursoDAL;
        }

        // Listar cursos
        public List<Curso> ListarCursos()
        {
            return _cursoDAL.GetCursos();
        }

        // Obtener curso por Id
        public Curso ObtenerCurso(int id)
        {
            // Como tu DAL no tiene un método específico, lo buscamos en la lista
            return _cursoDAL.ObtenerCurso(id);
        }

        // Insertar curso
        public void InsertarCurso(Curso curso)
        {
            _cursoDAL.InsertCurso(curso);
        }

        // Actualizar curso
        public void ActualizarCurso(Curso curso)
        {
            _cursoDAL.UpdateCurso(curso);
        }

        // Eliminar curso
        public void EliminarCurso(int id)
        {
            _cursoDAL.DeleteCurso(id);
        }
    }
}
