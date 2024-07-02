using csharp_Sqlite;
using Eventos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            EnsureTableCreated();
            DataTable clientes = DalHelper.GetClientes();
            List<Evento> eventos = new List<Evento>();

            foreach (DataRow row in clientes.Rows)
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
        }

        //[HttpGet]
        //[HttpGet]
        //public async Task<IEnumerable<Evento>> GetById(int id)
        //{
        //    var item = new List<Evento> { };
        //    return await Task.Run(() => (item));
        //}

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Evento evento) 
        {
            try
            {
                EnsureTableCreated();
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
                EnsureTableCreated();
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
                EnsureTableCreated();
                DalHelper.Delete(id);
                return Ok("Evento deletado com Sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private void EnsureTableCreated()
        {
            if (!_TabelaCriada)
            {
                DalHelper.CriarTabelaSQlite();
                _TabelaCriada = true;
            }
        }
    }
}
