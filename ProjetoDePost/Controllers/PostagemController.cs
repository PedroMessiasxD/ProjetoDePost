using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemService _postagemService;
        private readonly IMapper _mapper;

        public PostagemController(IPostagemService postagemService, IMapper mapper)
        {
            _postagemService = postagemService;
            _mapper = mapper;
        }
        /// <summary>
        /// Obtém todas as postagens de uma campanha específica.
        /// </summary>
        [HttpGet("campanha/{campanhaId}")]
        public async Task<ActionResult<IEnumerable<PostagemReadDto>>> ObterPostagensPorCampanha(int campanhaId)
        {
            try
            {
                var postagens = await _postagemService.BuscarPorCampanhaIdAsync(campanhaId);
                if (postagens == null)
                {
                    return NotFound("Não há postagens para esta campanha.");
                }
                return Ok(postagens);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria uma nova postagem.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PostagemReadDto>> CriarPostagemAsync(PostagemCreateDto postagemCreateDto)
        {
            try
            {
                var postagem = await _postagemService.CriarPostagemAsync(postagemCreateDto);
                return CreatedAtAction(nameof(ObterPostagemPorId), new { id = postagem.Id }, postagem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        /// <summary>
        /// Obtém uma postagem específica pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostagemReadDto>> ObterPostagemPorId(int id)
        {
            try
            {
                var postagem = await _postagemService.ObterPostagemPorIdAsync(id);
                if (postagem == null)
                {
                    return NotFound("Postagem não encontrada.");
                }
                return Ok(postagem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deleta uma postagem existente.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarPostagemAsync(int id)
        {
            try
            {
                await _postagemService.DeletarPostagemAsync(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
