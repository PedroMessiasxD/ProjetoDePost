using FluentValidation;
using ProjetoDePost.Data.DTOs;

namespace ProjetoDePost.Data.Validations
{
    public class ParticipanteEmpresaCreateDtoValidator : AbstractValidator<ParticipanteEmpresaCreateDto>
    {
        public ParticipanteEmpresaCreateDtoValidator()
        {
            RuleFor(x => x.UsuarioId)
                .NotEmpty().WithMessage("O ID do usuário é obrigatório.")
                .Length(36).WithMessage("O ID do usuário deve ter 36 caracteres."); 

            RuleFor(x => x.EmpresaId)
                .GreaterThan(0).WithMessage("O ID da empresa deve ser maior que zero.");

            RuleFor(x => x.Papel)
                .NotEmpty().WithMessage("O papel do usuário é obrigatório.")
                .Must(p => p == "AdminEmpresarial" || p == "Membro")
                .WithMessage("O papel deve ser 'AdminEmpresarial' ou 'Membro'.");
        }
    }
}
