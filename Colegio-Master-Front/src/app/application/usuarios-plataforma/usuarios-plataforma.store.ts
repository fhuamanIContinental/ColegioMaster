import { inject, Injectable } from '@angular/core';
import { BaseCrudStore } from '@application/shared/base-crud.store';
import {
  ActualizarUsuarioPlataforma,
  CrearUsuarioPlataforma,
  UsuarioPlataforma
} from '@domain/usuarios-plataforma/models/usuario-plataforma.models';
import { RepositorioUsuariosPlataformaPort } from '@domain/usuarios-plataforma/ports/usuarios-plataforma-repository.port';

@Injectable({ providedIn: 'root' })
export class UsuariosPlataformaStore extends BaseCrudStore<number, UsuarioPlataforma, CrearUsuarioPlataforma, ActualizarUsuarioPlataforma> {
  constructor() {
    super(inject(RepositorioUsuariosPlataformaPort));
  }
}
