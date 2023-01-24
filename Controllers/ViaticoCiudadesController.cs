using apicea.Data;
using Entidades.Viaticos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apicea.Controllers
{
    [Route("api-viaticos/Ciudades")]
    [ApiController]
    public class ViaticoCiudadesController : ControllerBase
    {
        private readonly DataContext dbContext;

        public ViaticoCiudadesController(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViaticosCiudad>>> GetAll()
        {
            var result = await dbContext.ViaticosCiudades.OrderBy(c => c.IdCiudad).ToListAsync();
            return Ok(result);
        }

        [HttpGet("ByIdestado/{id:int}")]
        public async Task<ActionResult<IEnumerable<ViaticosCiudad>>> GetByEstado(int id)
        {
            var result = await dbContext.ViaticosCiudades.Where(v => v.IdEstado== id).ToListAsync();  
            return Ok(result);
        }
    }
}
