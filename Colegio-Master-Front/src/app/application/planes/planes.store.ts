import { inject, Injectable } from '@angular/core';
import { BaseCrudStore } from '@application/shared/base-crud.store';
import { ActualizarPlan, CrearPlan, Plan } from '@domain/planes/models/plan.models';
import { RepositorioPlanesPort } from '@domain/planes/ports/planes-repository.port';

@Injectable({ providedIn: 'root' })
export class PlanesStore extends BaseCrudStore<number, Plan, CrearPlan, ActualizarPlan> {
  constructor() {
    super(inject(RepositorioPlanesPort));
  }
}
