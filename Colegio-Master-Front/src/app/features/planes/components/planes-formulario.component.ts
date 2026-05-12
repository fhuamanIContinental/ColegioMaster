import { Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Plan, CrearPlan, ActualizarPlan } from '@domain/planes/models/plan.models';

@Component({
  selector: 'app-planes-formulario',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <div class="formulario-container">
      <h3>{{ registro() ? 'Editar' : 'Nuevo' }} Plan</h3>

      <form [formGroup]="form" (ngSubmit)="onSubmit()">
        <div class="campo">
          <label for="codigo">Código *</label>
          <input id="codigo" type="text" formControlName="codigo" />
          @if (form.controls.codigo.invalid && form.controls.codigo.touched) {
            <span class="error">El código es requerido.</span>
          }
        </div>

        <div class="campo">
          <label for="nombre">Nombre *</label>
          <input id="nombre" type="text" formControlName="nombre" />
          @if (form.controls.nombre.invalid && form.controls.nombre.touched) {
            <span class="error">El nombre es requerido.</span>
          }
        </div>

        <div class="campo">
          <label for="precioMensual">Precio Mensual *</label>
          <input id="precioMensual" type="number" step="0.01" min="0" formControlName="precioMensual" />
          @if (form.controls.precioMensual.invalid && form.controls.precioMensual.touched) {
            <span class="error">El precio mensual es requerido.</span>
          }
        </div>

        <div class="campo">
          <label for="precioAnual">Precio Anual *</label>
          <input id="precioAnual" type="number" step="0.01" min="0" formControlName="precioAnual" />
          @if (form.controls.precioAnual.invalid && form.controls.precioAnual.touched) {
            <span class="error">El precio anual es requerido.</span>
          }
        </div>

        <div class="campo">
          <label for="maxEstudiante">Máx. Estudiantes</label>
          <input id="maxEstudiante" type="number" min="0" formControlName="maxEstudiante" placeholder="Vacío = ilimitado" />
        </div>

        <div class="campo">
          <label for="maxUsuario">Máx. Usuarios</label>
          <input id="maxUsuario" type="number" min="0" formControlName="maxUsuario" placeholder="Vacío = ilimitado" />
        </div>

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
export class PlanesFormularioComponent {
  private readonly fb = inject(FormBuilder);

  readonly registro = input<Plan | null>(null);
  readonly guardar = output<CrearPlan | ActualizarPlan>();
  readonly cancelar = output<void>();

  readonly form = this.fb.group({
    codigo: ['', Validators.required],
    nombre: ['', Validators.required],
    precioMensual: [0, [Validators.required, Validators.min(0)]],
    precioAnual: [0, [Validators.required, Validators.min(0)]],
    maxEstudiante: [null as number | null],
    maxUsuario: [null as number | null],
    estado: [true]
  });

  constructor() {
    effect(() => {
      const r = this.registro();
      if (r) {
        this.form.patchValue({
          codigo: r.codigo,
          nombre: r.nombre,
          precioMensual: r.precioMensual,
          precioAnual: r.precioAnual,
          maxEstudiante: r.maxEstudiante,
          maxUsuario: r.maxUsuario,
          estado: r.estado
        });
      } else {
        this.form.reset({ estado: true, precioMensual: 0, precioAnual: 0 });
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    const v = this.form.getRawValue();
    this.guardar.emit({
      codigo: v.codigo!,
      nombre: v.nombre!,
      precioMensual: v.precioMensual!,
      precioAnual: v.precioAnual!,
      maxEstudiante: v.maxEstudiante ?? null,
      maxUsuario: v.maxUsuario ?? null,
      estado: v.estado!
    } as CrearPlan | ActualizarPlan);
  }
}
