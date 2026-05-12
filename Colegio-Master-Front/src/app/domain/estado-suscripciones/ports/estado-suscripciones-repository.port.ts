import { CrudRepositoryPort } from '@domain/shared/ports/crud-repository.port';
import {
  ActualizarEstadoSuscripcion,
  CrearEstadoSuscripcion,
  EstadoSuscripcion
} from '@domain/estado-suscripciones/models/estado-suscripcion.models';

export abstract class RepositorioEstadoSuscripcionesPort extends CrudRepositoryPort<number, EstadoSuscripcion, CrearEstadoSuscripcion, ActualizarEstadoSuscripcion> {}
