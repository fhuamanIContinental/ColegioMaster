import { CrudRepositoryPort } from '@domain/shared/ports/crud-repository.port';
import { ActualizarPlan, CrearPlan, Plan } from '@domain/planes/models/plan.models';

export abstract class RepositorioPlanesPort extends CrudRepositoryPort<number, Plan, CrearPlan, ActualizarPlan> {}
