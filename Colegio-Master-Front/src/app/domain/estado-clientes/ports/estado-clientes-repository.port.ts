import { CrudRepositoryPort } from '@domain/shared/ports/crud-repository.port';
import {
  ActualizarEstadoCliente,
  CrearEstadoCliente,
  EstadoCliente
} from '@domain/estado-clientes/models/estado-cliente.models';

export abstract class RepositorioEstadoClientesPort extends CrudRepositoryPort<number, EstadoCliente, CrearEstadoCliente, ActualizarEstadoCliente> {}
