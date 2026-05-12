export interface LoginRequest {
  nombreUsuario: string;
  clave: string;
}

export interface LoginResponse {
  token: string;
  expiraEnUtc: string;
  nombreUsuario: string;
  nombreCompleto: string;
}
