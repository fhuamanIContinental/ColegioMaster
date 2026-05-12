import { CrudRepositoryPort } from '@domain/shared/ports/crud-repository.port';
import {
  ActualizarUsuarioPlataforma,
  CrearUsuarioPlataforma,
  UsuarioPlataforma
} from '@domain/usuarios-plataforma/models/usuario-plataforma.models';

export abstract class RepositorioUsuariosPlataformaPort extends CrudRepositoryPort<number, UsuarioPlataforma, CrearUsuarioPlataforma, ActualizarUsuarioPlataforma> {}
