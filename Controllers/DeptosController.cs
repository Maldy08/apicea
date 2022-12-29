using apicea.Data;
using Entidades.RecursosHumanos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace apicea.Controllers
{
    [Route("api/[controller]")]
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
            var result = await dbContext.DeptosUe.ToListAsync();
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult<DeptoUe>> GetById(int id)
        {
            var result = await dbContext.DeptosUe.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("idshpoa")]
        public async Task<ActionResult<IEnumerable<DeptoUe>>> GetByIdShPoa(int id)
        {
            var result = await dbContext.DeptosUe.Where(d => d.IdShpoa.Equals(id)).ToListAsync();   
            return Ok(result);
        }

        [HttpGet("idagrupapoa")]
        public async Task<ActionResult<IEnumerable<DeptoUe>>> GetByIdAgrupaPoa(int id)
        {
            var result = await dbContext.DeptosUe.Where(d => d.AgrupaPoa.Equals(id)).ToListAsync(); 
            return Ok(result);
        }
        
    }
}
