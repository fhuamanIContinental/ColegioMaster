import { computed, inject, Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { LoginRequest, LoginResponse } from '@domain/auth/models/auth.models';
import { RepositorioAutenticacionPort } from '@domain/auth/ports/auth-repository.port';

const STORAGE_KEY = 'colegio-master.sesion';

@Injectable({ providedIn: 'root' })
export class AuthStore {
  private readonly router = inject(Router);
  private readonly repositorio = inject(RepositorioAutenticacionPort);
  private readonly sesionInicial = this.leerSesion();

  readonly cargando = signal(false);
  readonly error = signal<string | null>(null);
  readonly sesion = signal<LoginResponse | null>(this.sesionInicial);
  readonly token = computed(() => this.sesion()?.token ?? null);
  readonly nombreCompleto = computed(() => this.sesion()?.nombreCompleto ?? null);
  readonly nombreUsuario = computed(() => this.sesion()?.nombreUsuario ?? null);
  readonly estaAutenticado = computed(() => Boolean(this.token()));

  async iniciarSesion(payload: LoginRequest): Promise<boolean> {
    this.cargando.set(true);
    this.error.set(null);

    try {
      const respuesta = await firstValueFrom(this.repositorio.iniciarSesion(payload));

      if (!respuesta.success || !respuesta.content) {
        this.error.set(respuesta.textMessage || 'Credenciales inválidas.');
        return false;
      }

      this.sesion.set(respuesta.content);
      this.persistirSesion(respuesta.content);
      await this.router.navigateByUrl('/inicio');
      return true;
    } catch (error) {
      this.error.set(error instanceof Error ? error.message : 'No se pudo iniciar sesión.');
      return false;
    } finally {
      this.cargando.set(false);
    }
  }

  async cerrarSesion(): Promise<void> {
    this.sesion.set(null);
    localStorage.removeItem(STORAGE_KEY);
    await this.router.navigateByUrl('/auth/login');
  }

  private persistirSesion(sesion: LoginResponse): void {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(sesion));
  }

  private leerSesion(): LoginResponse | null {
    const raw = localStorage.getItem(STORAGE_KEY);

    if (!raw) {
      return null;
    }

    try {
      return JSON.parse(raw) as LoginResponse;
    } catch {
      localStorage.removeItem(STORAGE_KEY);
      return null;
    }
  }
}
