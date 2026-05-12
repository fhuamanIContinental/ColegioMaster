export interface Plan {
  id: number;
  codigo: string;
  nombre: string;
  precioMensual: number;
  precioAnual: number;
  maxEstudiante: number | null;
  maxUsuario: number | null;
  estado: boolean;
  fechaCreacion: string;
  fechaModificacion: string | null;
  usuarioCreacion: string;
  usuarioModificacion: string | null;
}

export interface CrearPlan {
  codigo: string;
  nombre: string;
  precioMensual: number;
  precioAnual: number;
  maxEstudiante: number | null;
  maxUsuario: number | null;
  estado: boolean;
  usuarioCreacion: string;
}

export interface ActualizarPlan {
  codigo: string;
  nombre: string;
  precioMensual: number;
  precioAnual: number;
  maxEstudiante: number | null;
  maxUsuario: number | null;
  estado: boolean;
  usuarioModificacion: string;
}
