using FluentValidation;
using ProjetoDePost.Data.DTOs;

namespace ProjetoDePost.Data.Validations
{
    public class PostagemCreateDtoValidator : AbstractValidator<PostagemCreateDto>
    {
        public PostagemCreateDtoValidator()
        {
            RuleFor(x => x.CampanhaId)
                .GreaterThan(0).WithMessage("O ID da campanha deve ser maior que zero.");
        }
    }
}
