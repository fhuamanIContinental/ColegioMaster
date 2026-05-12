export interface Cliente {
  id: number;
  ruc: string;
  codigo: string;
  razonSocial: string;
  nombreComercial: string;
  direccion: string | null;
  telefono: string | null;
  correoContacto: string | null;
  servidorSql: string;
  bdNombre: string;
  bdUsuario: string | null;
  bdPasswordCifrada: string | null;
  idEstado: number;
  fechaActivacion: string | null;
  fechaCreacion: string;
  fechaModificacion: string | null;
  usuarioCreacion: string;
  usuarioModificacion: string | null;
}

export interface CrearCliente {
  ruc: string;
  codigo: string;
  razonSocial: string;
  nombreComercial: string;
  direccion: string | null;
  telefono: string | null;
  correoContacto: string | null;
  servidorSql: string;
  bdNombre: string;
  bdUsuario: string | null;
  bdPasswordCifrada: string | null;
  idEstado: number;
  fechaActivacion: string | null;
  usuarioCreacion: string;
}

export interface ActualizarCliente {
  ruc: string;
  codigo: string;
  razonSocial: string;
  nombreComercial: string;
  direccion: string | null;
  telefono: string | null;
  correoContacto: string | null;
  servidorSql: string;
  bdNombre: string;
  bdUsuario: string | null;
  bdPasswordCifrada: string | null;
  idEstado: number;
  fechaActivacion: string | null;
  usuarioModificacion: string;
}
