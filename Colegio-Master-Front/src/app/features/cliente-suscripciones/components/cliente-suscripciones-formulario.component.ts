import { Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClienteSuscripcion, CrearClienteSuscripcion, ActualizarClienteSuscripcion } from '@domain/cliente-suscripciones/models/cliente-suscripcion.models';

@Component({
  selector: 'app-cliente-suscripciones-formulario',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <div class="formulario-container">
      <h3>{{ registro() ? 'Editar' : 'Nueva' }} Suscripción de Cliente</h3>

      <form [formGroup]="form" (ngSubmit)="onSubmit()">
        <div class="campo">
          <label for="idCliente">ID Cliente *</label>
          <input id="idCliente" type="number" formControlName="idCliente" />
          @if (form.controls.idCliente.invalid && form.controls.idCliente.touched) {
            <span class="error">El ID de cliente es requerido.</span>
          }
        </div>

        <div class="campo">
          <label for="idPlan">ID Plan *</label>
          <input id="idPlan" type="number" formControlName="idPlan" />
          @if (form.controls.idPlan.invalid && form.controls.idPlan.touched) {
            <span class="error">El ID de plan es requerido.</span>
          }
        </div>

        <div class="campo">
          <label for="modalidad">Modalidad *</label>
          <select id="modalidad" formControlName="modalidad">
            <option value="">Seleccione...</option>
            <option value="MENSUAL">Mensual</option>
            <option value="ANUAL">Anual</option>
          </select>
          @if (form.controls.modalidad.invalid && form.controls.modalidad.touched) {
            <span class="error">La modalidad es requerida.</span>
          }
        </div>

        <div class="campo">
          <label for="montoPactado">Monto Pactado *</label>
          <input id="montoPactado" type="number" step="0.01" min="0" formControlName="montoPactado" />
          @if (form.controls.montoPactado.invalid && form.controls.montoPactado.touched) {
            <span class="error">El monto es requerido.</span>
          }
        </div>

        <div class="campo">
          <label for="fechaInicio">Fecha Inicio *</label>
          <input id="fechaInicio" type="date" formControlName="fechaInicio" />
          @if (form.controls.fechaInicio.invalid && form.controls.fechaInicio.touched) {
            <span class="error">La fecha de inicio es requerida.</span>
          }
        </div>

        <div class="campo">
          <label for="fechaFin">Fecha Fin</label>
          <input id="fechaFin" type="date" formControlName="fechaFin" />
        </div>

        <div class="campo">
          <label for="idEstado">ID Estado *</label>
          <input id="idEstado" type="number" formControlName="idEstado" />
          @if (form.controls.idEstado.invalid && form.controls.idEstado.touched) {
            <span class="error">El estado es requerido.</span>
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
export class ClienteSuscripcionesFormularioComponent {
  private readonly fb = inject(FormBuilder);

  readonly registro = input<ClienteSuscripcion | null>(null);
  readonly guardar = output<CrearClienteSuscripcion | ActualizarClienteSuscripcion>();
  readonly cancelar = output<void>();

  readonly form = this.fb.group({
    idCliente: [null as number | null, Validators.required],
    idPlan: [null as number | null, Validators.required],
    modalidad: ['', Validators.required],
    montoPactado: [0, [Validators.required, Validators.min(0)]],
    fechaInicio: ['', Validators.required],
    fechaFin: [null as string | null],
    idEstado: [1, Validators.required]
  });

  constructor() {
    effect(() => {
      const r = this.registro();
      if (r) {
        this.form.patchValue({
          idCliente: r.idCliente,
          idPlan: r.idPlan,
          modalidad: r.modalidad,
          montoPactado: r.montoPactado,
          fechaInicio: r.fechaInicio?.slice(0, 10) ?? '',
          fechaFin: r.fechaFin?.slice(0, 10) ?? null,
          idEstado: r.idEstado
        });
      } else {
        this.form.reset({ idEstado: 1, montoPactado: 0 });
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    const v = this.form.getRawValue();
    this.guardar.emit({
      idCliente: v.idCliente!,
      idPlan: v.idPlan!,
      modalidad: v.modalidad!,
      montoPactado: v.montoPactado!,
      fechaInicio: v.fechaInicio!,
      fechaFin: v.fechaFin ?? null,
      idEstado: v.idEstado!
    } as CrearClienteSuscripcion | ActualizarClienteSuscripcion);
  }
}
