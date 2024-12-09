namespace GerenciadorDeProdutoApi.Models {
    public class Produto {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string Status { get; set; }

        
        public int? CategoriaId { get; set; } 

        public Categoria Categoria { get; set; }
    }
}
