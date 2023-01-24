using apicea.Data;
using Entidades.RecursosHumanos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace apicea.Controllers
{
    [Route("api-viaticos/Departamentos")]
    [ApiController]
    public class DeptosController : ControllerBase
    {
        private readonly DataContext dbContext;

        public DeptosController(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeptoUe>>> GetAll()
        {
            var result = await dbContext.DeptosUe.OrderBy(d => d.IdCea).ToListAsync();
            return Ok(result);
        }

        [HttpGet("byId/{id:int}")]
        public async Task<ActionResult<DeptoUe>> GetById(int id)
        {
            var result = await dbContext.DeptosUe.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("byIdshpoa/{id:int}")]
        public async Task<ActionResult<IEnumerable<DeptoUe>>> GetByIdShPoa(int id)
        {
            var result = await dbContext.DeptosUe.Where(d => d.IdShpoa.Equals(id)).ToListAsync();   
            return Ok(result);
        }

        [HttpGet("ByIdagrupapoa/{id:int}")]
        public async Task<ActionResult<IEnumerable<DeptoUe>>> GetByIdAgrupaPoa(int id)
        {
            var result = await dbContext.DeptosUe.Where(d => d.AgrupaPoa.Equals(id)).ToListAsync(); 
            return Ok(result);
        }
        
    }
}
