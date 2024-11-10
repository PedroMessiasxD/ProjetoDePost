using FluentValidation;
using ProjetoDePost.Data.DTOs;

namespace ProjetoDePost.Validations
{
    public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
    {
        public UsuarioCreateDtoValidator()
        {
            RuleFor(x => x.Senha)
                .Equal(x => x.ConfirmarSenha).WithMessage("A senha e a confirmação de senha devem ser iguais.");
        }
    }
}
