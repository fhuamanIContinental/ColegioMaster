import { DecimalPipe } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { Plan } from '@domain/planes/models/plan.models';

@Component({
  selector: 'app-planes-tabla',
  standalone: true,
  imports: [DecimalPipe],
  template: `
    <div class="tabla-container">
      <table>
        <thead>
          <tr>
            <th>Código</th>
            <th>Nombre</th>
            <th>Precio Mensual</th>
            <th>Precio Anual</th>
            <th>Máx. Estudiantes</th>
            <th>Máx. Usuarios</th>
            <th>Estado</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          @for (item of registros(); track item.id) {
            <tr>
              <td>{{ item.codigo }}</td>
              <td>{{ item.nombre }}</td>
              <td>{{ item.precioMensual | number:'1.2-2' }}</td>
              <td>{{ item.precioAnual | number:'1.2-2' }}</td>
              <td>{{ item.maxEstudiante ?? 'Ilimitado' }}</td>
              <td>{{ item.maxUsuario ?? 'Ilimitado' }}</td>
              <td>{{ item.estado ? 'Activo' : 'Inactivo' }}</td>
              <td>
                <button type="button" (click)="editar.emit(item)">Editar</button>
                <button type="button" (click)="eliminar.emit(item)">Eliminar</button>
              </td>
            </tr>
          } @empty {
            <tr><td colspan="8">Sin registros.</td></tr>
          }
        </tbody>
      </table>
    </div>
  `
})
export class PlanesTablaComponent {
  readonly registros = input<Plan[]>([]);
  readonly editar = output<Plan>();
  readonly eliminar = output<Plan>();
}
