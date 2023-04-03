namespace ApiProdutos.Model
{
    public class Produto
    {
        public int Id{ get; set; }
        public int CategoriaId{ get; set; }
        public Categoria Categoria { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; } = null!;
        public decimal Preco{ get; set; }
        public bool Ativo{ get; set; }

    }
}
