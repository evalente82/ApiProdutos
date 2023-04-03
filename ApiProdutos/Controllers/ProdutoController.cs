using ApiProdutos.DTO;
using ApiProdutos.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProdutos.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoDbContext context;
        private readonly IMapper mapper;

        public ProdutoController(ProdutoDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProdutoCreationDTO produtoCreationDTO)
        {
            var produto = mapper.Map<Produto>(produtoCreationDTO);
            context.Add(produto);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            return await context.Produto.ToListAsync();
        }

        [HttpGet("nome")]
        public async Task<ActionResult<IEnumerable<Produto>>> Get(string nome)
        {
            return await context.Produto.Where(x => x.Nome.Contains(nome)).ToListAsync();
        }

        [HttpGet("buscarProduto/categoria")]
        public async Task<ActionResult<IEnumerable<Produto>>> BuscaPorCategoria(string categoria)
        {

            var contexto = context.Categoria;

            var idCategoria = contexto.Where(c => c.Nome == categoria).Select(c => c.Id).FirstOrDefault();

            return await context.Produto.Where(x => x.CategoriaId == idCategoria).ToListAsync();
                        
        }

        [HttpPost("lista")]
        public async Task<ActionResult> Post(ProdutoCreationDTO[] produtoCreationDTO)
        {
            var produto = mapper.Map<Produto[]>(produtoCreationDTO);
            context.AddRange(produto);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("buscaNomeAtivo")]
        public async Task<ActionResult<IEnumerable<Produto>>> BuscaPorNomeAtivo(string? categoria, bool? ativo, string? descricao)
        {
            var batata = from num in context.Produto where num.Categoria.Nome.Contains(categoria) && num.Ativo == ativo && num.Descricao.Contains(descricao) select num;
            if (!String.IsNullOrEmpty(categoria) && !String.IsNullOrEmpty(descricao) && ativo is null)
            {
                batata = from num in context.Produto where num.Categoria.Nome.Contains(categoria) && num.Ativo == ativo && num.Descricao.Contains(descricao) select num;
            }
            else if (!String.IsNullOrEmpty(categoria) && !String.IsNullOrEmpty(descricao))
            {
                batata = from num in context.Produto where num.Categoria.Nome.Contains(categoria) && num.Descricao.Contains(descricao) select num;
            }
            else if (!String.IsNullOrEmpty(categoria))
            {
                batata = from num in context.Produto where num.Categoria.Nome.Contains(categoria) select num;
            }
            else if (ativo != null)
            {
                batata = from num in context.Produto where num.Ativo == ativo select num;
            }
            else if (!String.IsNullOrEmpty(descricao))
            {
                batata = from num in context.Produto where num.Descricao.Contains(descricao) select num;
            }
            else
            {
                return NotFound();
            }

            return await batata.ToListAsync();
        }


        [HttpPut("{id:int}/nome")]
        public async Task<ActionResult> Put(int id, ProdutoCreationDTO produtoCreationDTO)
        {
            var produto = mapper.Map<Produto>(produtoCreationDTO);
            if (produto == null)
            {
                return NotFound();
            }
            else
            {
                produto.Id = id;
                context.Update(produto);
                await context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
