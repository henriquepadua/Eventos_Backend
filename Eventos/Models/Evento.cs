namespace Eventos.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime PrazoInscricao { get; set; }
        public DateTime PrazoSubmissao { get; set; }
    }

    public class EventoRequest
    {
        public bool Ativo { get; set; }
        public DateTime PrazoInscricao { get; set; }
        public DateTime PrazoSubmissao { get; set; }
    } 
}
