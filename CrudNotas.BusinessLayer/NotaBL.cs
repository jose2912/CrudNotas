using System;
using System.Collections.Generic;
using CrudNotas.EntityLayer;
using CrudNotas.DataLayer;

namespace CrudNotas.BusinessLayer
{
    public class NotaBL
    {
        private readonly NotaDAL _notaDAL;

        public NotaBL(NotaDAL notaDAL)
        {
            _notaDAL = notaDAL;
        }

        // Listar todas las notas
        public List<Nota> ListarNotas()
        {
            return _notaDAL.GetNotas();
        }

        // Obtener una nota por Id
        public Nota ObtenerNota(int id)
        {
            return _notaDAL.GetNotas().Find(n => n.Id == id);
        }

        // Insertar nueva nota
        public void InsertarNota(Nota nota)
        {
            if (string.IsNullOrWhiteSpace(nota.Titulo))
                throw new ArgumentException("El título no puede estar vacío.");

            _notaDAL.InsertNota(nota);
        }

        // Actualizar nota existente
        public void ActualizarNota(Nota nota)
        {
            if (nota.Id <= 0)
                throw new ArgumentException("Id inválido para actualizar la nota.");

            _notaDAL.UpdateNota(nota);
        }

        // Eliminar nota
        public void EliminarNota(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id inválido para eliminar la nota.");

            _notaDAL.DeleteNota(id);
        }
    }
}
