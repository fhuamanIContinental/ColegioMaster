import { Injectable } from '@angular/core';
import { ENDPOINTS_API } from '@core/config/api.config';
import {
  ActualizarClienteSuscripcion,
  ClienteSuscripcion,
  CrearClienteSuscripcion
} from '@domain/cliente-suscripciones/models/cliente-suscripcion.models';
import { RepositorioClienteSuscripcionesPort } from '@domain/cliente-suscripciones/ports/cliente-suscripciones-repository.port';
import { BaseCrudHttpRepository } from '@infrastructure/http/base-crud-http.repository';

@Injectable()
export class RepositorioClienteSuscripcionesHttp extends BaseCrudHttpRepository<number, ClienteSuscripcion, CrearClienteSuscripcion, ActualizarClienteSuscripcion> implements RepositorioClienteSuscripcionesPort {
  constructor() {
    super(ENDPOINTS_API.clienteSuscripciones);
  }
}
