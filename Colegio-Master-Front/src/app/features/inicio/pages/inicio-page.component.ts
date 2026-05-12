import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthStore } from '@application/auth/auth.store';

@Component({
  selector: 'app-inicio-page',
  standalone: true,
  imports: [RouterLink],
  template: `
    <section>
      <h2>Inicio</h2>
      <p>La estructura ya está lista para consumir todos los endpoints actuales del backend.</p>

      <ul>
        <li><a routerLink="/clientes">Clientes</a></li>
        <li><a routerLink="/cliente-suscripciones">Cliente Suscripciones</a></li>
        <li><a routerLink="/estado-clientes">Estado Clientes</a></li>
        <li><a routerLink="/estado-suscripciones">Estado Suscripciones</a></li>
        <li><a routerLink="/planes">Planes</a></li>
        <li><a routerLink="/usuarios-plataforma">Usuarios Plataforma</a></li>
      </ul>

      <p>Usuario autenticado: {{ authStore.nombreCompleto() ?? 'Sin datos' }}</p>
    </section>
  `
})
export class InicioPageComponent {
  protected readonly authStore = inject(AuthStore);
}
