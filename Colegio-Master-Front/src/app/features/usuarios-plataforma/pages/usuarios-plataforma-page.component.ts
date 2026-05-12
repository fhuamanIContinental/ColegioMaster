import { Component, inject, OnInit, signal } from '@angular/core';
import { UsuariosPlataformaStore } from '@application/usuarios-plataforma/usuarios-plataforma.store';
import { UsuarioPlataforma, CrearUsuarioPlataforma, ActualizarUsuarioPlataforma } from '@domain/usuarios-plataforma/models/usuario-plataforma.models';
import { UsuariosPlataformaTablaComponent } from '../components/usuarios-plataforma-tabla.component';
import { UsuariosPlataformaFormularioComponent } from '../components/usuarios-plataforma-formulario.component';

@Component({
  selector: 'app-usuarios-plataforma-page',
  standalone: true,
  imports: [UsuariosPlataformaTablaComponent, UsuariosPlataformaFormularioComponent],
  template: `
    <section>
      <div class="page-header">
        <h2>Usuarios de Plataforma</h2>
        @if (!mostrarFormulario()) {
          <button type="button" (click)="onNuevo()">+ Nuevo</button>
        }
      </div>

      @if (store.cargando()) { <p>Cargando...</p> }
      @if (store.error()) { <p class="error">{{ store.error() }}</p> }
      @if (store.mensaje()) { <p class="exito">{{ store.mensaje() }}</p> }

      @if (!mostrarFormulario()) {
        <app-usuarios-plataforma-tabla
          [registros]="store.registros()"
          (editar)="onEditar($event)"
          (eliminar)="onEliminar($event)"
        />
      } @else {
        <app-usuarios-plataforma-formulario
          [registro]="store.seleccionado()"
          (guardar)="onGuardar($event)"
          (cancelar)="onCancelar()"
        />
      }
    </section>
  `
})
export class UsuariosPlataformaPageComponent implements OnInit {
  protected readonly store = inject(UsuariosPlataformaStore);
  protected readonly mostrarFormulario = signal(false);

  async ngOnInit(): Promise<void> {
    await this.store.cargarListadoInicial();
  }

  onNuevo(): void {
    this.store.seleccionado.set(null);
    this.mostrarFormulario.set(true);
  }

  onEditar(item: UsuarioPlataforma): void {
    this.store.seleccionado.set(item);
    this.mostrarFormulario.set(true);
  }

  async onEliminar(item: UsuarioPlataforma): Promise<void> {
    if (!confirm(`¿Eliminar el usuario "${item.nombres} ${item.apellidos}"?`)) return;
    await this.store.eliminar(item.id);
  }

  async onGuardar(payload: CrearUsuarioPlataforma | ActualizarUsuarioPlataforma): Promise<void> {
    const seleccionado = this.store.seleccionado();
    if (seleccionado) {
      await this.store.actualizar(seleccionado.id, payload as ActualizarUsuarioPlataforma);
    } else {
      await this.store.crear(payload as CrearUsuarioPlataforma);
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
