import { Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { EstadoCliente, CrearEstadoCliente, ActualizarEstadoCliente } from '@domain/estado-clientes/models/estado-cliente.models';

@Component({
  selector: 'app-estado-clientes-formulario',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <div class="formulario-container">
      <h3>{{ registro() ? 'Editar' : 'Nuevo' }} Estado de Cliente</h3>

      <form [formGroup]="form" (ngSubmit)="onSubmit()">
        <div class="campo">
          <label for="codigo">Código *</label>
          <input id="codigo" type="text" formControlName="codigo" />
          @if (form.controls.codigo.invalid && form.controls.codigo.touched) {
            <span class="error">El código es requerido.</span>
          }
        </div>

        <div class="campo">
          <label for="descripcion">Descripción *</label>
          <input id="descripcion" type="text" formControlName="descripcion" />
          @if (form.controls.descripcion.invalid && form.controls.descripcion.touched) {
            <span class="error">La descripción es requerida.</span>
          }
        </div>

        <div class="acciones">
          <button type="submit" [disabled]="form.invalid">Guardar</button>
          <button type="button" (click)="cancelar.emit()">Cancelar</button>
        </div>
      </form>
    </div>
  `
})
export class EstadoClientesFormularioComponent {
  private readonly fb = inject(FormBuilder);

  readonly registro = input<EstadoCliente | null>(null);
  readonly guardar = output<CrearEstadoCliente | ActualizarEstadoCliente>();
  readonly cancelar = output<void>();

  readonly form = this.fb.group({
    codigo: ['', Validators.required],
    descripcion: ['', Validators.required]
  });

  constructor() {
    effect(() => {
      const r = this.registro();
      if (r) {
        this.form.patchValue({ codigo: r.codigo, descripcion: r.descripcion });
      } else {
        this.form.reset();
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    const { codigo, descripcion } = this.form.getRawValue();
    this.guardar.emit({ codigo: codigo!, descripcion: descripcion! });
  }
}
