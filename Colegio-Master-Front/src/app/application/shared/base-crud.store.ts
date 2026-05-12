import { computed, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import {
  ConsultaPaginada,
  RespuestaGeneral,
  ResultadoPaginado,
  consultaInicial
} from '@domain/shared/models/api.models';
import { CrudRepositoryPort } from '@domain/shared/ports/crud-repository.port';

export abstract class BaseCrudStore<TId, TDto, TCrearDto, TActualizarDto> {
  protected readonly queryInicial = consultaInicial();

  readonly cargando = signal(false);
  readonly error = signal<string | null>(null);
  readonly mensaje = signal<string | null>(null);
  readonly seleccionado = signal<TDto | null>(null);
  readonly resultadoPaginado = signal<ResultadoPaginado<TDto> | null>(null);
  readonly filtrosActivos = signal<ConsultaPaginada>(this.queryInicial);
  readonly registros = computed(() => this.resultadoPaginado()?.registros ?? []);

  protected constructor(protected readonly repositorio: CrudRepositoryPort<TId, TDto, TCrearDto, TActualizarDto>) {}

  async cargarListadoInicial(): Promise<void> {
    await this.listar(this.queryInicial.numeroPagina, this.queryInicial.tamanioPagina);
  }

  async listar(numeroPagina = 1, tamanioPagina = 20): Promise<void> {
    this.filtrosActivos.set({ numeroPagina, tamanioPagina, filters: [] });
    await this.ejecutar(async () => {
      const respuesta = await firstValueFrom(this.repositorio.listar(numeroPagina, tamanioPagina));
      this.asignarResultadoPaginado(respuesta);
    });
  }

  async buscar(consulta: ConsultaPaginada): Promise<void> {
    this.filtrosActivos.set(consulta);
    await this.ejecutar(async () => {
      const respuesta = await firstValueFrom(this.repositorio.buscar(consulta));
      this.asignarResultadoPaginado(respuesta);
    });
  }

  async obtenerPorId(id: TId): Promise<void> {
    await this.ejecutar(async () => {
      const respuesta = await firstValueFrom(this.repositorio.obtenerPorId(id));
      this.asignarEntidad(respuesta);
    });
  }

  async crear(payload: TCrearDto): Promise<void> {
    await this.ejecutar(async () => {
      const respuesta = await firstValueFrom(this.repositorio.crear(payload));
      this.asignarEntidad(respuesta);
      await this.buscar(this.filtrosActivos());
    });
  }

  async actualizar(id: TId, payload: TActualizarDto): Promise<void> {
    await this.ejecutar(async () => {
      const respuesta = await firstValueFrom(this.repositorio.actualizar(id, payload));
      this.asignarEntidad(respuesta);
      await this.buscar(this.filtrosActivos());
    });
  }

  async eliminar(id: TId): Promise<void> {
    await this.ejecutar(async () => {
      const respuesta = await firstValueFrom(this.repositorio.eliminar(id));

      if (!respuesta.success) {
        throw new Error(respuesta.textMessage || 'No se pudo eliminar el registro.');
      }

      this.mensaje.set(respuesta.textMessage || respuesta.titleMessage);
      this.seleccionado.set(null);
      await this.buscar(this.filtrosActivos());
    });
  }

  private async ejecutar(accion: () => Promise<void>): Promise<void> {
    this.cargando.set(true);
    this.error.set(null);

    try {
      await accion();
    } catch (error) {
      this.error.set(error instanceof Error ? error.message : 'Error no controlado.');
    } finally {
      this.cargando.set(false);
    }
  }

  private asignarResultadoPaginado(respuesta: RespuestaGeneral<ResultadoPaginado<TDto>>): void {
    if (!respuesta.success || !respuesta.content) {
      throw new Error(respuesta.textMessage || 'No se pudo obtener el listado.');
    }

    this.resultadoPaginado.set(respuesta.content);
    this.mensaje.set(respuesta.textMessage || respuesta.titleMessage);
  }

  private asignarEntidad(respuesta: RespuestaGeneral<TDto>): void {
    if (!respuesta.success || !respuesta.content) {
      throw new Error(respuesta.textMessage || 'No se pudo procesar la entidad.');
    }

    this.seleccionado.set(respuesta.content);
    this.mensaje.set(respuesta.textMessage || respuesta.titleMessage);
  }
}
