import { Injectable } from '@angular/core';
import { ENDPOINTS_API } from '@core/config/api.config';
import {
  ActualizarUsuarioPlataforma,
  CrearUsuarioPlataforma,
  UsuarioPlataforma
} from '@domain/usuarios-plataforma/models/usuario-plataforma.models';
import { RepositorioUsuariosPlataformaPort } from '@domain/usuarios-plataforma/ports/usuarios-plataforma-repository.port';
import { BaseCrudHttpRepository } from '@infrastructure/http/base-crud-http.repository';

@Injectable()
export class RepositorioUsuariosPlataformaHttp extends BaseCrudHttpRepository<number, UsuarioPlataforma, CrearUsuarioPlataforma, ActualizarUsuarioPlataforma> implements RepositorioUsuariosPlataformaPort {
  constructor() {
    super(ENDPOINTS_API.usuariosPlataforma);
  }
}
