using ColegioMaster.Application.Dtos.Crud;
using FluentValidation;

namespace ColegioMaster.Api.Model.Crud.Validadores;

public class CrearEstadoClienteDtoValidator : AbstractValidator<CrearEstadoClienteDto>
{
    public CrearEstadoClienteDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El campo Id debe ser mayor a cero.");

        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El campo Codigo es obligatorio.")
            .MaximumLength(30).WithMessage("El campo Codigo no puede exceder 30 caracteres.");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("El campo Descripcion es obligatorio.")
            .MaximumLength(100).WithMessage("El campo Descripcion no puede exceder 100 caracteres.");
    }
}

public class ActualizarEstadoClienteDtoValidator : AbstractValidator<ActualizarEstadoClienteDto>
{
    public ActualizarEstadoClienteDtoValidator()
    {
        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El campo Codigo es obligatorio.")
            .MaximumLength(30).WithMessage("El campo Codigo no puede exceder 30 caracteres.");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("El campo Descripcion es obligatorio.")
            .MaximumLength(100).WithMessage("El campo Descripcion no puede exceder 100 caracteres.");
    }
}
