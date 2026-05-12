import { inject, Injectable } from '@angular/core';
import { BaseCrudStore } from '@application/shared/base-crud.store';
import {
  ActualizarEstadoSuscripcion,
  CrearEstadoSuscripcion,
  EstadoSuscripcion
} from '@domain/estado-suscripciones/models/estado-suscripcion.models';
import { RepositorioEstadoSuscripcionesPort } from '@domain/estado-suscripciones/ports/estado-suscripciones-repository.port';

@Injectable({ providedIn: 'root' })
export class EstadoSuscripcionesStore extends BaseCrudStore<number, EstadoSuscripcion, CrearEstadoSuscripcion, ActualizarEstadoSuscripcion> {
  constructor() {
    super(inject(RepositorioEstadoSuscripcionesPort));
  }
}
