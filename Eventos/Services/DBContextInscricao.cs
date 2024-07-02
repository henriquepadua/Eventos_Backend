using Eventos.Models;
using System;
using System.Data;
using System.Data.SQLite;
namespace csharp_Sqlite
{
    public class DalInscricao
    {
        private static SQLiteConnection sqliteConnection;
        public const string DatabasePath = @"c:\dados\Eventos.sqlite";
        public DalInscricao()
        { }

        public static SQLiteConnection DbConnection(
            )
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
        public static void CriarTabelaInscricaoSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Inscricao(
                        evento_id INTEGER,
                        participante_id INTEGER,
                        data DATETIME,
                        PRIMARY KEY (evento_id, participante_id),
                        FOREIGN KEY (evento_id) REFERENCES Evento(id),
                        FOREIGN KEY (participante_id) REFERENCES Participante(id)
                    )";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível criar a tabela de inscrição", ex);
            }
        }

        public static int InscreverParticipante(int eventoId, int participanteId)
        {
            try
            {
                using (var connection = DbConnection())
                {
                    string query = "INSERT INTO Inscricao (evento_id, participante_id, data) VALUES (@EventoId, @ParticipanteId, @Data)";
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@EventoId", eventoId);
                    command.Parameters.AddWithValue("@ParticipanteId", participanteId);
                    command.Parameters.AddWithValue("@Data", DateTime.Now);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível inscrever o participante", ex);
            }
        }

        public static int CancelarInscricao(int participanteId, int eventoId)
        {
            try
            {
                using (var connection = DbConnection())
                {
                    string query = "DELETE FROM Inscricao WHERE participante_id=@ParticipanteId AND evento_id=@EventoId";
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@ParticipanteId", participanteId);
                    command.Parameters.AddWithValue("@EventoId", eventoId);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível cancelar a inscrição", ex);
            }
        }

        public static DataTable GetParticipantesPorEvento(int eventoId)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT p.id, p.email, p.nome, p.ativo 
                    FROM Participante p
                    INNER JOIN Inscricao i ON p.id = i.participante_id
                    WHERE i.evento_id = @EventoId";
                    cmd.Parameters.AddWithValue("@EventoId", eventoId);
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível obter os participantes do evento", ex);
            }
        }
    }
}