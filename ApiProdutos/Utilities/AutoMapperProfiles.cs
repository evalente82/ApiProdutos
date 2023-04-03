using ApiProdutos.DTO;
using ApiProdutos.Model;
using AutoMapper;

namespace ApiProdutos.Utilities
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<CategoriaCreationDTO, Categoria>();
            CreateMap<ProdutoCreationDTO, Produto>();
        }
    }
}
