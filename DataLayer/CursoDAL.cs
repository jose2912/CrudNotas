using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using CrudNotas.EntityLayer;
using CrudNotas.CapaConexion;

namespace CrudNotas.DataLayer
{
    public class CursoDAL
    {
        private readonly Conexion _conexion;

        public CursoDAL(Conexion conexion)
        {
            _conexion = conexion;
        }

        // Listar cursos
        public List<Curso> GetCursos()
        {
            var cursos = new List<Curso>();

            using (var cn = _conexion.ConectarDB())
            {
                using (var cmd = new SqlCommand("sp_GetCursos", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cursos.Add(new Curso
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString()!,
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
            }

            return cursos;
        }

        // Insertar curso
        public void InsertCurso(Curso curso)
        {
            using (var cn = _conexion.ConectarDB())
            {
                using (var cmd = new SqlCommand("sp_InsertCurso", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", curso.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", curso.Descripcion ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        // Actualizar curso
        public void UpdateCurso(Curso curso)
        {
            using (var cn = _conexion.ConectarDB())
            {
                using (var cmd = new SqlCommand("sp_UpdateCurso", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", curso.Id);
                    cmd.Parameters.AddWithValue("@Nombre", curso.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", curso.Descripcion ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Eliminar curso
        public void DeleteCurso(int id)
        {
            using (var cn = _conexion.ConectarDB())
            {
                using (var cmd = new SqlCommand("sp_DeleteCurso", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
