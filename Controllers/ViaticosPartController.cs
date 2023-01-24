using apicea.Data;
using Entidades.Viaticos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apicea.Controllers
{
    [Route("api-viaticos/[controller]")]
    [ApiController]
    public class ViaticosPartController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ViaticosPartController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("byEjercicio/{id:int}")]
        public async Task<ActionResult<IEnumerable<ViaticosPart>>> GetByEjecicio(int id)
        {
            var response = await _dataContext.ViaticosPartidas
                    .Where(v => v.Ejercicio == id).ToListAsync();
            return Ok(response);    
        }

        [HttpGet("byEjercicioOficina/{ejercicio:int}/{oficina:int}")]
        public async Task<ActionResult<IEnumerable<ViaticosPart>>> GetByEjercicioOficina( int ejercicio,int oficina)
        {
            var response = await _dataContext.ViaticosPartidas
                        .Where(v => v.Ejercicio == ejercicio && v.Oficina== oficina)
                        .OrderBy( v => v.Ejercicio )
                            .ThenBy( v => v.Oficina)
                            .ThenBy( v => v.NoViat)
                        .ToListAsync(); 
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ViaticosPart>> Save(ViaticosPart viaticosPart)
        {
            _dataContext.ViaticosPartidas.Add(viaticosPart);
            try
            {
                await _dataContext.SaveChangesAsync();
                return Ok("registro guardado exitosamente!");

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
