import { InjectionToken } from '@angular/core';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

export const ENDPOINTS_API = {
  auth: 'Auth',
  clientes: 'Clientes',
  clienteSuscripciones: 'ClienteSuscripciones',
  estadoClientes: 'EstadoClientes',
  estadoSuscripciones: 'EstadoSuscripciones',
  planes: 'Planes',
  usuariosPlataforma: 'UsuariosPlataforma'
} as const;
