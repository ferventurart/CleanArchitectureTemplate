using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class AlumnoConfiguration : IEntityTypeConfiguration<Alumno>
    {
        public void Configure(EntityTypeBuilder<Alumno> builder)
        {
            builder.ToTable("Alumnos");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Codigo)
                   .HasMaxLength(12)
                   .IsRequired();

            builder.Property(ci => ci.Nombre)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(ci => ci.Apellido)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(ci => ci.Genero)
                    .IsRequired();

            builder.Property(ci => ci.FechaNacimiento)
                    .IsRequired();

            builder.Property(ci => ci.Fotografia);
        }
    }
}
