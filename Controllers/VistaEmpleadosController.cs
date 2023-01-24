using apicea.Data;
using Entidades.RecursosHumanos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apicea.Controllers
{
    [Route("api-viaticos/[controller]")]
    [ApiController]
    public class VistaEmpleadosController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public VistaEmpleadosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VistaEmpleados>>> GetAll()
        {
            var response = await _dataContext.VistaEmpleados.ToListAsync();
            return Ok(response);
        }

        [HttpGet("byId/{id:int}")]
        public async Task<ActionResult<VistaEmpleados>> GetById( int id)
        {
            var response = await _dataContext.VistaEmpleados
                        .FirstOrDefaultAsync(v => v.Empleado == id);
            return Ok(response);
        }

        [HttpGet("byDeptoComi/{id:int}")]
        public async Task<ActionResult<VistaEmpleados>> GetByDeptoComi(int id)
        {
            var response = await _dataContext.VistaEmpleados
             .FirstOrDefaultAsync(v => v.Deptocomi == id);
            return Ok(response);
        }
    }
}
