using Datos.Models;
using Datos.Config;
using System.Data;
using System.Data.SqlClient;

namespace Datos.Repository
{
    public class UsuarioRepository
    {
        private readonly string _connectionString = Conexion.ConnectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Consultar todos o uno por Id
        public List<Usuario> Consultar(int? id = null)
        {
            var lista = new List<Usuario>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("usp_Users_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", 'R');
                cmd.Parameters.AddWithValue("@Id", id.HasValue ? id.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Nombre", DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaNacimiento", DBNull.Value);
                cmd.Parameters.AddWithValue("@Sexo", DBNull.Value);

                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Usuario
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString(),
                        FechaNacimiento = (DateTime)reader["FechaNacimiento"],
                        Sexo = reader["Sexo"].ToString()
                    });
                }
            }

            return lista;
        }

        // Agregar usuario
        public int Agregar(Usuario usuario)
        {
            int newId = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("usp_Users_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", 'C');
                cmd.Parameters.AddWithValue("@Id", DBNull.Value);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo ?? (object)DBNull.Value);

                conn.Open();
                newId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return newId;
        }

        // Modificar usuario
        public int Modificar(Usuario usuario)
        {
            int affectedRows = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("usp_Users_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", 'U');
                cmd.Parameters.AddWithValue("@Id", usuario.Id);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo ?? (object)DBNull.Value);

                conn.Open();
                affectedRows = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return affectedRows;
        }

        // Eliminar usuario
        public int Eliminar(int id)
        {
            int affectedRows = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("usp_Users_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", 'D');
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Nombre", DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaNacimiento", DBNull.Value);
                cmd.Parameters.AddWithValue("@Sexo", DBNull.Value);

                conn.Open();
                affectedRows = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return affectedRows;
        }

    }
}
