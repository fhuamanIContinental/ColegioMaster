using ColegioMaster.Application.Dtos.Crud;
using FluentValidation;

namespace ColegioMaster.Api.Model.Crud.Validadores;

public class CrearUsuarioPlataformaDtoValidator : AbstractValidator<CrearUsuarioPlataformaDto>
{
    public CrearUsuarioPlataformaDtoValidator()
    {
        RuleFor(x => x.Nombres)
            .NotEmpty().WithMessage("El campo Nombres es obligatorio.")
            .MaximumLength(120).WithMessage("El campo Nombres no puede exceder 120 caracteres.");

        RuleFor(x => x.Apellidos)
            .NotEmpty().WithMessage("El campo Apellidos es obligatorio.")
            .MaximumLength(120).WithMessage("El campo Apellidos no puede exceder 120 caracteres.");

        RuleFor(x => x.Correo)
            .NotEmpty().WithMessage("El campo Correo es obligatorio.")
            .MaximumLength(150).WithMessage("El campo Correo no puede exceder 150 caracteres.")
            .EmailAddress().WithMessage("El campo Correo no tiene un formato valido.");

        RuleFor(x => x.ClaveCifrada)
            .NotEmpty().WithMessage("El campo ClaveCifrada es obligatorio.")
            .MaximumLength(500).WithMessage("El campo ClaveCifrada no puede exceder 500 caracteres.");

        RuleFor(x => x.UsuarioCreacion)
            .NotEmpty().WithMessage("El campo UsuarioCreacion es obligatorio.")
            .MaximumLength(100).WithMessage("El campo UsuarioCreacion no puede exceder 100 caracteres.");
    }
}

public class ActualizarUsuarioPlataformaDtoValidator : AbstractValidator<ActualizarUsuarioPlataformaDto>
{
    public ActualizarUsuarioPlataformaDtoValidator()
    {
        RuleFor(x => x.Nombres)
            .NotEmpty().WithMessage("El campo Nombres es obligatorio.")
            .MaximumLength(120).WithMessage("El campo Nombres no puede exceder 120 caracteres.");

        RuleFor(x => x.Apellidos)
            .NotEmpty().WithMessage("El campo Apellidos es obligatorio.")
            .MaximumLength(120).WithMessage("El campo Apellidos no puede exceder 120 caracteres.");

        RuleFor(x => x.Correo)
            .NotEmpty().WithMessage("El campo Correo es obligatorio.")
            .MaximumLength(150).WithMessage("El campo Correo no puede exceder 150 caracteres.")
            .EmailAddress().WithMessage("El campo Correo no tiene un formato valido.");

        RuleFor(x => x.ClaveCifrada)
            .NotEmpty().WithMessage("El campo ClaveCifrada es obligatorio.")
            .MaximumLength(500).WithMessage("El campo ClaveCifrada no puede exceder 500 caracteres.");

        RuleFor(x => x.IntentosFallidos)
            .GreaterThanOrEqualTo(0).WithMessage("El campo IntentosFallidos no puede ser negativo.");

        RuleFor(x => x.UsuarioModificacion)
            .NotEmpty().WithMessage("El campo UsuarioModificacion es obligatorio.")
            .MaximumLength(100).WithMessage("El campo UsuarioModificacion no puede exceder 100 caracteres.");
    }
}
