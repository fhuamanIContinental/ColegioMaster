import { inject, Injectable } from '@angular/core';
import { BaseCrudStore } from '@application/shared/base-crud.store';
import {
  ActualizarClienteSuscripcion,
  ClienteSuscripcion,
  CrearClienteSuscripcion
} from '@domain/cliente-suscripciones/models/cliente-suscripcion.models';
import { RepositorioClienteSuscripcionesPort } from '@domain/cliente-suscripciones/ports/cliente-suscripciones-repository.port';

@Injectable({ providedIn: 'root' })
export class ClienteSuscripcionesStore extends BaseCrudStore<number, ClienteSuscripcion, CrearClienteSuscripcion, ActualizarClienteSuscripcion> {
  constructor() {
    super(inject(RepositorioClienteSuscripcionesPort));
  }
}
