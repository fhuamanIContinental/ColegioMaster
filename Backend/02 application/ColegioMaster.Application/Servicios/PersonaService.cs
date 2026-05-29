using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using ColegioMaster.Infrastructure.Models;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColegioMaster.Application.Servicios
{
    public class PersonaService : CrudServicioBase<Persona, int, PersonaDto, CrearPersonaDto, ActualizarPersonaDto>, IPersonaService
    {

        protected override string NombreEntidad => "Persona";
        public PersonaService(IRepositorioCrud<Persona> repositorio)
        : base(repositorio)
        {

        }
        protected override List<string> ValidarCreacion(CrearPersonaDto request)
        {
            var errores = new List<string>();
            ValidarRequerido(errores, request.Materno, "Materno", 50);
            ValidarRequerido(errores, request.Paterno, "Paterno", 50);
            ValidarRequerido(errores, request.Nombres, "Nombres", 50);


            return errores;
        }

        protected override List<string> ValidarActualizacion(ActualizarPersonaDto request)
        {
            var errores = new List<string>();
            ValidarRequerido(errores, request.Materno, "Materno", 50);
            ValidarRequerido(errores, request.Paterno, "Paterno", 50);
            ValidarRequerido(errores, request.Nombres, "Nombres", 50);

            return errores;
        }

        protected override Persona MapearEntidadCreacion(CrearPersonaDto request)
        {

            Persona newRegistro = new Persona()
            {
                ApellidoMaterno = request.Materno,
                ApellidoPaterno = request.Paterno,
                Nombres = request.Nombres,
                TipoDocumento = request.TipoDocumento,
                NumeroDocumento = request.NumeroDocumento,
                FechaCreacion = DateTime.Now,
                UsuarioCreacion = request.UsuarioCreacion
            };

            return newRegistro;
        }

        protected override void MapearEntidadActualizacion(Persona entidad, ActualizarPersonaDto request)
        {
            Persona registroActualiza = new Persona() { 
             ApellidoMaterno = request.Materno,
                ApellidoPaterno = request.Paterno,
                Nombres = request.Nombres,
                TipoDocumento = request.TipoDocumento,
                NumeroDocumento = request.NumeroDocumento,
                FechaModificacion = DateTime.Now,
                UsuarioModificacion = request.UsuarioModificacion
            };
        }

        protected override PersonaDto MapearDto(Persona entidad)
        {
            PersonaDto dto = new PersonaDto()
            {
                Id = entidad.Id,
                Materno = entidad.ApellidoMaterno,
                Paterno = entidad.ApellidoPaterno,
                Nombres = entidad.Nombres,
                TipoDocumento = entidad.TipoDocumento,
                NumeroDocumento = entidad.NumeroDocumento,
                FechaCreacion = entidad.FechaCreacion,
                FechaModificacion = entidad.FechaModificacion,
                UsuarioCreacion = entidad.UsuarioCreacion,
                UsuarioModificacion = entidad.UsuarioModificacion
            };
            return dto;
        }
    }
}
