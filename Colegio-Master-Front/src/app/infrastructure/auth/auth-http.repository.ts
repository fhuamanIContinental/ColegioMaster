import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_BASE_URL, ENDPOINTS_API } from '@core/config/api.config';
import { LoginRequest, LoginResponse } from '@domain/auth/models/auth.models';
import { RepositorioAutenticacionPort } from '@domain/auth/ports/auth-repository.port';
import { RespuestaGeneral } from '@domain/shared/models/api.models';

@Injectable()
export class RepositorioAutenticacionHttp implements RepositorioAutenticacionPort {
  private readonly http = inject(HttpClient);
  private readonly apiBaseUrl = inject(API_BASE_URL);

  iniciarSesion(payload: LoginRequest): Observable<RespuestaGeneral<LoginResponse>> {
    return this.http.post<RespuestaGeneral<LoginResponse>>(
      `${this.apiBaseUrl}/${ENDPOINTS_API.auth}/login`,
      payload
    );
  }
}
