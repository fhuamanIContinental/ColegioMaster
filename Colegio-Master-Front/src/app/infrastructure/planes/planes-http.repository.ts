import { Injectable } from '@angular/core';
import { ENDPOINTS_API } from '@core/config/api.config';
import { ActualizarPlan, CrearPlan, Plan } from '@domain/planes/models/plan.models';
import { RepositorioPlanesPort } from '@domain/planes/ports/planes-repository.port';
import { BaseCrudHttpRepository } from '@infrastructure/http/base-crud-http.repository';

@Injectable()
export class RepositorioPlanesHttp extends BaseCrudHttpRepository<number, Plan, CrearPlan, ActualizarPlan> implements RepositorioPlanesPort {
  constructor() {
    super(ENDPOINTS_API.planes);
  }
}
