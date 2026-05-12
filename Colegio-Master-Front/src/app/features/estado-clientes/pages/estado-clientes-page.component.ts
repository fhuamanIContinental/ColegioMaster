import { Component, inject, OnInit, signal } from '@angular/core';
import { EstadoClientesStore } from '@application/estado-clientes/estado-clientes.store';
import { EstadoCliente, CrearEstadoCliente, ActualizarEstadoCliente } from '@domain/estado-clientes/models/estado-cliente.models';
import { EstadoClientesTablaComponent } from '../components/estado-clientes-tabla.component';
import { EstadoClientesFormularioComponent } from '../components/estado-clientes-formulario.component';

@Component({
  selector: 'app-estado-clientes-page',
  standalone: true,
  imports: [EstadoClientesTablaComponent, EstadoClientesFormularioComponent],
  template: `
    <section>
      <div class="page-header">
        <h2>Estados de Clientes</h2>
        @if (!mostrarFormulario()) {
          <button type="button" (click)="onNuevo()">+ Nuevo</button>
        }
      </div>

      @if (store.cargando()) { <p>Cargando...</p> }
      @if (store.error()) { <p class="error">{{ store.error() }}</p> }
      @if (store.mensaje()) { <p class="exito">{{ store.mensaje() }}</p> }

      @if (!mostrarFormulario()) {
        <app-estado-clientes-tabla
          [registros]="store.registros()"
          (editar)="onEditar($event)"
          (eliminar)="onEliminar($event)"
        />
      } @else {
        <app-estado-clientes-formulario
          [registro]="store.seleccionado()"
          (guardar)="onGuardar($event)"
          (cancelar)="onCancelar()"
        />
      }
    </section>
  `
})
export class EstadoClientesPageComponent implements OnInit {
  protected readonly store = inject(EstadoClientesStore);
  protected readonly mostrarFormulario = signal(false);

  async ngOnInit(): Promise<void> {
    await this.store.cargarListadoInicial();
  }

  onNuevo(): void {
    this.store.seleccionado.set(null);
    this.mostrarFormulario.set(true);
  }

  onEditar(item: EstadoCliente): void {
    this.store.seleccionado.set(item);
    this.mostrarFormulario.set(true);
  }

  async onEliminar(item: EstadoCliente): Promise<void> {
    if (!confirm(`¿Eliminar el estado "${item.descripcion}"?`)) return;
    await this.store.eliminar(item.id);
  }

  async onGuardar(payload: CrearEstadoCliente | ActualizarEstadoCliente): Promise<void> {
    const seleccionado = this.store.seleccionado();
    if (seleccionado) {
      await this.store.actualizar(seleccionado.id, payload as ActualizarEstadoCliente);
    } else {
      await this.store.crear(payload as CrearEstadoCliente);
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
