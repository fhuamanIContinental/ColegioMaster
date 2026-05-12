import { Component, inject, OnInit, signal } from '@angular/core';
import { ClienteSuscripcionesStore } from '@application/cliente-suscripciones/cliente-suscripciones.store';
import { ClienteSuscripcion, CrearClienteSuscripcion, ActualizarClienteSuscripcion } from '@domain/cliente-suscripciones/models/cliente-suscripcion.models';
import { ClienteSuscripcionesTablaComponent } from '../components/cliente-suscripciones-tabla.component';
import { ClienteSuscripcionesFormularioComponent } from '../components/cliente-suscripciones-formulario.component';

@Component({
  selector: 'app-cliente-suscripciones-page',
  standalone: true,
  imports: [ClienteSuscripcionesTablaComponent, ClienteSuscripcionesFormularioComponent],
  template: `
    <section>
      <div class="page-header">
        <h2>Suscripciones de Clientes</h2>
        @if (!mostrarFormulario()) {
          <button type="button" (click)="onNuevo()">+ Nueva</button>
        }
      </div>

      @if (store.cargando()) { <p>Cargando...</p> }
      @if (store.error()) { <p class="error">{{ store.error() }}</p> }
      @if (store.mensaje()) { <p class="exito">{{ store.mensaje() }}</p> }

      @if (!mostrarFormulario()) {
        <app-cliente-suscripciones-tabla
          [registros]="store.registros()"
          (editar)="onEditar($event)"
          (eliminar)="onEliminar($event)"
        />
      } @else {
        <app-cliente-suscripciones-formulario
          [registro]="store.seleccionado()"
          (guardar)="onGuardar($event)"
          (cancelar)="onCancelar()"
        />
      }
    </section>
  `
})
export class ClienteSuscripcionesPageComponent implements OnInit {
  protected readonly store = inject(ClienteSuscripcionesStore);
  protected readonly mostrarFormulario = signal(false);

  async ngOnInit(): Promise<void> {
    await this.store.cargarListadoInicial();
  }

  onNuevo(): void {
    this.store.seleccionado.set(null);
    this.mostrarFormulario.set(true);
  }

  onEditar(item: ClienteSuscripcion): void {
    this.store.seleccionado.set(item);
    this.mostrarFormulario.set(true);
  }

  async onEliminar(item: ClienteSuscripcion): Promise<void> {
    if (!confirm(`¿Eliminar la suscripción #${item.id}?`)) return;
    await this.store.eliminar(item.id);
  }

  async onGuardar(payload: CrearClienteSuscripcion | ActualizarClienteSuscripcion): Promise<void> {
    const seleccionado = this.store.seleccionado();
    if (seleccionado) {
      await this.store.actualizar(seleccionado.id, payload as ActualizarClienteSuscripcion);
    } else {
      await this.store.crear(payload as CrearClienteSuscripcion);
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
