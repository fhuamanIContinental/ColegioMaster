import { Injectable } from '@angular/core';
import { ENDPOINTS_API } from '@core/config/api.config';
import {
  ActualizarEstadoCliente,
  CrearEstadoCliente,
  EstadoCliente
} from '@domain/estado-clientes/models/estado-cliente.models';
import { RepositorioEstadoClientesPort } from '@domain/estado-clientes/ports/estado-clientes-repository.port';
import { BaseCrudHttpRepository } from '@infrastructure/http/base-crud-http.repository';

@Injectable()
export class RepositorioEstadoClientesHttp extends BaseCrudHttpRepository<number, EstadoCliente, CrearEstadoCliente, ActualizarEstadoCliente> implements RepositorioEstadoClientesPort {
  constructor() {
    super(ENDPOINTS_API.estadoClientes);
  }
}
