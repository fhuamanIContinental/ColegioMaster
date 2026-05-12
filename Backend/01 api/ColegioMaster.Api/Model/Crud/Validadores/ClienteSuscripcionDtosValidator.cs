using ColegioMaster.Application.Dtos.Crud;
using FluentValidation;

namespace ColegioMaster.Api.Model.Crud.Validadores;

public class CrearClienteSuscripcionDtoValidator : AbstractValidator<CrearClienteSuscripcionDto>
{
    public CrearClienteSuscripcionDtoValidator()
    {
        RuleFor(x => x.IdCliente)
            .GreaterThan(0).WithMessage("El campo IdCliente debe ser mayor a cero.");

        RuleFor(x => x.IdPlan)
            .GreaterThan(0).WithMessage("El campo IdPlan debe ser mayor a cero.");

        RuleFor(x => x.IdEstado)
            .GreaterThan(0).WithMessage("El campo IdEstado debe ser mayor a cero.");

        RuleFor(x => x.Modalidad)
            .NotEmpty().WithMessage("El campo Modalidad es obligatorio.")
            .MaximumLength(20).WithMessage("El campo Modalidad no puede exceder 20 caracteres.");

        RuleFor(x => x.MontoPactado)
            .GreaterThanOrEqualTo(0).WithMessage("El campo MontoPactado no puede ser negativo.");

        RuleFor(x => x.UsuarioCreacion)
            .NotEmpty().WithMessage("El campo UsuarioCreacion es obligatorio.")
            .MaximumLength(100).WithMessage("El campo UsuarioCreacion no puede exceder 100 caracteres.");

        RuleFor(x => x)
            .Must(x => !x.FechaFin.HasValue || x.FechaFin.Value >= x.FechaInicio)
            .WithMessage("FechaFin no puede ser menor a FechaInicio.");
    }
}

public class ActualizarClienteSuscripcionDtoValidator : AbstractValidator<ActualizarClienteSuscripcionDto>
{
    public ActualizarClienteSuscripcionDtoValidator()
    {
        RuleFor(x => x.IdCliente)
            .GreaterThan(0).WithMessage("El campo IdCliente debe ser mayor a cero.");

        RuleFor(x => x.IdPlan)
            .GreaterThan(0).WithMessage("El campo IdPlan debe ser mayor a cero.");

        RuleFor(x => x.IdEstado)
            .GreaterThan(0).WithMessage("El campo IdEstado debe ser mayor a cero.");

        RuleFor(x => x.Modalidad)
            .NotEmpty().WithMessage("El campo Modalidad es obligatorio.")
            .MaximumLength(20).WithMessage("El campo Modalidad no puede exceder 20 caracteres.");

        RuleFor(x => x.MontoPactado)
            .GreaterThanOrEqualTo(0).WithMessage("El campo MontoPactado no puede ser negativo.");

        RuleFor(x => x.UsuarioModificacion)
            .NotEmpty().WithMessage("El campo UsuarioModificacion es obligatorio.")
            .MaximumLength(100).WithMessage("El campo UsuarioModificacion no puede exceder 100 caracteres.");

        RuleFor(x => x)
            .Must(x => !x.FechaFin.HasValue || x.FechaFin.Value >= x.FechaInicio)
            .WithMessage("FechaFin no puede ser menor a FechaInicio.");
    }
}
