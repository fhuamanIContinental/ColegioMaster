import { Observable } from 'rxjs';
import { LoginRequest, LoginResponse } from '@domain/auth/models/auth.models';
import { RespuestaGeneral } from '@domain/shared/models/api.models';

export abstract class RepositorioAutenticacionPort {
  abstract iniciarSesion(payload: LoginRequest): Observable<RespuestaGeneral<LoginResponse>>;
}
