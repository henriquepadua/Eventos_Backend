using csharp_Sqlite;
using Eventos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Eventos.Controllers
{
    [ApiController]
    public class ParticipanteController : ControllerBase
    {
        private static bool _TabelaCriada = false;
        [Route("api/[controller]/PegarTodosParticipantes")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                VerificaSeTabelaFoiCriada();
                DataTable ParticipanteTable = DalParticipante.GetParticipantes();
                List<Participante> participantes = new List<Participante>();

                foreach (DataRow row in ParticipanteTable.Rows)
                {
                    Participante participante = new Participante
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Nome = row["Nome"].ToString(),
                        Email = row["Email"].ToString(),
                        Ativo = Convert.ToBoolean(row["ativo"])
                    };
                    participantes.Add(participante);
                }
                if (participantes != null)
                {
                    return Ok(participantes);
                }
                else
                {
                    return StatusCode(400, "Nenhum Participante Cadastrado");

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Route("api/[controller]/CriaParticipante")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Participante participante)
        {
            try
            {
                VerificaSeTabelaFoiCriada();
                DalParticipante.AddParticipante(participante);
                return Ok(participante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Route("api/[controller]/AtualizaParticipante")]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Participante participante)
        {
            try
            {
                VerificaSeTabelaFoiCriada();
                DalParticipante.UpdateParticipante(participante);
                return Ok(participante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Route("api/[controller]/DeletaParticipante")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                VerificaSeTabelaFoiCriada();
                if (DalParticipante.DeleteParticipante(id) == 1)
                {
                    return Ok("Participante deletado com sucesso");
                }
                else
                {
                    return StatusCode(500,"Participante não encontrado");
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
                DalParticipante.CriarTabelaSQlite();
                _TabelaCriada = true;
            }
        }
    }
}
