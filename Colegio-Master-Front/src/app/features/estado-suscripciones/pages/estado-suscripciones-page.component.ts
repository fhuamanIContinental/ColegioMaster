import { Component, inject, OnInit, signal } from '@angular/core';
import { EstadoSuscripcionesStore } from '@application/estado-suscripciones/estado-suscripciones.store';
import { EstadoSuscripcion, CrearEstadoSuscripcion, ActualizarEstadoSuscripcion } from '@domain/estado-suscripciones/models/estado-suscripcion.models';
import { EstadoSuscripcionesTablaComponent } from '../components/estado-suscripciones-tabla.component';
import { EstadoSuscripcionesFormularioComponent } from '../components/estado-suscripciones-formulario.component';

@Component({
  selector: 'app-estado-suscripciones-page',
  standalone: true,
  imports: [EstadoSuscripcionesTablaComponent, EstadoSuscripcionesFormularioComponent],
  template: `
    <section>
      <div class="page-header">
        <h2>Estados de Suscripciones</h2>
        @if (!mostrarFormulario()) {
          <button type="button" (click)="onNuevo()">+ Nuevo</button>
        }
      </div>

      @if (store.cargando()) { <p>Cargando...</p> }
      @if (store.error()) { <p class="error">{{ store.error() }}</p> }
      @if (store.mensaje()) { <p class="exito">{{ store.mensaje() }}</p> }

      @if (!mostrarFormulario()) {
        <app-estado-suscripciones-tabla
          [registros]="store.registros()"
          (editar)="onEditar($event)"
          (eliminar)="onEliminar($event)"
        />
      } @else {
        <app-estado-suscripciones-formulario
          [registro]="store.seleccionado()"
          (guardar)="onGuardar($event)"
          (cancelar)="onCancelar()"
        />
      }
    </section>
  `
})
export class EstadoSuscripcionesPageComponent implements OnInit {
  protected readonly store = inject(EstadoSuscripcionesStore);
  protected readonly mostrarFormulario = signal(false);

  async ngOnInit(): Promise<void> {
    await this.store.cargarListadoInicial();
  }

  onNuevo(): void {
    this.store.seleccionado.set(null);
    this.mostrarFormulario.set(true);
  }

  onEditar(item: EstadoSuscripcion): void {
    this.store.seleccionado.set(item);
    this.mostrarFormulario.set(true);
  }

  async onEliminar(item: EstadoSuscripcion): Promise<void> {
    if (!confirm(`¿Eliminar el estado "${item.descripcion}"?`)) return;
    await this.store.eliminar(item.id);
  }

  async onGuardar(payload: CrearEstadoSuscripcion | ActualizarEstadoSuscripcion): Promise<void> {
    const seleccionado = this.store.seleccionado();
    if (seleccionado) {
      await this.store.actualizar(seleccionado.id, payload as ActualizarEstadoSuscripcion);
    } else {
      await this.store.crear(payload as CrearEstadoSuscripcion);
    }
    if (!this.store.error()) {
      this.mostrarFormulario.set(false);
    }
  }

  onCancelar(): void {
    this.store.seleccionado.set(null);
    this.mostrarFormulario.set(false);
  }
}
