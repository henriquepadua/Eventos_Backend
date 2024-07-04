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

                if (listaParticipantes != null)
                    return Ok(listaParticipantes);
                else
                    return StatusCode(400, "Nenhum Participante inscrito no evento");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Inscrever")]
        public async Task<ActionResult> Inscrever([FromBody] InscricaoRequest inscricao)
        {
            try
            {
                if (DalInscricao.VerificarInscricaoExistente(inscricao.eventoId, inscricao.participanteId))
                {
                    return StatusCode(404, "Inscrição já existe.");
                }

                if (DalInscricao.InscreverParticipante(inscricao.eventoId, inscricao.participanteId) == 1)
                    return Ok("Participante inscrito com sucesso");
                else
                    return StatusCode(400, "Este participante não realizou inscrição");
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
                if(DalInscricao.CancelarInscricao(participanteId, eventoId) == 1)
                    return Ok("Inscrição cancelada com sucesso");
                else
                    return StatusCode(400,"Este participante não realizou inscrição");

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
