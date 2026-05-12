import { HttpClient, HttpParams } from '@angular/common/http';
import { inject } from '@angular/core';
import { API_BASE_URL } from '@core/config/api.config';
import {
  ConsultaPaginada,
  RespuestaGeneral,
  RespuestaGeneralSinContenido,
  ResultadoPaginado
} from '@domain/shared/models/api.models';
import { Observable } from 'rxjs';

export abstract class BaseCrudHttpRepository<TId, TDto, TCrearDto, TActualizarDto> {
  protected readonly http = inject(HttpClient);
  protected readonly apiBaseUrl = inject(API_BASE_URL);

  protected constructor(private readonly endpoint: string) {}

  listar(numeroPagina = 1, tamanioPagina = 20): Observable<RespuestaGeneral<ResultadoPaginado<TDto>>> {
    const params = new HttpParams()
      .set('numeroPagina', numeroPagina)
      .set('tamanioPagina', tamanioPagina);

    return this.http.get<RespuestaGeneral<ResultadoPaginado<TDto>>>(this.urlBase, { params });
  }

  buscar(consulta: ConsultaPaginada): Observable<RespuestaGeneral<ResultadoPaginado<TDto>>> {
    return this.http.post<RespuestaGeneral<ResultadoPaginado<TDto>>>(`${this.urlBase}/buscar`, consulta);
  }

  obtenerPorId(id: TId): Observable<RespuestaGeneral<TDto>> {
    return this.http.get<RespuestaGeneral<TDto>>(`${this.urlBase}/${id}`);
  }

  crear(payload: TCrearDto): Observable<RespuestaGeneral<TDto>> {
    return this.http.post<RespuestaGeneral<TDto>>(this.urlBase, payload);
  }

  actualizar(id: TId, payload: TActualizarDto): Observable<RespuestaGeneral<TDto>> {
    return this.http.put<RespuestaGeneral<TDto>>(`${this.urlBase}/${id}`, payload);
  }

  eliminar(id: TId): Observable<RespuestaGeneralSinContenido> {
    return this.http.delete<RespuestaGeneralSinContenido>(`${this.urlBase}/${id}`);
  }

  private get urlBase(): string {
    return `${this.apiBaseUrl}/${this.endpoint}`;
  }
}
