using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using CrudNotas.EntityLayer;
using CrudNotas.CapaConexion;

namespace CrudNotas.DataLayer
{
    public class NotaDAL
    {
        private readonly Conexion _conexion;

        public NotaDAL(Conexion conexion)
        {
            _conexion = conexion;
        }

        // Listar todas las notas con su curso
        public List<Nota> GetNotas()
        {
            var notas = new List<Nota>();

            using (var cn = _conexion.ConectarDB())
            {
                using (var cmd = new SqlCommand("sp_GetNotas", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            notas.Add(new Nota
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Titulo = dr["Titulo"].ToString()!,
                                Contenido = dr["Contenido"].ToString()!,
                                FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                                CursoId = Convert.ToInt32(dr["CursoId"]),

                                Curso = new Curso
                                {
                                    Id = Convert.ToInt32(dr["CursoIdReal"]),
                                    Nombre = dr["Curso"].ToString()!
                                }
                            });
                        }
                    }
                }
            }

            return notas;
        }


        // Insertar nueva nota
        public void InsertNota(Nota nota)
        {
            using (var cn = _conexion.ConectarDB())
            {
                using (var cmd = new SqlCommand("sp_InsertNota", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Titulo", nota.Titulo);
                    cmd.Parameters.AddWithValue("@Contenido", nota.Contenido);
                    cmd.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CursoId", nota.CursoId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Actualizar nota
        public void UpdateNota(Nota nota)
        {
            using (var cn = _conexion.ConectarDB())
            {
                using (var cmd = new SqlCommand("sp_UpdateNota", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", nota.Id);
                    cmd.Parameters.AddWithValue("@Titulo", nota.Titulo);
                    cmd.Parameters.AddWithValue("@Contenido", nota.Contenido);
                    cmd.Parameters.AddWithValue("@CursoId", nota.CursoId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Eliminar nota
        public void DeleteNota(int id)
        {
            using (var cn = _conexion.ConectarDB())
            {
                using (var cmd = new SqlCommand("sp_DeleteNota", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
