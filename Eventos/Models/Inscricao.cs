namespace Eventos.Models
{
    public class Inscricao
    {
        public int eventoId { get; set; }
        public int participanteId { get; set; }
        public DateTime data { get; set; }
    }

    public class InscricaoRequest
    {
        public int eventoId { get; set; }
        public int participanteId { get; set; }
    }

    public class InscricaoRequestListar
    {
        public int eventoId { get; set; }
    }
}
