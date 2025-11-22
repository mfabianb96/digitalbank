namespace Capa_de_datos.Repository;

public class UsuarioRepository
{
    private readonly string _connectionString;

    public UsuarioRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Usuario> Consultar(int? id = null)
    {
        var lista = new List<Usuario>();

        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_Usuario_CRUD", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Accion", "SELECT");
            cmd.Parameters.AddWithValue("@Id", id ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Nombre", DBNull.Value);
            cmd.Parameters.AddWithValue("@FechaNacimiento", DBNull.Value);
            cmd.Parameters.AddWithValue("@Sexo", DBNull.Value);

            cn.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    lista.Add(new Usuario
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString()!,
                        FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                        Sexo = dr["Sexo"].ToString()!
                    });
                }
            }
        }

        return lista;
    }

    public void Agregar(Usuario usuario)
    {
        Ejecutar("INSERT", usuario);
    }

    public void Modificar(Usuario usuario)
    {
        Ejecutar("UPDATE", usuario);
    }

    public void Eliminar(int id)
    {
        Ejecutar("DELETE", new Usuario { Id = id });
    }

    private void Ejecutar(string operacion, Usuario usuario)
    {
        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_Usuario_CRUD", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Accion", operacion);
            cmd.Parameters.AddWithValue("@Id", usuario.Id == 0 ? DBNull.Value : usuario.Id);
            cmd.Parameters.AddWithValue("@Nombre", (object?)usuario.Nombre ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento == default ? DBNull.Value : usuario.FechaNacimiento);
            cmd.Parameters.AddWithValue("@Sexo", (object?)usuario.Sexo ?? DBNull.Value);

            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
