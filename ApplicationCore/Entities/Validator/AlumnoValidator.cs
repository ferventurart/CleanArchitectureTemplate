using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Validator
{
    public class AlumnoValidator : AbstractValidator<Alumno>
    {
        public AlumnoValidator()
        {
            RuleFor(x => x.Id).NotNull();

            RuleFor(x => x.Codigo).NotNull().WithMessage("Codigo es requerido")
            .Length(12).WithMessage("El Codigo debe tener 12 caracteres");

            RuleFor(x => x.Nombre).NotNull().WithMessage("Nombre es requerido")
           .Length(3, 50).WithMessage("El Nombre debe contener entre 3 y 50 caracteres");

            RuleFor(x => x.Apellido).NotNull().WithMessage("Apellido es requerido")
           .Length(3, 50).WithMessage("El Apellido debe contener entre 3 y 50 caracteres");

            RuleFor(x => x.Genero).IsInEnum().WithMessage("Ingrese un Genero valido");

            RuleFor(x => x.FechaNacimiento).NotNull().WithMessage("Fecha Nacimiento es requerida");
        }
    }
}
