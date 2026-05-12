import { inject, Injectable } from '@angular/core';
import { BaseCrudStore } from '@application/shared/base-crud.store';
import {
  ActualizarEstadoCliente,
  CrearEstadoCliente,
  EstadoCliente
} from '@domain/estado-clientes/models/estado-cliente.models';
import { RepositorioEstadoClientesPort } from '@domain/estado-clientes/ports/estado-clientes-repository.port';

@Injectable({ providedIn: 'root' })
export class EstadoClientesStore extends BaseCrudStore<number, EstadoCliente, CrearEstadoCliente, ActualizarEstadoCliente> {
  constructor() {
    super(inject(RepositorioEstadoClientesPort));
  }
}
