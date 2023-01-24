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

        [HttpGet("byId/{id:int}")]
        public async Task<ActionResult<Empleado>> GetById(int id)
        {
            var result = await dbContext.Empleados.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("byDepto/{id:int}")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetByDepto(int id)
        {
            var result = await dbContext.Empleados.Where(e => e.Depto == id && e.Activo.Equals("V")).ToListAsync();
            return Ok(result);
        }

        [HttpGet("byDeptoppto/{id:int}")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetByDeptoPpto(int id)
        {
            var result = await dbContext.Empleados.Where(e => e.DeptoPpto == id && e.Activo.Equals("V")).ToListAsync();
            return Ok(result);
        }

        [HttpGet("byDeptocomi/{id:int}")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetByDeptoComi(int id)
        {
            var result = await dbContext.Empleados.Where(e => e.DeptoComi == id && (e.Activo.Equals("V") || e.Activo.Equals("C")) ).ToListAsync();
            return Ok(result);
        }

    }
}
