using apicea.Data;
using Entidades.Viaticos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace apicea.Controllers
{
    [Route("api-viaticos/[controller]")]
    [ApiController]
    public class ViaticosController : ControllerBase
    {
        private readonly DataContext dbContext;

        public ViaticosController(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Viaticos>>> GetAll(int ejercicio,int oficina)
        {
            var result = await dbContext.Viaticos.Where(v => v.Ejercicio == ejercicio && v.Oficina == oficina ).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{ejercicio}/{oficina}/{noviat}")]
        public async Task<ActionResult<Viaticos>> GetAllByEjercicioOficinaNoviat(int ejercicio, int oficina, int noviat)
        {
            var result = await dbContext.Viaticos.FirstOrDefaultAsync(v => v.Ejercicio == ejercicio && v.Oficina == oficina
                && v.NoViat == noviat);

            return Ok(result);
        }

        [HttpGet("get-consecutivo/{ejercicio:int}/{oficina:int}")]
        public  ActionResult<IEnumerable<Viaticos>> GetNoViat(int ejercicio, int oficina)
        {

            var exists = dbContext.Viaticos.AnyAsync(v => v.Ejercicio == ejercicio && v.Oficina == oficina).Result;
            if(exists)
            {

                var result = dbContext.Viaticos.Where(v => v.Ejercicio == ejercicio && v.Oficina == oficina).Select(v => v.NoViat).Max();
                return Ok(result);
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpGet("{ejercicio}/{empleado}")]
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
                //await Task.Delay
                  //  (1000);
                return Ok(viaticos);

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        public async Task<ActionResult<Viaticos>> Update(Viaticos viaticos)
        {
            var viaticoUpdate = await dbContext.Viaticos.FindAsync(viaticos.Oficina, viaticos.Ejercicio, viaticos.NoViat);
            if(viaticoUpdate != null)
            {
                viaticoUpdate.Motivo = viaticos.Motivo;
                viaticoUpdate.OrigenId = viaticos.OrigenId;
                viaticoUpdate.DestinoId = viaticos.DestinoId;
                viaticoUpdate.Dias = viaticos.Dias;
                viaticoUpdate.Fecha= viaticos.Fecha;
                viaticoUpdate.FechaSal = viaticos.FechaSal;
                viaticoUpdate.FechaReg = viaticos.FechaReg;
                viaticoUpdate.InforAct = viaticos.InforAct;
                viaticoUpdate.InforResul = viaticos.InforResul;
                viaticoUpdate.InforFecha = viaticos.InforFecha;

                try
                {
                    await dbContext.SaveChangesAsync();
                    return Ok(viaticoUpdate);
                }
                catch (Exception e)
                {

                    return BadRequest(e.ToString());
                }

            }
            else
            {
                return BadRequest("Error al actualizar el registro");
            }

        }

        [HttpDelete]
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

        [HttpGet("lista-viaticos-empleado/{ejercicio:int}/{empleado:int}")]
        public async Task<ActionResult<IEnumerable<ListaViaticosPorEmpleado>>> ListaViaticosPorEmpleado(int ejercicio, int empleado)
        {
            var result = await dbContext.listaViaticosPorEmpleados.FromSqlInterpolated($"select * from table (F_LISTAVIATICOSXEMP({ejercicio},{empleado}))").ToListAsync();
            return Ok(result);
        }

        [HttpGet("formato-comision/{oficina:int}/{ejercicio:int}/{noviat:int}")]
        public async Task<ActionResult<VsFormatoComision>> GetFormatoComision(int oficina, int ejercicio, int noviat)
        {
            var result = await dbContext.VistaFormatoComision.Where( v => v.Oficina == oficina
                        && v.Ejercicio == ejercicio
                        && v.NoViat == noviat).FirstOrDefaultAsync();
            return Ok(result);
        }
        
    }
}
