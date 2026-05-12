import { CrudRepositoryPort } from '@domain/shared/ports/crud-repository.port';
import {
  ActualizarClienteSuscripcion,
  ClienteSuscripcion,
  CrearClienteSuscripcion
} from '@domain/cliente-suscripciones/models/cliente-suscripcion.models';

export abstract class RepositorioClienteSuscripcionesPort extends CrudRepositoryPort<number, ClienteSuscripcion, CrearClienteSuscripcion, ActualizarClienteSuscripcion> {}
