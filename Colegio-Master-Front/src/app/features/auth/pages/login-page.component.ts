import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthStore } from '@application/auth/auth.store';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <section class="login">
      <form class="login__form" [formGroup]="formulario" (ngSubmit)="iniciarSesion()">
        <h1>Ingreso</h1>

        <label>
          Usuario
          <input type="text" formControlName="nombreUsuario" />
        </label>

        <label>
          Clave
          <input type="password" formControlName="clave" />
        </label>

        <button type="submit" [disabled]="formulario.invalid || authStore.cargando()">
          {{ authStore.cargando() ? 'Ingresando...' : 'Ingresar' }}
        </button>

        @if (authStore.error()) {
          <p class="error">{{ authStore.error() }}</p>
        }
      </form>
    </section>
  `,
  styles: `
    :host {
      display: grid;
      min-height: 100vh;
      place-items: center;
      background: #f3f6f9;
      font-family: 'Segoe UI', sans-serif;
    }

    .login__form {
      display: grid;
      gap: 16px;
      width: min(420px, calc(100vw - 32px));
      padding: 24px;
      border: 1px solid #d8e0ea;
      border-radius: 12px;
      background: #ffffff;
    }

    label {
      display: grid;
      gap: 8px;
    }

    input {
      padding: 10px 12px;
    }

    .error {
      color: #b42318;
    }
  `
})
export class LoginPageComponent {
  protected readonly authStore = inject(AuthStore);
  private readonly fb = inject(FormBuilder);

  protected readonly formulario = this.fb.nonNullable.group({
    nombreUsuario: ['', Validators.required],
    clave: ['', Validators.required]
  });

  protected async iniciarSesion(): Promise<void> {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    await this.authStore.iniciarSesion(this.formulario.getRawValue());
  }
}
