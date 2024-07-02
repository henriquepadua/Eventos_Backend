using csharp_Sqlite;
using Eventos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Eventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscricaoController : ControllerBase
    {
        private static bool _TabelasCriadas = false;

        [HttpGet("ListarPorEvento")]
        public async Task<ActionResult> ListarParticipantesPorEvento(int eventoId)
        {
            try
            {
                VerificaSeTabelasForamCriadas();
                DataTable participantes = DalInscricao.GetParticipantesPorEvento(eventoId);
                List<Participante> listaParticipantes = new List<Participante>();

                foreach (DataRow row in participantes.Rows)
                {
                    Participante participante = new Participante
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Email = row["email"].ToString(),
                        Nome = row["nome"].ToString(),
                        Ativo = Convert.ToBoolean(row["ativo"])
                    };
                    listaParticipantes.Add(participante);
                }

                return Ok(listaParticipantes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Inscrever")]
        public async Task<ActionResult> Inscrever(int eventoId, int participanteId)
        {
            try
            {
                VerificaSeTabelasForamCriadas();
                DalInscricao.InscreverParticipante(eventoId, participanteId);
                return Ok("Participante inscrito com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("CancelarInscricao")]
        public async Task<ActionResult> CancelarInscricao(int eventoId, int participanteId)
        {
            try
            {
                VerificaSeTabelasForamCriadas();
                DalInscricao.CancelarInscricao(participanteId, eventoId);
                return Ok("Inscrição cancelada com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private void VerificaSeTabelasForamCriadas()
        {
            if (!_TabelasCriadas)
            {
                DalEvento.CriarTabelaSQlite();
                DalParticipante.CriarTabelaSQlite();
                DalInscricao.CriarTabelaInscricaoSQlite();
                _TabelasCriadas = true;
            }
        }
    }
}
