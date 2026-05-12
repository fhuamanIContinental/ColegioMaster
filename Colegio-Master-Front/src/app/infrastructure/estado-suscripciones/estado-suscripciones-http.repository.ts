import { Injectable } from '@angular/core';
import { ENDPOINTS_API } from '@core/config/api.config';
import {
  ActualizarEstadoSuscripcion,
  CrearEstadoSuscripcion,
  EstadoSuscripcion
} from '@domain/estado-suscripciones/models/estado-suscripcion.models';
import { RepositorioEstadoSuscripcionesPort } from '@domain/estado-suscripciones/ports/estado-suscripciones-repository.port';
import { BaseCrudHttpRepository } from '@infrastructure/http/base-crud-http.repository';

@Injectable()
export class RepositorioEstadoSuscripcionesHttp extends BaseCrudHttpRepository<number, EstadoSuscripcion, CrearEstadoSuscripcion, ActualizarEstadoSuscripcion> implements RepositorioEstadoSuscripcionesPort {
  constructor() {
    super(ENDPOINTS_API.estadoSuscripciones);
  }
}
