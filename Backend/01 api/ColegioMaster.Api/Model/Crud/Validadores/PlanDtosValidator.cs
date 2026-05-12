using ColegioMaster.Application.Dtos.Crud;
using FluentValidation;

namespace ColegioMaster.Api.Model.Crud.Validadores;

public class CrearPlanDtoValidator : AbstractValidator<CrearPlanDto>
{
    public CrearPlanDtoValidator()
    {
        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El campo Codigo es obligatorio.")
            .MaximumLength(30).WithMessage("El campo Codigo no puede exceder 30 caracteres.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El campo Nombre es obligatorio.")
            .MaximumLength(120).WithMessage("El campo Nombre no puede exceder 120 caracteres.");

        RuleFor(x => x.PrecioMensual)
            .GreaterThanOrEqualTo(0).WithMessage("El campo PrecioMensual no puede ser negativo.");

        RuleFor(x => x.PrecioAnual)
            .GreaterThanOrEqualTo(0).WithMessage("El campo PrecioAnual no puede ser negativo.");

        RuleFor(x => x.MaxEstudiante)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxEstudiante.HasValue)
            .WithMessage("El campo MaxEstudiante no puede ser negativo.");

        RuleFor(x => x.MaxUsuario)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxUsuario.HasValue)
            .WithMessage("El campo MaxUsuario no puede ser negativo.");

        RuleFor(x => x.UsuarioCreacion)
            .NotEmpty().WithMessage("El campo UsuarioCreacion es obligatorio.")
            .MaximumLength(100).WithMessage("El campo UsuarioCreacion no puede exceder 100 caracteres.");
    }
}

public class ActualizarPlanDtoValidator : AbstractValidator<ActualizarPlanDto>
{
    public ActualizarPlanDtoValidator()
    {
        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El campo Codigo es obligatorio.")
            .MaximumLength(30).WithMessage("El campo Codigo no puede exceder 30 caracteres.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El campo Nombre es obligatorio.")
            .MaximumLength(120).WithMessage("El campo Nombre no puede exceder 120 caracteres.");

        RuleFor(x => x.PrecioMensual)
            .GreaterThanOrEqualTo(0).WithMessage("El campo PrecioMensual no puede ser negativo.");

        RuleFor(x => x.PrecioAnual)
            .GreaterThanOrEqualTo(0).WithMessage("El campo PrecioAnual no puede ser negativo.");

        RuleFor(x => x.MaxEstudiante)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxEstudiante.HasValue)
            .WithMessage("El campo MaxEstudiante no puede ser negativo.");

        RuleFor(x => x.MaxUsuario)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxUsuario.HasValue)
            .WithMessage("El campo MaxUsuario no puede ser negativo.");

        RuleFor(x => x.UsuarioModificacion)
            .NotEmpty().WithMessage("El campo UsuarioModificacion es obligatorio.")
            .MaximumLength(100).WithMessage("El campo UsuarioModificacion no puede exceder 100 caracteres.");
    }
}
