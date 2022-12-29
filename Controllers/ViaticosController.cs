﻿using apicea.Data;
using Entidades.Viaticos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace apicea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViaticosController : ControllerBase
    {
        private readonly DataContext dbContext;

        public ViaticosController(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("ejercicio/oficina")]
        public async Task<ActionResult<IEnumerable<Viaticos>>> GetAll(int ejercicio,int oficina)
        {
            var result = await dbContext.Viaticos.Where(v => v.Ejercicio == ejercicio && v.Oficina == oficina ).ToListAsync();
            return Ok(result);
        }

        [HttpGet("ejercicio/oficina/noviat")]
        public async Task<ActionResult<IEnumerable<Viaticos>>> GetAllByEjercicioOficinaNoviat(int ejercicio, int oficina, int noviat)
        {
            var result = await dbContext.Viaticos.Where(v => v.Ejercicio == ejercicio && v.Oficina == oficina
                && v.NoViat == noviat).ToListAsync();
            return Ok(result);
        }

        [HttpGet("consecutivo/ejercicio/oficina")]
        public  ActionResult<IEnumerable<Viaticos>> GetNoViat(int ejercicio, int oficina)
        {
            var result = dbContext.Viaticos.Where(v => v.Ejercicio == ejercicio && v.Oficina == oficina).Select(v => v.NoViat).Max();
            return Ok(result);
        }

        [HttpGet("ejercicio/empleado")]
        public async Task<ActionResult<IEnumerable<Viaticos>>> GetAllByEjercicioDepto(int ejercicio, int empleado)
        {
            var result = await dbContext.Viaticos.Where(v => v.Ejercicio == ejercicio && v.NoEmp == empleado).ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Viaticos>> Save( Viaticos viaticos)
        {
            dbContext.Viaticos.Add(viaticos);
            try
            {
                await dbContext.SaveChangesAsync();
                return Ok("registro guardado exitosamente!");

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete("ejercicio/oficina/noviat")]
        public async Task<ActionResult<Viaticos>> Delete(int ejercicio, int oficina, int noviat)
        {
            var result = await dbContext.Viaticos.FindAsync(oficina, ejercicio, noviat);
            if(result != null)
            {
                dbContext.Viaticos.Remove(result);
                try
                {

                    await dbContext.SaveChangesAsync();
                    return Ok("Registro eliminado exitosamente");
                }
                catch
                {

                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error deleting data");
                }
            }
            
            else
                return BadRequest("registro no encontrado");
        }
        
    }
}
