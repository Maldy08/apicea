using apicea.Data;
using Entidades.Viaticos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apicea.Controllers
{
    [Route("api-viaticos/Paises")]
    [ApiController]
    public class ViaticosPaisesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ViaticosPaisesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViaticosPais>>> GetAll()
        {
            var response = await _dataContext.ViaticosPaises.ToListAsync();
            return Ok(response);
        }

        [HttpGet("ById/{id:int}")]
        public async Task<ActionResult<ViaticosPais>> GetById(int id)
        {
            var reponse = await _dataContext.ViaticosPaises.FirstOrDefaultAsync(p => p.IdPais == id);
            return Ok(reponse);
        }
    }
}
