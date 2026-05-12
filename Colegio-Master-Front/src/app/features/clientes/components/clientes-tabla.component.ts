import { Component, input, output } from '@angular/core';
import { Cliente } from '@domain/clientes/models/cliente.models';

@Component({
  selector: 'app-clientes-tabla',
  standalone: true,
  template: `
    <div class="tabla-container">
      <table>
        <thead>
          <tr>
            <th>Código</th>
            <th>RUC</th>
            <th>Razón Social</th>
            <th>Nombre Comercial</th>
            <th>Correo</th>
            <th>Teléfono</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          @for (item of registros(); track item.id) {
            <tr>
              <td>{{ item.codigo }}</td>
              <td>{{ item.ruc }}</td>
              <td>{{ item.razonSocial }}</td>
              <td>{{ item.nombreComercial }}</td>
              <td>{{ item.correoContacto ?? '—' }}</td>
              <td>{{ item.telefono ?? '—' }}</td>
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
export class ClientesTablaComponent {
  readonly registros = input<Cliente[]>([]);
  readonly editar = output<Cliente>();
  readonly eliminar = output<Cliente>();
}
