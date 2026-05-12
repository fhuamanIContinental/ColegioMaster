import { Injectable } from '@angular/core';
import { ENDPOINTS_API } from '@core/config/api.config';
import { ActualizarCliente, Cliente, CrearCliente } from '@domain/clientes/models/cliente.models';
import { RepositorioClientesPort } from '@domain/clientes/ports/clientes-repository.port';
import { BaseCrudHttpRepository } from '@infrastructure/http/base-crud-http.repository';

@Injectable()
export class RepositorioClientesHttp extends BaseCrudHttpRepository<number, Cliente, CrearCliente, ActualizarCliente> implements RepositorioClientesPort {
  constructor() {
    super(ENDPOINTS_API.clientes);
  }
}
