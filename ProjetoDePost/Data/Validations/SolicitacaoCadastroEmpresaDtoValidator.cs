using FluentValidation;
using ProjetoDePost.Data.DTOs;

namespace ProjetoDePost.Data.Validations
{
    public class SolicitacaoCadastroEmpresaDtoValidator : AbstractValidator<SolicitacaoCadastroEmpresaDto>
    {
        public SolicitacaoCadastroEmpresaDtoValidator()
        {
            RuleFor(x => x.NomeEmpresa)
                .NotEmpty().WithMessage("O nome da empresa é obrigatório.");

            RuleFor(x => x.DescricaoEmpresa)
           .NotEmpty().WithMessage("A descrição da empresa é obrigatória.");

            RuleFor(x => x.SetorAtuacao)
           .NotEmpty().WithMessage("O setor de atuação é obrigatório.");

            RuleFor(x => x.LinkRedeSocial)
            .Must(link => Uri.TryCreate(link, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.LinkRedeSocial))
            .WithMessage("O link da rede social deve ser uma URL válida.");

            RuleFor(x => x.NomeAdministrador)
           .NotEmpty().WithMessage("O nome do administrador é obrigatório.");

            RuleFor(x => x.EmailAdministrador)
            .NotEmpty().WithMessage("O email do administrador é obrigatório.")
            .EmailAddress().WithMessage("O email do administrador deve ser um endereço de email válido.");

            RuleFor(x => x.TelefoneAdministrador)
            .NotEmpty().WithMessage("O telefone do administrador é obrigatório.");
        }
    }
}
