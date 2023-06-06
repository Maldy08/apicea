using apicea.Data;
using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace apicea.Controllers
{
    [Produces("application/json")]
    [Route("api-viaticos/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext dbContext, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Usuarios>> Login(string user, string password)
        {
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Login.ToUpper().Equals(user.ToUpper())
                && u.Pass.ToUpper().Equals(password.ToUpper()));

            if (usuario != null)
                //return Ok(new AuthenticateResponse { Token = CreateToken(usuario), usuario = usuario });
                return Ok(new AuthResponse { Ok = true, Token = CreateToken(usuario), Id = usuario.Usuario, User = usuario.Login, UserData = await GetUserData(usuario.Login) });

            return BadRequest( new AuthResponse { Ok = false } );
        }

        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            var result = await _dbContext.Usuarios.ToListAsync();
            return Ok(result);
        }

        [HttpGet("usuarioById/{id:int}")]
        public async Task<ActionResult<Usuarios>> GetUsuarioById(int id)
        {
            var result = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Usuario == id);
            if( result!= null) return Ok( new AuthResponse {Ok = true, Id = result.Usuario, User = result.Login });

            return BadRequest(new AuthResponse { Ok = false, Id = 0 });
        }

        [HttpGet("validate-token")]
        public async Task<ActionResult> ValidateToken(string token)
        {
            var handler = DecodeToken(token);
            if (handler!.Claims.Any())
            {
                var user = handler.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var id = int.Parse( handler.Claims.First(c => c.Type == "userid").Value);
                return Ok( new AuthResponse { Ok = true, Token = token, Id = id, User = user, UserData = await GetUserData( user ) } );
            }
            else
            {
                return BadRequest( new AuthResponse { Ok = false});
            }
             
          
        }

        private async Task<UserData?> GetUserData(string user)
        {
            var result = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Login.Equals(user));
            if (result != null)
            {
                return new UserData
                {
                    Activo = result.Activo,
                    Depto = result.Depto,
                    NoEmpleado = result.NoEmpleado,
                    Viaticos = result.Viaticos,
                    ViaticosNivel = result.ViaticosNivel,
                    DeptoDescripcion = result.DeptoDescripcion,
                    NombreCompleto = result.NombreCompleto,
                    IdPue = result.IdPue,
                    DescripcionPuesto = result.Descripcion,
                    Municipio= result.Municipio,
                    Oficina= result.Oficina,
                };
            }
            else return null;
        }

        private string CreateToken(Usuarios user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Login),
                new Claim("userid",user.Usuario.ToString()),
               // new Claim("depto", user.Depto.ToString())
              //  new Claim(ClaimTypes.Role,user.OficiosNivel == 9 ? "mc": "usuario")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                     _configuration["Jwt:Issuer"],
                     _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private ClaimsPrincipal? DecodeToken(string token)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration["Jwt:Key"]));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            try
            {
                var handler = new JwtSecurityTokenHandler().
                    ValidateToken(token, new TokenValidationParameters()
                    {
                        IssuerSigningKey = key,
                        ValidIssuer = issuer,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true

                    }, out SecurityToken stoken);
                return handler;
            }
            catch (SecurityTokenException e)
            {
                
                Console.WriteLine(e.Message);
                return null;
            }
           
            //var a = handler.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }


    }

    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public bool Ok { get; set; } = false;
        public int? Id { get; set; }
        public string? User { get; set; } 
        public UserData? UserData { get; set; } 
    }

    public class UserData
    {
        public bool? Activo { get; set; }
        public int Depto { get; set; }
        public int NoEmpleado { get; set; }
        public bool? Viaticos { get; set; }
        public int? ViaticosNivel { get; set; }
        public string DeptoDescripcion { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public int IdPue { get; set; }
        public string DescripcionPuesto{ get; set; } = string.Empty;
        public int Municipio { get; set; }
        public int Oficina { get; set; }
    }
}
