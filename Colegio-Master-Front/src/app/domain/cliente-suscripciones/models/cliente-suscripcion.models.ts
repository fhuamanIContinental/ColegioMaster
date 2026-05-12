export interface ClienteSuscripcion {
  id: number;
  idCliente: number;
  idPlan: number;
  fechaInicio: string;
  fechaFin: string | null;
  modalidad: string;
  montoPactado: number;
  idEstado: number;
  fechaCreacion: string;
  fechaModificacion: string | null;
  usuarioCreacion: string;
  usuarioModificacion: string | null;
}

export interface CrearClienteSuscripcion {
  idCliente: number;
  idPlan: number;
  fechaInicio: string;
  fechaFin: string | null;
  modalidad: string;
  montoPactado: number;
  idEstado: number;
  usuarioCreacion: string;
}

export interface ActualizarClienteSuscripcion {
  idCliente: number;
  idPlan: number;
  fechaInicio: string;
  fechaFin: string | null;
  modalidad: string;
  montoPactado: number;
  idEstado: number;
  usuarioModificacion: string;
}
