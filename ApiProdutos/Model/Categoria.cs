namespace ApiProdutos.Model
{
    public class Categoria
    {
        public int Id{ get; set; }
        public string Nome { get; set; } = null!;
        public bool Ativo{ get; set; }
        public List<Produto> Produtos { get; set; } = null!;
    }
}
