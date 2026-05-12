import { Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { UsuarioPlataforma, CrearUsuarioPlataforma, ActualizarUsuarioPlataforma } from '@domain/usuarios-plataforma/models/usuario-plataforma.models';

@Component({
  selector: 'app-usuarios-plataforma-formulario',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <div class="formulario-container">
      <h3>{{ registro() ? 'Editar' : 'Nuevo' }} Usuario de Plataforma</h3>

      <form [formGroup]="form" (ngSubmit)="onSubmit()">
        <div class="campo">
          <label for="nombres">Nombres *</label>
          <input id="nombres" type="text" formControlName="nombres" />
          @if (form.controls.nombres.invalid && form.controls.nombres.touched) {
            <span class="error">Los nombres son requeridos.</span>
          }
        </div>

        <div class="campo">
          <label for="apellidos">Apellidos *</label>
          <input id="apellidos" type="text" formControlName="apellidos" />
          @if (form.controls.apellidos.invalid && form.controls.apellidos.touched) {
            <span class="error">Los apellidos son requeridos.</span>
          }
        </div>

        <div class="campo">
          <label for="correo">Correo *</label>
          <input id="correo" type="email" formControlName="correo" />
          @if (form.controls.correo.invalid && form.controls.correo.touched) {
            <span class="error">El correo es requerido y debe tener formato válido.</span>
          }
        </div>

        @if (!registro()) {
          <div class="campo">
            <label for="claveCifrada">Contraseña *</label>
            <input id="claveCifrada" type="password" formControlName="claveCifrada" />
            @if (form.controls.claveCifrada.invalid && form.controls.claveCifrada.touched) {
              <span class="error">La contraseña es requerida.</span>
            }
          </div>
        }

        <div class="campo campo-check">
          <input id="estado" type="checkbox" formControlName="estado" />
          <label for="estado">Activo</label>
        </div>

        <div class="acciones">
          <button type="submit" [disabled]="form.invalid">Guardar</button>
          <button type="button" (click)="cancelar.emit()">Cancelar</button>
        </div>
      </form>
    </div>
  `
})
export class UsuariosPlataformaFormularioComponent {
  private readonly fb = inject(FormBuilder);

  readonly registro = input<UsuarioPlataforma | null>(null);
  readonly guardar = output<CrearUsuarioPlataforma | ActualizarUsuarioPlataforma>();
  readonly cancelar = output<void>();

  readonly form = this.fb.group({
    nombres: ['', Validators.required],
    apellidos: ['', Validators.required],
    correo: ['', [Validators.required, Validators.email]],
    claveCifrada: [''],
    estado: [true]
  });

  constructor() {
    effect(() => {
      const r = this.registro();
      if (r) {
        this.form.patchValue({
          nombres: r.nombres,
          apellidos: r.apellidos,
          correo: r.correo,
          estado: r.estado
        });
        this.form.controls.claveCifrada.clearValidators();
      } else {
        this.form.reset({ estado: true });
        this.form.controls.claveCifrada.setValidators(Validators.required);
      }
      this.form.controls.claveCifrada.updateValueAndValidity();
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    const v = this.form.getRawValue();
    const r = this.registro();
    if (r) {
      this.guardar.emit({
        nombres: v.nombres!,
        apellidos: v.apellidos!,
        correo: v.correo!,
        claveCifrada: r.claveCifrada,
        intentosFallidos: r.intentosFallidos,
        bloqueadoHasta: r.bloqueadoHasta,
        ultimoAcceso: r.ultimoAcceso,
        estado: v.estado!,
        usuarioModificacion: 'sistema'
      } as ActualizarUsuarioPlataforma);
    } else {
      this.guardar.emit({
        nombres: v.nombres!,
        apellidos: v.apellidos!,
        correo: v.correo!,
        claveCifrada: v.claveCifrada!,
        estado: v.estado!,
        usuarioCreacion: 'sistema'
      } as CrearUsuarioPlataforma);
    }
  }
}
