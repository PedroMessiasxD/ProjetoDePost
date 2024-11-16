using FluentValidation;
using ProjetoDePost.Data.DTOs;

namespace ProjetoDePost.Data.Validations
{
    public class CampanhaCreateDtoValidator : AbstractValidator<CampanhaCreateDto>
    {
        public CampanhaCreateDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da campanha é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da campanha deve ter no máximo 100 caracteres.");

            RuleFor(x => x.TemaPrincipal)
                .NotEmpty().WithMessage("O tema principal é obrigatório.")
                .MaximumLength(200).WithMessage("O tema principal deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Frequencia)
                .GreaterThan(0).WithMessage("A frequência deve ser maior que zero.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição da campanha é obrigatória.")
                .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres.");
        }
    }
}
