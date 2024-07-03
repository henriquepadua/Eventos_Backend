using csharp_Sqlite;
using Eventos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Eventos.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private static bool _TabelaCriada = false;

        [HttpGet("ListarEventos")]
        public async Task<ActionResult> Get()
        {
            try
            {
            VerificaSeTabelaFoiCriada();
            DataTable Evento = DalEvento.GetEventos();
            List<Evento> eventos = new List<Evento>();

            foreach (DataRow row in Evento.Rows)
            {
                Evento evento = new Evento
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nome = row["Nome"].ToString(),
                    Descricao = row["Descricao"].ToString(),
                    Ativo = Convert.ToBoolean(row["ativo"]),
                    PrazoInscricao = Convert.ToDateTime(row["prazo_inscricao"]),
                    PrazoSubmissao = Convert.ToDateTime(row["prazo_submissao"])
                };
                eventos.Add(evento);
            }
                if (eventos != null)
                    return Ok(eventos);
                else
                    return StatusCode(400, "Nenhum Evento Cadastrado");
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CriarEvento")]
        public async Task<ActionResult> Post([FromBody] Evento evento) 
        {
            try
            {
                VerificaSeTabelaFoiCriada();
                if(DalEvento.Adicionar(evento) ==1)
                    return Ok(evento);
                else
                    return StatusCode(400, "Nenhum Evento Encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("AtualizarEvento")]
        public async Task<ActionResult> Update([FromBody] Evento evento)
        {
            try
            {
                VerificaSeTabelaFoiCriada();
                if (DalEvento.Atualizar(evento) == 1)
                    return Ok(evento);
                else
                    return StatusCode(400, "Nenhum Evento Encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("RemoverEvento")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                VerificaSeTabelaFoiCriada();
                if (DalEvento.Deletar(id) == 1)
                {
                    return Ok("Participante deletado com sucesso");
                }
                else
                {
                    return StatusCode(400, "Participante não encontrado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private void VerificaSeTabelaFoiCriada()
        {
            if (!_TabelaCriada)
            {
                DalEvento.CriarTabelaSQlite();
                _TabelaCriada = true;
            }
        }
    }
}
