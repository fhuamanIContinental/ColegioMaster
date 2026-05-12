using ColegioMaster.Application.Dtos.Crud;
using FluentValidation;

namespace ColegioMaster.Api.Model.Crud.Validadores;

public class CrearClienteDtoValidator : AbstractValidator<CrearClienteDto>
{
    public CrearClienteDtoValidator()
    {
        RuleFor(x => x.Ruc)
            .NotEmpty().WithMessage("El campo Ruc es obligatorio.")
            .MaximumLength(11).WithMessage("El campo Ruc no puede exceder 11 caracteres.");

        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El campo Codigo es obligatorio.")
            .MaximumLength(30).WithMessage("El campo Codigo no puede exceder 30 caracteres.");

        RuleFor(x => x.RazonSocial)
            .NotEmpty().WithMessage("El campo RazonSocial es obligatorio.")
            .MaximumLength(200).WithMessage("El campo RazonSocial no puede exceder 200 caracteres.");

        RuleFor(x => x.NombreComercial)
            .NotEmpty().WithMessage("El campo NombreComercial es obligatorio.")
            .MaximumLength(200).WithMessage("El campo NombreComercial no puede exceder 200 caracteres.");

        RuleFor(x => x.ServidorSql)
            .NotEmpty().WithMessage("El campo ServidorSql es obligatorio.")
            .MaximumLength(120).WithMessage("El campo ServidorSql no puede exceder 120 caracteres.");

        RuleFor(x => x.BdNombre)
            .NotEmpty().WithMessage("El campo BdNombre es obligatorio.")
            .MaximumLength(120).WithMessage("El campo BdNombre no puede exceder 120 caracteres.");

        RuleFor(x => x.UsuarioCreacion)
            .NotEmpty().WithMessage("El campo UsuarioCreacion es obligatorio.")
            .MaximumLength(100).WithMessage("El campo UsuarioCreacion no puede exceder 100 caracteres.");

        RuleFor(x => x.IdEstado)
            .GreaterThan(0).WithMessage("El campo IdEstado debe ser mayor a cero.");
    }
}

public class ActualizarClienteDtoValidator : AbstractValidator<ActualizarClienteDto>
{
    public ActualizarClienteDtoValidator()
    {
        RuleFor(x => x.Ruc)
            .NotEmpty().WithMessage("El campo Ruc es obligatorio.")
            .MaximumLength(11).WithMessage("El campo Ruc no puede exceder 11 caracteres.");

        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El campo Codigo es obligatorio.")
            .MaximumLength(30).WithMessage("El campo Codigo no puede exceder 30 caracteres.");

        RuleFor(x => x.RazonSocial)
            .NotEmpty().WithMessage("El campo RazonSocial es obligatorio.")
            .MaximumLength(200).WithMessage("El campo RazonSocial no puede exceder 200 caracteres.");

        RuleFor(x => x.NombreComercial)
            .NotEmpty().WithMessage("El campo NombreComercial es obligatorio.")
            .MaximumLength(200).WithMessage("El campo NombreComercial no puede exceder 200 caracteres.");

        RuleFor(x => x.ServidorSql)
            .NotEmpty().WithMessage("El campo ServidorSql es obligatorio.")
            .MaximumLength(120).WithMessage("El campo ServidorSql no puede exceder 120 caracteres.");

        RuleFor(x => x.BdNombre)
            .NotEmpty().WithMessage("El campo BdNombre es obligatorio.")
            .MaximumLength(120).WithMessage("El campo BdNombre no puede exceder 120 caracteres.");

        RuleFor(x => x.UsuarioModificacion)
            .NotEmpty().WithMessage("El campo UsuarioModificacion es obligatorio.")
            .MaximumLength(100).WithMessage("El campo UsuarioModificacion no puede exceder 100 caracteres.");

        RuleFor(x => x.IdEstado)
            .GreaterThan(0).WithMessage("El campo IdEstado debe ser mayor a cero.");
    }
}
