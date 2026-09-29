using Microsoft.Data.SqlClient;

namespace SistemaBarbearia
{
    public static class Conexao
    {
        private static readonly string StringConexao =
            @"Server=localhost\SQLEXPRESS;Database=BarbeariaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection Abrir()
        {
            var conn = new SqlConnection(StringConexao);
            conn.Open();
            return conn;
        }
    }
}