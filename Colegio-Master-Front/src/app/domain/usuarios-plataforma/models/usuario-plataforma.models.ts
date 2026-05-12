export interface UsuarioPlataforma {
  id: number;
  nombres: string;
  apellidos: string;
  correo: string;
  claveCifrada: string;
  intentosFallidos: number;
  bloqueadoHasta: string | null;
  ultimoAcceso: string | null;
  estado: boolean;
  fechaCreacion: string;
  fechaModificacion: string | null;
  usuarioCreacion: string;
  usuarioModificacion: string | null;
}

export interface CrearUsuarioPlataforma {
  nombres: string;
  apellidos: string;
  correo: string;
  claveCifrada: string;
  estado: boolean;
  usuarioCreacion: string;
}

export interface ActualizarUsuarioPlataforma {
  nombres: string;
  apellidos: string;
  correo: string;
  claveCifrada: string;
  intentosFallidos: number;
  bloqueadoHasta: string | null;
  ultimoAcceso: string | null;
  estado: boolean;
  usuarioModificacion: string;
}
