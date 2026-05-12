import { Component, input, output } from '@angular/core';
import { EstadoCliente } from '@domain/estado-clientes/models/estado-cliente.models';

@Component({
  selector: 'app-estado-clientes-tabla',
  standalone: true,
  template: `
    <div class="tabla-container">
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Código</th>
            <th>Descripción</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          @for (item of registros(); track item.id) {
            <tr>
              <td>{{ item.id }}</td>
              <td>{{ item.codigo }}</td>
              <td>{{ item.descripcion }}</td>
              <td>
                <button type="button" (click)="editar.emit(item)">Editar</button>
                <button type="button" (click)="eliminar.emit(item)">Eliminar</button>
              </td>
            </tr>
          } @empty {
            <tr><td colspan="4">Sin registros.</td></tr>
          }
        </tbody>
      </table>
    </div>
  `
})
export class EstadoClientesTablaComponent {
  readonly registros = input<EstadoCliente[]>([]);
  readonly editar = output<EstadoCliente>();
  readonly eliminar = output<EstadoCliente>();
}
