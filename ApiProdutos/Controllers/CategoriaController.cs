using ApiProdutos.DTO;
using ApiProdutos.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProdutos.Controllers
{
    [ApiController]
    [Route("api/categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly ProdutoDbContext context;
        private readonly IMapper mapper;

        public CategoriaController(ProdutoDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CategoriaCreationDTO categoriaCreationDTO)
        {
            var categoria = mapper.Map<Categoria>(categoriaCreationDTO);
            context.Add(categoria);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            return await context.Categoria.ToListAsync();
        }

        [HttpGet("buscaNomeAtivo")]
        public async Task<ActionResult<IEnumerable<Categoria>>> BuscaPorNomeAtivo(string nome, bool ativo)
        {

            if (!String.IsNullOrEmpty(context.Categoria.Where(x => x.Nome.Contains(nome)).ToString()))
            {

                var batata = from num in context.Categoria where num.Nome.Contains(nome) && num.Ativo == ativo select num;
                
                return await batata.ToListAsync();
            }            
            else
            {
                return NotFound();
            }
        }

        [HttpPost("lista")]
        public async Task<ActionResult> Post(CategoriaCreationDTO[] categoriaCreationDTO)
        {
            var categoria = mapper.Map<Categoria[]>(categoriaCreationDTO);
            context.AddRange(categoria);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}/nome")]
        public async Task<ActionResult> Put(int id, CategoriaCreationDTO categoriaCreationDTO)
        {
            var categoria = mapper.Map<Categoria>(categoriaCreationDTO);
            if (categoria == null)
            {
                return NotFound();
            }
            else
            {
                categoria.Id = id;
                context.Update(categoria);
                await context.SaveChangesAsync();
                return Ok();
            }            
        }
    }
}
