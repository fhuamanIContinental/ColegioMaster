import { inject, Injectable } from '@angular/core';
import { BaseCrudStore } from '@application/shared/base-crud.store';
import { ActualizarCliente, Cliente, CrearCliente } from '@domain/clientes/models/cliente.models';
import { RepositorioClientesPort } from '@domain/clientes/ports/clientes-repository.port';

@Injectable({ providedIn: 'root' })
export class ClientesStore extends BaseCrudStore<number, Cliente, CrearCliente, ActualizarCliente> {
  constructor() {
    super(inject(RepositorioClientesPort));
  }
}
