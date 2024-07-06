using Eventos.Models;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace csharp_Sqlite
{
    public class DalParticipante  
    {
        private static SQLiteConnection sqliteConnection;
        public const string DatabasePath = @"c:\dados\Eventos.sqlite";

        public DalParticipante() { }

        public static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;");
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
            catch (Exception ex)
            {
                throw new Exception("Não foi possível criar o arquivo do banco de dados.", ex);
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
                throw new Exception("Não foi possível criar a tabela de eventos.", ex);
            }

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Participante(
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nome VARCHAR(50),
                            Email VARCHAR(80) UNIQUE,
                            ativo BOOLEAN
                        )";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível criar a tabela de participantes.", ex);
            }
        }

        // Métodos de gerenciamento de eventos (Get, Add, Update, Delete) permanecem os mesmos...

        public static bool EmailExiste(string email)
        {
            try
            {
                using (var connection = DbConnection())
                {
                    string query = "SELECT COUNT(1) FROM Participante WHERE Email = @Email";
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar o email do participante.", ex);
            }
        }

        public static int AdicionarParticipante(Participante participante)
        {
            try
            {
                using (var connection = DbConnection())
                {
                    string query = "INSERT INTO Participante (Nome, Email, ativo) VALUES (@Nome, @Email, @Ativo)";
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@Nome", participante.Nome);
                    command.Parameters.AddWithValue("@Email", participante.Email);
                    command.Parameters.AddWithValue("@Ativo", participante.Ativo);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar o participante.", ex);
            }
        }

        public static int AtualizaParticipante(Participante participante)
        {
            try
            {
                using (var connection = DbConnection())
                {
                    string query = "UPDATE Participante SET Nome=@Nome, Email=@Email, ativo=@Ativo WHERE id=@Id";
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@Nome", participante.Nome);
                    command.Parameters.AddWithValue("@Email", participante.Email);
                    command.Parameters.AddWithValue("@Ativo", participante.Ativo);
                    command.Parameters.AddWithValue("@Id", participante.Id);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o participante.", ex);
            }
        }

        public static int DeleteParticipante(int id)
        {
            try
            {
                int retornoQuery = 0;
                using (var connection = DbConnection())
                {
                    string query = "DELETE FROM Participante WHERE id=@Id";
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    retornoQuery  = command.ExecuteNonQuery();
                }
                return retornoQuery;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível deletar o participante.", ex);
            }
        }

        public static DataTable GetParticipantes()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Participante";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter os participantes.", ex);
            }
        }
    }
}
