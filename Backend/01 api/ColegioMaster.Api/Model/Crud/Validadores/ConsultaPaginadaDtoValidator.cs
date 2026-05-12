using ColegioMaster.Application.Dtos.Comun;
using FluentValidation;

namespace ColegioMaster.Api.Model.Crud.Validadores;

public class ConsultaPaginadaDtoValidator : AbstractValidator<ConsultaPaginadaDto>
{
    public ConsultaPaginadaDtoValidator()
    {
        RuleFor(x => x.NumeroPagina)
            .GreaterThan(0).WithMessage("NumeroPagina debe ser mayor a cero.");

        RuleFor(x => x.TamanioPagina)
            .GreaterThan(0).WithMessage("TamanioPagina debe ser mayor a cero.")
            .LessThanOrEqualTo(200).WithMessage("TamanioPagina no puede exceder 200.");

        RuleForEach(x => x.Filters)
            .ChildRules(filtro =>
            {
                filtro.RuleFor(f => f.Campo)
                    .NotEmpty().WithMessage("El campo Campo del filtro es obligatorio.");

                filtro.RuleFor(f => f.Operador)
                    .NotEmpty().WithMessage("El campo Operador del filtro es obligatorio.");

                filtro.RuleFor(f => f.Valor)
                    .NotEmpty().WithMessage("El campo Valor del filtro es obligatorio.");
            });
    }
}
