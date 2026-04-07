using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CrudNotas.CapaConexion
{
    public class Conexion
    {
        private readonly string _cadena;

        public Conexion(IConfiguration configuration)
        {
            _cadena = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection ConectarDB()
        {
            var cn = new SqlConnection(_cadena);
            cn.Open();
            return cn;
        }
    }
}
