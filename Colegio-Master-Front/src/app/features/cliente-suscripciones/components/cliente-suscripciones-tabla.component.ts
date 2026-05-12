import { DecimalPipe, SlicePipe } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { ClienteSuscripcion } from '@domain/cliente-suscripciones/models/cliente-suscripcion.models';

@Component({
  selector: 'app-cliente-suscripciones-tabla',
  standalone: true,
  imports: [DecimalPipe, SlicePipe],
  template: `
    <div class="tabla-container">
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>ID Cliente</th>
            <th>ID Plan</th>
            <th>Modalidad</th>
            <th>Monto Pactado</th>
            <th>Fecha Inicio</th>
            <th>Fecha Fin</th>
            <th>ID Estado</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          @for (item of registros(); track item.id) {
            <tr>
              <td>{{ item.id }}</td>
              <td>{{ item.idCliente }}</td>
              <td>{{ item.idPlan }}</td>
              <td>{{ item.modalidad }}</td>
              <td>{{ item.montoPactado | number:'1.2-2' }}</td>
              <td>{{ item.fechaInicio | slice:0:10 }}</td>
              <td>{{ item.fechaFin ? (item.fechaFin | slice:0:10) : '—' }}</td>
              <td>{{ item.idEstado }}</td>
              <td>
                <button type="button" (click)="editar.emit(item)">Editar</button>
                <button type="button" (click)="eliminar.emit(item)">Eliminar</button>
              </td>
            </tr>
          } @empty {
            <tr><td colspan="9">Sin registros.</td></tr>
          }
        </tbody>
      </table>
    </div>
  `
})
export class ClienteSuscripcionesTablaComponent {
  readonly registros = input<ClienteSuscripcion[]>([]);
  readonly editar = output<ClienteSuscripcion>();
  readonly eliminar = output<ClienteSuscripcion>();
}
