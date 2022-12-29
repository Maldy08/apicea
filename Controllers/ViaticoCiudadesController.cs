using apicea.Data;
using Entidades.Viaticos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apicea.Controllers
{
    [Route("api/[controller]")]
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
            var result = await dbContext.ViaticosCiudades.ToListAsync();
            return Ok(result);
        }

        [HttpGet("idestado")]
        public async Task<ActionResult<IEnumerable<ViaticosCiudad>>> GetByEstado(int idestado)
        {
            var result = await dbContext.ViaticosCiudades.Where(v => v.IdEstado== idestado).ToListAsync();  
            return Ok(result);
        }
    }
}
