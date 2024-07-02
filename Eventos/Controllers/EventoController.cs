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

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
            VerificaSeTabelaFoiCriada();
            DataTable Evento = DalHelper.GetEventos();
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

            return Ok(eventos);
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Evento evento) 
        {
            try
            {
                VerificaSeTabelaFoiCriada();
                DalHelper.Add(evento);
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Evento evento)
        {
            try
            {
                VerificaSeTabelaFoiCriada();
                DalHelper.Update(evento);
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                VerificaSeTabelaFoiCriada();
                //return Ok("Evento deletado com Sucesso");
                if (DalHelper.Delete(id) == 1)
                {
                    return Ok("Participante deletado com sucesso");
                }
                else
                {
                    return StatusCode(500, "Participante não encontrado");
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
                DalHelper.CriarTabelaSQlite();
                _TabelaCriada = true;
            }
        }
    }
}
