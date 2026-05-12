import { Component, input, output } from '@angular/core';
import { UsuarioPlataforma } from '@domain/usuarios-plataforma/models/usuario-plataforma.models';

@Component({
  selector: 'app-usuarios-plataforma-tabla',
  standalone: true,
  template: `
    <div class="tabla-container">
      <table>
        <thead>
          <tr>
            <th>Nombres</th>
            <th>Apellidos</th>
            <th>Correo</th>
            <th>Estado</th>
            <th>Último Acceso</th>
            <th>Bloqueado Hasta</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          @for (item of registros(); track item.id) {
            <tr>
              <td>{{ item.nombres }}</td>
              <td>{{ item.apellidos }}</td>
              <td>{{ item.correo }}</td>
              <td>{{ item.estado ? 'Activo' : 'Inactivo' }}</td>
              <td>{{ item.ultimoAcceso ?? '—' }}</td>
              <td>{{ item.bloqueadoHasta ?? '—' }}</td>
              <td>
                <button type="button" (click)="editar.emit(item)">Editar</button>
                <button type="button" (click)="eliminar.emit(item)">Eliminar</button>
              </td>
            </tr>
          } @empty {
            <tr><td colspan="7">Sin registros.</td></tr>
          }
        </tbody>
      </table>
    </div>
  `
})
export class UsuariosPlataformaTablaComponent {
  readonly registros = input<UsuarioPlataforma[]>([]);
  readonly editar = output<UsuarioPlataforma>();
  readonly eliminar = output<UsuarioPlataforma>();
}
