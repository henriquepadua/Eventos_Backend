using Eventos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.Controllers
{
    //var connection = new SQLiteConnection(connectionString);

    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Evento>> Get([FromQuery] EventoRequest evento)
        {
            var item = new List<Evento> { };
            
            return await Task.Run(() => (item));
        }

        //[HttpGet]
        //public async Task<IEnumerable<Evento>> GetById(int id)
        //{
        //    var item = new List<Evento> { };
        //    return await Task.Run(() => (item));
        //}

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Evento evento) 
        {
            string query = "INSERT INTO person (name) VALUES ('John')";
            var command = new SQLiteCommand(query, connection);
            //command.ExecuteNonQuery();
            return Ok();
        }
        

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Evento evento)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok();
        }
    }
}
