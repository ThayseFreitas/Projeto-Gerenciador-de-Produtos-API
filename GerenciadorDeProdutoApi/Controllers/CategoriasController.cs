using GerenciadorDeProdutoApi.Models;
using GerenciadorDeProdutoApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeProdutoApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase {
        private readonly ProdutoContext _context;

        public CategoriaController(ProdutoContext context) {
            _context = context;
        }

        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetCategorias() {
            var categorias = _context.Categorias.ToList();
            return Ok(categorias);
        }

       
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetCategoriaById(int id) {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound("Categoria não encontrada.");
            return Ok(categoria);
        }

        
        [Authorize(Roles = "Gerente,Funcionario")]
        [HttpPost]
        public IActionResult CreateCategoria([FromBody] Categoria categoria) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCategoriaById), new { id = categoria.Id }, categoria);
        }

        
        [Authorize(Roles = "Gerente,Funcionario")]
        [HttpPut("{id}")]
        public IActionResult UpdateCategoria(int id, [FromBody] Categoria categoriaAtualizada) {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound("Categoria não encontrada.");

            categoria.Nome = categoriaAtualizada.Nome;

            _context.SaveChanges();
            return Ok(categoria);
        }

        
        [Authorize(Roles = "Gerente")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategoria(int id) {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound("Categoria não encontrada.");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
