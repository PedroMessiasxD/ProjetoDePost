using FluentValidation;
using ProjetoDePost.Data.DTOs;

namespace ProjetoDePost.Data.Validations
{
    public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
    {
        public UsuarioCreateDtoValidator()
        {
            RuleFor(x => x.Senha)
                .Equal(x => x.ConfirmarSenha).WithMessage("A senha e a confirmação de senha devem ser iguais.")
                .NotEmpty().WithMessage("A confirmação de senha é obrigatória.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
            .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches(@"[0-9]").WithMessage("A senha deve conter pelo menos um número.")
            .Matches(@"[\W_]").WithMessage("A senha deve conter pelo menos um caractere especial.");
        }
    }
}
