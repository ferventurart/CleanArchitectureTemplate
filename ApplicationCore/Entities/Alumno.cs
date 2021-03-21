using ApplicationCore.Enum;
using System;

namespace ApplicationCore.Entities
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Genero Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public string NombreCompleto()
        {
            return Nombre + ' ' + Apellido;
        }
    }
}
