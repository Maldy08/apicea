using apicea.Data;
using Entidades.RecursosHumanos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apicea.Controllers
{
    [Route("api-viaticos/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly DataContext dbContext;

        public EmpleadosController(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetAll()
        {
            var result = await dbContext.Empleados.Where(e => e.Activo.Equals("V")).ToListAsync();
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult<Empleado>> GetById(int idempleado)
        {
            var result = await dbContext.Empleados.FindAsync(idempleado);
            return Ok(result);
        }

        [HttpGet("depto")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetByDepto(int iddepto)
        {
            var result = await dbContext.Empleados.Where(e => e.Depto== iddepto && e.Activo.Equals("V")).ToListAsync();
            return Ok(result);
        }

        [HttpGet("deptoppto")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetByDeptoPpto(int iddeptoppto)
        {
            var result = await dbContext.Empleados.Where(e => e.DeptoPpto == iddeptoppto && e.Activo.Equals("V")).ToListAsync();
            return Ok(result);
        }

    }
}
