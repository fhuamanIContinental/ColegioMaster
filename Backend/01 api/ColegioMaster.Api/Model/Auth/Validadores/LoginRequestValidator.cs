using FluentValidation;

namespace ColegioMaster.Api.Model.Auth.Validadores;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.NombreUsuario)
            .NotEmpty().WithMessage("Debe ingresar nombre de usuario.")
            .MaximumLength(150).WithMessage("El nombre de usuario no puede superar 150 caracteres.");

        RuleFor(x => x.Clave)
            .NotEmpty().WithMessage("Debe ingresar la clave.")
            .MaximumLength(200).WithMessage("La clave no puede superar 200 caracteres.");
    }
}
