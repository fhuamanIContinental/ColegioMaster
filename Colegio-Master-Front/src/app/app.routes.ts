import { Routes } from '@angular/router';
import { authGuard } from '@core/guards/auth.guard';
import { publicGuard } from '@core/guards/public.guard';

export const routes: Routes = [
  {
    path: 'auth/login',
    canActivate: [publicGuard],
    loadComponent: () => import('@features/auth/pages/login-page.component').then((m) => m.LoginPageComponent)
  },
  {
    path: '',
    canActivate: [authGuard],
    loadComponent: () => import('@features/layout/shell-page.component').then((m) => m.ShellPageComponent),
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'inicio'
      },
      {
        path: 'inicio',
        loadComponent: () => import('@features/inicio/pages/inicio-page.component').then((m) => m.InicioPageComponent)
      },
      {
        path: 'clientes',
        loadComponent: () => import('@features/clientes/pages/clientes-page.component').then((m) => m.ClientesPageComponent)
      },
      {
        path: 'cliente-suscripciones',
        loadComponent: () => import('@features/cliente-suscripciones/pages/cliente-suscripciones-page.component').then((m) => m.ClienteSuscripcionesPageComponent)
      },
      {
        path: 'estado-clientes',
        loadComponent: () => import('@features/estado-clientes/pages/estado-clientes-page.component').then((m) => m.EstadoClientesPageComponent)
      },
      {
        path: 'estado-suscripciones',
        loadComponent: () => import('@features/estado-suscripciones/pages/estado-suscripciones-page.component').then((m) => m.EstadoSuscripcionesPageComponent)
      },
      {
        path: 'planes',
        loadComponent: () => import('@features/planes/pages/planes-page.component').then((m) => m.PlanesPageComponent)
      },
      {
        path: 'usuarios-plataforma',
        loadComponent: () => import('@features/usuarios-plataforma/pages/usuarios-plataforma-page.component').then((m) => m.UsuariosPlataformaPageComponent)
      }
    ]
  },
  {
    path: '**',
    redirectTo: 'inicio'
  }
];
