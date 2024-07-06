using Eventos.Models;
using System;
using System.Data;
using System.Data.SQLite;
namespace csharp_Sqlite
{
    public class DalEvento
    {
        private static SQLiteConnection sqliteConnection;
        public const string DatabasePath = @"c:\dados\Eventos.sqlite";
        public DalEvento()
        { }
        public static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=c:\\dados\\Eventos.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }
        public static void CriarBancoSQLite()
        {
            try
            {
                string directory = Path.GetDirectoryName(DatabasePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                SQLiteConnection.CreateFile(DatabasePath); 
            }
            catch
            {
                throw;
            }
        }
        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Evento(
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome VARCHAR(50),
                    descricao VARCHAR(80),
                    ativo BOOLEAN,
                    prazo_inscricao DATETIME,
                    prazo_submissao DATETIME
                )";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoverTabelaSQLite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS Evento";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel remover a tabela", ex);
            }
        }

        public static DataTable GetEventos()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Evento";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int Adicionar(Evento evento)
        {
            try
            {
                using (var connection = DalEvento.DbConnection())
                {
                    string query = "INSERT INTO Evento (Nome, descricao, ativo, prazo_inscricao, prazo_submissao) VALUES (@Nome, @Descricao, @Ativo, @PrazoInscricao, @PrazoSubmissao)";
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@Nome", evento.Nome);
                    command.Parameters.AddWithValue("@Descricao", evento.Descricao);
                    command.Parameters.AddWithValue("@Ativo", evento.Ativo);
                    command.Parameters.AddWithValue("@PrazoInscricao", evento.PrazoInscricao);
                    command.Parameters.AddWithValue("@PrazoSubmissao", evento.PrazoSubmissao);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int Atualizar(Evento evento)
        {
            try
            {
                using (var connection = DbConnection())
                {
                    string query = "UPDATE Evento SET Nome=@Nome, descricao=@Descricao, ativo=@Ativo, prazo_inscricao=@PrazoInscricao, prazo_submissao=@PrazoSubmissao WHERE id=@Id";
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@Nome", evento.Nome);
                    command.Parameters.AddWithValue("@Descricao", evento.Descricao);
                    command.Parameters.AddWithValue("@Ativo", evento.Ativo);
                    command.Parameters.AddWithValue("@PrazoInscricao", evento.PrazoInscricao);
                    command.Parameters.AddWithValue("@PrazoSubmissao", evento.PrazoSubmissao);
                    command.Parameters.AddWithValue("@Id", evento.Id);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o evento", ex);
            }
        }

        public static int Deletar(int id)
        {
            try
            {
                int retornoQuery = 0;
                using (var connection = DbConnection())
                {
                    string query = "DELETE FROM Evento WHERE id=@Id";
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    retornoQuery = command.ExecuteNonQuery();
                }
                return retornoQuery;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível deletar os eventos", ex);
            }
        }
    }
}