using apicea.Data;
using Entidades.Viaticos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apicea.Controllers
{
    [Route("api-viaticos/Oficinas")]
    [ApiController]
    public class ViaticosOficinasController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ViaticosOficinasController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViaticosOfi>>> GetAll()
        {
            var response = await _dataContext.ViaticosOficinas.OrderBy(o => o.IdOfi).ToListAsync();
            return Ok(response);
        }

        [HttpGet("ById/{id:int}")]
        public async Task<ActionResult<ViaticosOfi>> GetById(int id)
        {
            var response = await _dataContext.ViaticosOficinas.FirstOrDefaultAsync(o => o.IdOfi== id);
            return Ok(response);
        }
    }
}
