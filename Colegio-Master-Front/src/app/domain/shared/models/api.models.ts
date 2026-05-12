export interface RespuestaGeneral<T> {
  success: boolean;
  titleMessage: string;
  textMessage: string;
  showAlert: boolean;
  content: T | null;
}

export interface RespuestaGeneralSinContenido {
  success: boolean;
  titleMessage: string;
  textMessage: string;
  showAlert: boolean;
}

export interface FiltroConsulta {
  campo: string;
  operador: string;
  valor: string | null;
}

export interface ConsultaPaginada {
  numeroPagina: number;
  tamanioPagina: number;
  filters: FiltroConsulta[];
}

export interface ResultadoPaginado<T> {
  numeroPagina: number;
  tamanioPagina: number;
  totalRegistros: number;
  totalPaginas: number;
  registros: T[];
}

export const consultaInicial = (numeroPagina = 1, tamanioPagina = 20): ConsultaPaginada => ({
  numeroPagina,
  tamanioPagina,
  filters: []
});
