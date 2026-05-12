using ColegioMaster.Application.Dtos.Crud;

namespace ColegioMaster.Application.Servicios.Interfaces;

public interface IPlanService : ICrudServicio<int, PlanDto, CrearPlanDto, ActualizarPlanDto>
{
}
