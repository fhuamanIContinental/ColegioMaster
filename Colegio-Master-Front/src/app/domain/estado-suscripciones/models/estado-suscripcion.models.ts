export interface EstadoSuscripcion {
  id: number;
  codigo: string;
  descripcion: string;
}

export interface CrearEstadoSuscripcion {
  id: number;
  codigo: string;
  descripcion: string;
}

export interface ActualizarEstadoSuscripcion {
  codigo: string;
  descripcion: string;
}
