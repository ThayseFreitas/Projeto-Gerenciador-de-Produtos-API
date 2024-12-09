using GerenciadorDeProdutoApi.Models;
using GerenciadorDeProdutoApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeProdutos.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase {
        private readonly ProdutoContext _context;

        public ProdutoController(ProdutoContext context) {
            _context = context;
        }

        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll() {
            var produtos = _context.Produtos.Include(p => p.Categoria).ToList();
            if (!produtos.Any())
                return NotFound("Nenhum produto encontrado.");

            return Ok(produtos);
        }

        
        [AllowAnonymous]
        [HttpGet("em-estoque")]
        public IActionResult GetProdutosEmEstoque() {
            var produtos = _context.Produtos
                .Where(p => p.Status == "Em estoque")
                .Include(p => p.Categoria)
                .ToList();

            if (!produtos.Any())
                return NotFound("Nenhum produto em estoque no momento.");

            return Ok(produtos);
        }

       
        [Authorize(Roles = "Gerente")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id) {
            var produto = _context.Produtos.Find(id);
            if (produto == null)
                return NotFound("Produto não encontrado.");

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(new { Message = $"Produto {produto.Nome} excluído com sucesso." });
        }

        
        [Authorize(Roles = "Gerente,Funcionario")]
        [HttpPut("{id}")]
        public IActionResult UpdateProduto(int id, [FromBody] Produto produtoAtualizado) {
            if (produtoAtualizado == null)
                return BadRequest("Dados inválidos.");

            var produto = _context.Produtos.Find(id);
            if (produto == null)
                return NotFound("Produto não encontrado.");

            
            produto.Nome = produtoAtualizado.Nome ?? produto.Nome;
            produto.Descricao = produtoAtualizado.Descricao ?? produto.Descricao;
            produto.Preco = produtoAtualizado.Preco != 0 ? produtoAtualizado.Preco : produto.Preco;
            produto.Status = produtoAtualizado.Status ?? produto.Status;
            produto.QuantidadeEstoque = produtoAtualizado.QuantidadeEstoque >= 0 ?
                                        produtoAtualizado.QuantidadeEstoque : produto.QuantidadeEstoque;

            _context.SaveChanges();

            return Ok(new { Message = "Produto atualizado com sucesso.", Produto = produto });
        }

        
        [Authorize(Roles = "Gerente,Funcionario")]
        [HttpPost]
        public IActionResult CreateProduto([FromBody] Produto novoProduto) {
            if (novoProduto == null)
                return BadRequest("Dados inválidos.");

            
            if (novoProduto.CategoriaId.HasValue && !_context.Categorias.Any(c => c.Id == novoProduto.CategoriaId))
                return BadRequest("Categoria inválida.");

            
            _context.Produtos.Add(novoProduto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAll), new { id = novoProduto.Id },
                new { Message = "Produto criado com sucesso.", Produto = novoProduto });
        }

    }
}
