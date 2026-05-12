import { Observable } from 'rxjs';
import {
  ConsultaPaginada,
  RespuestaGeneral,
  RespuestaGeneralSinContenido,
  ResultadoPaginado
} from '@domain/shared/models/api.models';

export abstract class CrudRepositoryPort<TId, TDto, TCrearDto, TActualizarDto> {
  abstract listar(numeroPagina?: number, tamanioPagina?: number): Observable<RespuestaGeneral<ResultadoPaginado<TDto>>>;
  abstract buscar(consulta: ConsultaPaginada): Observable<RespuestaGeneral<ResultadoPaginado<TDto>>>;
  abstract obtenerPorId(id: TId): Observable<RespuestaGeneral<TDto>>;
  abstract crear(payload: TCrearDto): Observable<RespuestaGeneral<TDto>>;
  abstract actualizar(id: TId, payload: TActualizarDto): Observable<RespuestaGeneral<TDto>>;
  abstract eliminar(id: TId): Observable<RespuestaGeneralSinContenido>;
}
