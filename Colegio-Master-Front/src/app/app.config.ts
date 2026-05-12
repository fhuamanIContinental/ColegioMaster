import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { API_BASE_URL } from '@core/config/api.config';
import { authTokenInterceptor } from '@core/interceptors/auth-token.interceptor';
import { apiErrorInterceptor } from '@core/interceptors/api-error.interceptor';
import { RepositorioAutenticacionPort } from '@domain/auth/ports/auth-repository.port';
import { RepositorioClienteSuscripcionesPort } from '@domain/cliente-suscripciones/ports/cliente-suscripciones-repository.port';
import { RepositorioClientesPort } from '@domain/clientes/ports/clientes-repository.port';
import { RepositorioEstadoClientesPort } from '@domain/estado-clientes/ports/estado-clientes-repository.port';
import { RepositorioEstadoSuscripcionesPort } from '@domain/estado-suscripciones/ports/estado-suscripciones-repository.port';
import { RepositorioPlanesPort } from '@domain/planes/ports/planes-repository.port';
import { RepositorioUsuariosPlataformaPort } from '@domain/usuarios-plataforma/ports/usuarios-plataforma-repository.port';
import { RepositorioAutenticacionHttp } from '@infrastructure/auth/auth-http.repository';
import { RepositorioClienteSuscripcionesHttp } from '@infrastructure/cliente-suscripciones/cliente-suscripciones-http.repository';
import { RepositorioClientesHttp } from '@infrastructure/clientes/clientes-http.repository';
import { RepositorioEstadoClientesHttp } from '@infrastructure/estado-clientes/estado-clientes-http.repository';
import { RepositorioEstadoSuscripcionesHttp } from '@infrastructure/estado-suscripciones/estado-suscripciones-http.repository';
import { RepositorioPlanesHttp } from '@infrastructure/planes/planes-http.repository';
import { RepositorioUsuariosPlataformaHttp } from '@infrastructure/usuarios-plataforma/usuarios-plataforma-http.repository';
import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authTokenInterceptor, apiErrorInterceptor])),
    { provide: API_BASE_URL, useValue: '/api' },
    { provide: RepositorioAutenticacionPort, useClass: RepositorioAutenticacionHttp },
    { provide: RepositorioClientesPort, useClass: RepositorioClientesHttp },
    { provide: RepositorioClienteSuscripcionesPort, useClass: RepositorioClienteSuscripcionesHttp },
    { provide: RepositorioEstadoClientesPort, useClass: RepositorioEstadoClientesHttp },
    { provide: RepositorioEstadoSuscripcionesPort, useClass: RepositorioEstadoSuscripcionesHttp },
    { provide: RepositorioPlanesPort, useClass: RepositorioPlanesHttp },
    { provide: RepositorioUsuariosPlataformaPort, useClass: RepositorioUsuariosPlataformaHttp }
  ]
};
