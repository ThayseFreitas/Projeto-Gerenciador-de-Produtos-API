using GerenciadorDeProdutoApi.Models;
using GerenciadorDeProdutoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeProdutoApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService) {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserRequest login) {
            
            if (ValidateUser(login, out string role, out string userId)) {
                
                var token = _jwtService.GenerateToken(userId, role);
                return Ok(new { Token = token });
            }

            return Unauthorized(new { Message = "Usuário ou senha inválidos." });
        }

        private bool ValidateUser(UserRequest login, out string role, out string userId) {
            role = null;
            userId = null;

            
            if (login.Username == "gerente" && login.Password == "123456") {
                role = "Gerente";
                userId = "1";
                return true;
            } else if (login.Username == "funcionario" && login.Password == "123456") {
                role = "Funcionario";
                userId = "2";
                return true;
            } else if (login.Username == "visitante" && login.Password == "123456") {
                role = "Visitante";
                userId = "3";
                return true;
            }

            return false;
        }
    }
}
