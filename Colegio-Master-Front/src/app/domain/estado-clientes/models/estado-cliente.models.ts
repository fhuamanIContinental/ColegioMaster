export interface EstadoCliente {
  id: number;
  codigo: string;
  descripcion: string;
}

export interface CrearEstadoCliente {
  id: number;
  codigo: string;
  descripcion: string;
}

export interface ActualizarEstadoCliente {
  codigo: string;
  descripcion: string;
}
