namespace ApiProdutos.DTO
{
    public class ProdutoCreationDTO
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; } = null!;
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
    }
}
