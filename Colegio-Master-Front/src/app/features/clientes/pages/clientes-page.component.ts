import { Component, inject, OnInit, signal } from '@angular/core';
import { ClientesStore } from '@application/clientes/clientes.store';
import { Cliente, CrearCliente, ActualizarCliente } from '@domain/clientes/models/cliente.models';
import { ClientesTablaComponent } from '../components/clientes-tabla.component';
import { ClientesFormularioComponent } from '../components/clientes-formulario.component';

@Component({
  selector: 'app-clientes-page',
  standalone: true,
  imports: [ClientesTablaComponent, ClientesFormularioComponent],
  template: `
    <section>
      <div class="page-header">
        <h2>Clientes</h2>
        @if (!mostrarFormulario()) {
          <button type="button" (click)="onNuevo()">+ Nuevo</button>
        }
      </div>

      @if (store.cargando()) { <p>Cargando...</p> }
      @if (store.error()) { <p class="error">{{ store.error() }}</p> }
      @if (store.mensaje()) { <p class="exito">{{ store.mensaje() }}</p> }

      @if (!mostrarFormulario()) {
        <app-clientes-tabla
          [registros]="store.registros()"
          (editar)="onEditar($event)"
          (eliminar)="onEliminar($event)"
        />
      } @else {
        <app-clientes-formulario
          [registro]="store.seleccionado()"
          (guardar)="onGuardar($event)"
          (cancelar)="onCancelar()"
        />
      }
    </section>
  `
})
export class ClientesPageComponent implements OnInit {
  protected readonly store = inject(ClientesStore);
  protected readonly mostrarFormulario = signal(false);

  async ngOnInit(): Promise<void> {
    await this.store.cargarListadoInicial();
  }

  onNuevo(): void {
    this.store.seleccionado.set(null);
    this.mostrarFormulario.set(true);
  }

  onEditar(item: Cliente): void {
    this.store.seleccionado.set(item);
    this.mostrarFormulario.set(true);
  }

  async onEliminar(item: Cliente): Promise<void> {
    if (!confirm(`¿Eliminar el cliente "${item.razonSocial}"?`)) return;
    await this.store.eliminar(item.id);
  }

  async onGuardar(payload: CrearCliente | ActualizarCliente): Promise<void> {
    const seleccionado = this.store.seleccionado();
    if (seleccionado) {
      await this.store.actualizar(seleccionado.id, payload as ActualizarCliente);
    } else {
      await this.store.crear(payload as CrearCliente);
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
