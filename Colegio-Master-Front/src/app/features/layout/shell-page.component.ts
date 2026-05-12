import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthStore } from '@application/auth/auth.store';

@Component({
  selector: 'app-shell-page',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, RouterOutlet],
  template: `
    <div class="shell">
      <aside class="shell__menu">
        <h1>Colegio Master</h1>
        <p>Angular 21, signals, interceptores y guardias.</p>

        <nav class="shell__nav">
          <a routerLink="/inicio" routerLinkActive="activo">Inicio</a>
          <a routerLink="/clientes" routerLinkActive="activo">Clientes</a>
          <a routerLink="/cliente-suscripciones" routerLinkActive="activo">Cliente Suscripciones</a>
          <a routerLink="/estado-clientes" routerLinkActive="activo">Estado Clientes</a>
          <a routerLink="/estado-suscripciones" routerLinkActive="activo">Estado Suscripciones</a>
          <a routerLink="/planes" routerLinkActive="activo">Planes</a>
          <a routerLink="/usuarios-plataforma" routerLinkActive="activo">Usuarios Plataforma</a>
        </nav>

        <section class="shell__sesion">
          <strong>{{ authStore.nombreCompleto() ?? 'Sin sesión' }}</strong>
          <span>{{ authStore.nombreUsuario() ?? '' }}</span>
          <button type="button" (click)="authStore.cerrarSesion()">Cerrar sesión</button>
        </section>
      </aside>

      <main class="shell__contenido">
        <router-outlet />
      </main>
    </div>
  `,
  styles: `
    :host {
      display: block;
      min-height: 100vh;
      font-family: 'Segoe UI', sans-serif;
      color: #17212b;
    }

    .shell {
      display: grid;
      grid-template-columns: 280px 1fr;
      min-height: 100vh;
    }

    .shell__menu {
      padding: 24px;
      border-right: 1px solid #d8e0ea;
      background: #f7f9fb;
      display: flex;
      flex-direction: column;
      gap: 24px;
    }

    .shell__nav {
      display: flex;
      flex-direction: column;
      gap: 8px;
    }

    .shell__nav a {
      color: #24415f;
      text-decoration: none;
      padding: 8px 12px;
      border-radius: 8px;
    }

    .shell__nav a.activo {
      background: #dde9f5;
      font-weight: 600;
    }

    .shell__sesion {
      margin-top: auto;
      display: flex;
      flex-direction: column;
      gap: 8px;
    }

    .shell__sesion button {
      width: fit-content;
    }

    .shell__contenido {
      padding: 24px;
      background: #ffffff;
    }

    @media (max-width: 960px) {
      .shell {
        grid-template-columns: 1fr;
      }

      .shell__menu {
        border-right: 0;
        border-bottom: 1px solid #d8e0ea;
      }
    }
  `
})
export class ShellPageComponent {
  protected readonly authStore = inject(AuthStore);
}
