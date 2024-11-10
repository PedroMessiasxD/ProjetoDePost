using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Exceptions;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;
using ProjetoDePost.Validations;

namespace ProjetoDePost.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly UserManager<Usuario> _userManager;
        private readonly IMapper _mapper;
        private readonly IParticipanteEmpresaRepository _participanteEmpresaRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IParticipanteEmpresaService _participanteEmpresaService;
        
        public UsuarioService(IGenericRepository<Usuario> usuarioRepository, UserManager<Usuario> userManager, IMapper mapper, RoleManager<IdentityRole>roleManager, IParticipanteEmpresaService participanteEmpresaService)
        {
            _usuarioRepository = usuarioRepository;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _participanteEmpresaService = participanteEmpresaService;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        public async Task<Usuario> CriarUsuarioAsync(Usuario usuario, UsuarioCreateDto usuarioCreateDto)
        {
            var validator = new UsuarioCreateDtoValidator();
            var validationResult = await validator.ValidateAsync(usuarioCreateDto);

            if (!validationResult.IsValid)
            {
                throw new UsuarioCriacaoException(validationResult.Errors.First().ErrorMessage);
            }
            usuario.UserName = usuarioCreateDto.Email;
            var resultado = await _userManager.CreateAsync(usuario, usuarioCreateDto.Senha);
            if (!resultado.Succeeded)
            {
                throw new UsuarioCriacaoException("Erro ao criar o usuário: " + string.Join(", ", resultado.Errors.Select(e => e.Description)));
            }

            return usuario;
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        public async Task<Usuario> AtualizarUsuarioAsync(Usuario usuario)
        {
            var usuarioExistente = await _usuarioRepository.BuscarPorIdAsync(usuario.Id);
            if (usuarioExistente == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Sobrenome = usuario.Sobrenome;
            await _usuarioRepository.AtualizarAsync(usuarioExistente);
            return usuarioExistente;
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        public async Task<bool> DeletarUsuarioAsync(string usuarioId)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            await _usuarioRepository.DeletarAsync(usuarioId);
            return true;
        }

        /// <summary>
        /// Busca todos os usuários.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        public async Task<IEnumerable<UsuarioReadDto>> BuscarTodosUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.BuscarTodosAsync();
            return _mapper.Map<IEnumerable<UsuarioReadDto>>(usuarios);
        }

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        /// <returns>O usuário encontrado.</returns>
        public async Task<Usuario> BuscarUsuarioPorIdAsync(string id)
        {
            return await _usuarioRepository.BuscarPorIdAsync(id);
        }

        /// <summary>
        /// Promove um usuário a administrador.
        /// </summary>
        public async Task<bool> PromoverUsuarioAsync(string usuarioId)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            // Adiciona o usuário à role "Admin"
            var result = await _userManager.AddToRoleAsync(usuario, "AdminEmpresarial");
            return result.Succeeded;
        }

        /// <summary>
        /// Promove um usuário a administrador empresarial de uma empresa específica por e-mail.
        /// </summary>
        public async Task<bool> PromoverUsuarioPorEmailAsync(string email, int empresaId)
        {

            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }
            
            // Verificar se a role AdminEmpresarial já existe, caso contrário, criar
            var roleExists = await _roleManager.RoleExistsAsync("AdminEmpresarial");
            if (!roleExists)
            {
                var role = new IdentityRole("AdminEmpresarial");
                await _roleManager.CreateAsync(role);
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(usuario, "AdminEmpresarial");
            if (!addToRoleResult.Succeeded)
            {
                throw new Exception("Erro ao associar usuário à role AdminEmpresarial.");
            }

            // Verificar se o usuário já tem a role AdminEmpresarial, se não, adiciona
            if (!await _userManager.IsInRoleAsync(usuario, "AdminEmpresarial"))
            {
                await _userManager.AddToRoleAsync(usuario, "AdminEmpresarial");
            }

            // Cria o DTO de ParticipanteEmpresaCreateDto
            var participanteEmpresaCreateDto = new ParticipanteEmpresaCreateDto
            {
                UsuarioId = usuario.Id,
                EmpresaId = empresaId,
                Papel = "AdminEmpresarial"
            };
            
            await _participanteEmpresaService.AdicionarUsuarioNaEmpresaAsync(participanteEmpresaCreateDto);

            return true;
        }

        /// <summary>
        /// Busca um usuário pelo email.
        /// </summary>
        public async Task<Usuario> BuscarUsuarioPorEmailAsync(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            return usuario;
        }
    }
}
