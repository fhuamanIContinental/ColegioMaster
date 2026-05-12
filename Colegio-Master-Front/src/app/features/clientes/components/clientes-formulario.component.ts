import { Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Cliente, CrearCliente, ActualizarCliente } from '@domain/clientes/models/cliente.models';

@Component({
  selector: 'app-clientes-formulario',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <div class="formulario-container">
      <h3>{{ registro() ? 'Editar' : 'Nuevo' }} Cliente</h3>

      <form [formGroup]="form" (ngSubmit)="onSubmit()">
        <fieldset>
          <legend>Datos Generales</legend>

          <div class="campo">
            <label for="codigo">Código *</label>
            <input id="codigo" type="text" formControlName="codigo" />
            @if (form.controls.codigo.invalid && form.controls.codigo.touched) {
              <span class="error">El código es requerido.</span>
            }
          </div>

          <div class="campo">
            <label for="ruc">RUC *</label>
            <input id="ruc" type="text" formControlName="ruc" />
            @if (form.controls.ruc.invalid && form.controls.ruc.touched) {
              <span class="error">El RUC es requerido.</span>
            }
          </div>

          <div class="campo">
            <label for="razonSocial">Razón Social *</label>
            <input id="razonSocial" type="text" formControlName="razonSocial" />
            @if (form.controls.razonSocial.invalid && form.controls.razonSocial.touched) {
              <span class="error">La razón social es requerida.</span>
            }
          </div>

          <div class="campo">
            <label for="nombreComercial">Nombre Comercial *</label>
            <input id="nombreComercial" type="text" formControlName="nombreComercial" />
            @if (form.controls.nombreComercial.invalid && form.controls.nombreComercial.touched) {
              <span class="error">El nombre comercial es requerido.</span>
            }
          </div>

          <div class="campo">
            <label for="direccion">Dirección</label>
            <input id="direccion" type="text" formControlName="direccion" />
          </div>

          <div class="campo">
            <label for="telefono">Teléfono</label>
            <input id="telefono" type="text" formControlName="telefono" />
          </div>

          <div class="campo">
            <label for="correoContacto">Correo de Contacto</label>
            <input id="correoContacto" type="email" formControlName="correoContacto" />
          </div>
        </fieldset>

        <fieldset>
          <legend>Datos de Base de Datos</legend>

          <div class="campo">
            <label for="servidorSql">Servidor SQL *</label>
            <input id="servidorSql" type="text" formControlName="servidorSql" />
            @if (form.controls.servidorSql.invalid && form.controls.servidorSql.touched) {
              <span class="error">El servidor SQL es requerido.</span>
            }
          </div>

          <div class="campo">
            <label for="bdNombre">Nombre BD *</label>
            <input id="bdNombre" type="text" formControlName="bdNombre" />
            @if (form.controls.bdNombre.invalid && form.controls.bdNombre.touched) {
              <span class="error">El nombre de la BD es requerido.</span>
            }
          </div>

          <div class="campo">
            <label for="bdUsuario">Usuario BD</label>
            <input id="bdUsuario" type="text" formControlName="bdUsuario" />
          </div>

          <div class="campo">
            <label for="bdPasswordCifrada">Contraseña BD</label>
            <input id="bdPasswordCifrada" type="password" formControlName="bdPasswordCifrada" />
          </div>
        </fieldset>

        <fieldset>
          <legend>Estado y Activación</legend>

          <div class="campo">
            <label for="idEstado">ID Estado *</label>
            <input id="idEstado" type="number" formControlName="idEstado" />
            @if (form.controls.idEstado.invalid && form.controls.idEstado.touched) {
              <span class="error">El estado es requerido.</span>
            }
          </div>

          <div class="campo">
            <label for="fechaActivacion">Fecha Activación</label>
            <input id="fechaActivacion" type="date" formControlName="fechaActivacion" />
          </div>
        </fieldset>

        <div class="acciones">
          <button type="submit" [disabled]="form.invalid">Guardar</button>
          <button type="button" (click)="cancelar.emit()">Cancelar</button>
        </div>
      </form>
    </div>
  `
})
export class ClientesFormularioComponent {
  private readonly fb = inject(FormBuilder);

  readonly registro = input<Cliente | null>(null);
  readonly guardar = output<CrearCliente | ActualizarCliente>();
  readonly cancelar = output<void>();

  readonly form = this.fb.group({
    codigo: ['', Validators.required],
    ruc: ['', Validators.required],
    razonSocial: ['', Validators.required],
    nombreComercial: ['', Validators.required],
    direccion: [null as string | null],
    telefono: [null as string | null],
    correoContacto: [null as string | null],
    servidorSql: ['', Validators.required],
    bdNombre: ['', Validators.required],
    bdUsuario: [null as string | null],
    bdPasswordCifrada: [null as string | null],
    idEstado: [1, Validators.required],
    fechaActivacion: [null as string | null]
  });

  constructor() {
    effect(() => {
      const r = this.registro();
      if (r) {
        this.form.patchValue({
          codigo: r.codigo,
          ruc: r.ruc,
          razonSocial: r.razonSocial,
          nombreComercial: r.nombreComercial,
          direccion: r.direccion,
          telefono: r.telefono,
          correoContacto: r.correoContacto,
          servidorSql: r.servidorSql,
          bdNombre: r.bdNombre,
          bdUsuario: r.bdUsuario,
          bdPasswordCifrada: r.bdPasswordCifrada,
          idEstado: r.idEstado,
          fechaActivacion: r.fechaActivacion
        });
      } else {
        this.form.reset({ idEstado: 1 });
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    const v = this.form.getRawValue();
    this.guardar.emit({
      codigo: v.codigo!,
      ruc: v.ruc!,
      razonSocial: v.razonSocial!,
      nombreComercial: v.nombreComercial!,
      direccion: v.direccion ?? null,
      telefono: v.telefono ?? null,
      correoContacto: v.correoContacto ?? null,
      servidorSql: v.servidorSql!,
      bdNombre: v.bdNombre!,
      bdUsuario: v.bdUsuario ?? null,
      bdPasswordCifrada: v.bdPasswordCifrada ?? null,
      idEstado: v.idEstado!,
      fechaActivacion: v.fechaActivacion ?? null
    } as CrearCliente | ActualizarCliente);
  }
}
