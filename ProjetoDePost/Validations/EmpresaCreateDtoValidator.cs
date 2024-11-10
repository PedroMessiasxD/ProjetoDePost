using FluentValidation;
using ProjetoDePost.Data.DTOs;

namespace ProjetoDePost.Validations
{
    public class EmpresaCreateDtoValidator : AbstractValidator<EmpresaCreateDto>
    {
        public EmpresaCreateDtoValidator()
        {
            RuleFor(e => e.Nome)
               .NotEmpty().WithMessage("O nome da empresa é obrigatório.");

            RuleFor(e => e.Descricao)
                .NotEmpty().WithMessage("A descrição da empresa é obrigatória.");

            RuleFor(e => e.SetorAtuacao)
                .NotEmpty().WithMessage("O setor de atuação é obrigatório.");

            RuleFor(e => e.LinksRedesSociais)
                .MaximumLength(30).WithMessage("Os links de redes sociais não podem exceder 30 caracteres.");
        }
    }
}
