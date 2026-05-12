import { Component, inject, OnInit, signal } from '@angular/core';
import { PlanesStore } from '@application/planes/planes.store';
import { Plan, CrearPlan, ActualizarPlan } from '@domain/planes/models/plan.models';
import { PlanesTablaComponent } from '../components/planes-tabla.component';
import { PlanesFormularioComponent } from '../components/planes-formulario.component';

@Component({
  selector: 'app-planes-page',
  standalone: true,
  imports: [PlanesTablaComponent, PlanesFormularioComponent],
  template: `
    <section>
      <div class="page-header">
        <h2>Planes</h2>
        @if (!mostrarFormulario()) {
          <button type="button" (click)="onNuevo()">+ Nuevo</button>
        }
      </div>

      @if (store.cargando()) { <p>Cargando...</p> }
      @if (store.error()) { <p class="error">{{ store.error() }}</p> }
      @if (store.mensaje()) { <p class="exito">{{ store.mensaje() }}</p> }

      @if (!mostrarFormulario()) {
        <app-planes-tabla
          [registros]="store.registros()"
          (editar)="onEditar($event)"
          (eliminar)="onEliminar($event)"
        />
      } @else {
        <app-planes-formulario
          [registro]="store.seleccionado()"
          (guardar)="onGuardar($event)"
          (cancelar)="onCancelar()"
        />
      }
    </section>
  `
})
export class PlanesPageComponent implements OnInit {
  protected readonly store = inject(PlanesStore);
  protected readonly mostrarFormulario = signal(false);

  async ngOnInit(): Promise<void> {
    await this.store.cargarListadoInicial();
  }

  onNuevo(): void {
    this.store.seleccionado.set(null);
    this.mostrarFormulario.set(true);
  }

  onEditar(item: Plan): void {
    this.store.seleccionado.set(item);
    this.mostrarFormulario.set(true);
  }

  async onEliminar(item: Plan): Promise<void> {
    if (!confirm(`¿Eliminar el plan "${item.nombre}"?`)) return;
    await this.store.eliminar(item.id);
  }

  async onGuardar(payload: CrearPlan | ActualizarPlan): Promise<void> {
    const seleccionado = this.store.seleccionado();
    if (seleccionado) {
      await this.store.actualizar(seleccionado.id, payload as ActualizarPlan);
    } else {
      await this.store.crear(payload as CrearPlan);
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
