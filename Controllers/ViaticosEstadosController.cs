using apicea.Data;
using Entidades.Viaticos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apicea.Controllers
{
    [Route("api-viaticos/Estados")]
    [ApiController]
    public class ViaticosEstadosController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ViaticosEstadosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViaticosEstado>>> GetAll()
        {
            var response = await _dataContext.ViaticosEstados.OrderBy(e => e.IdEstado).ToListAsync();
            return Ok(response);
        }

        [HttpGet("ByIdEstado/{id:int}")]
        public async Task<ActionResult<ViaticosEstado>> GetById(int id)
        {
            var response = await _dataContext.ViaticosEstados.FirstOrDefaultAsync(e => e.IdEstado == id);
            return Ok(response);
        }

        [HttpGet("ByIdPais/{id:int}")]
        public async Task<ActionResult<IEnumerable<ViaticosEstado>>> GetByIdPais(int id)
        {
            var response = await _dataContext.ViaticosEstados.Where(e => e.IdPais == id).ToListAsync();
            return Ok(response);
        }
    }
}
