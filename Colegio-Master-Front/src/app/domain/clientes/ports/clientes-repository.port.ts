import { CrudRepositoryPort } from '@domain/shared/ports/crud-repository.port';
import { ActualizarCliente, Cliente, CrearCliente } from '@domain/clientes/models/cliente.models';

export abstract class RepositorioClientesPort extends CrudRepositoryPort<number, Cliente, CrearCliente, ActualizarCliente> {}
