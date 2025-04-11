
using Xunit;
using ServicioGestionEstudiantes.Negocio;
using Moq;
using ServicioGestionEstudiantes.Datos;
using ServicioGestionEstudiantes.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicioGestionEstudiantes.Negocio.DTOS;


namespace ServicioGestionEstudiantes
{
    public class EstudianteServiceTests
    {


        [Fact]
        public async Task GetEstudiantesUnitTest()
        {
            // Arrange: configurar la base de datos en memoria
            var options = new DbContextOptionsBuilder<Datos.DBContext>()
                .UseInMemoryDatabase(databaseName: "EstudiantesTestDb")
                .Options;

            using var context = new Datos.DBContext(options);
            context.Estudiantes.AddRange(
            new Estudiante
            {
                IdEstudiante = "123",
                NombresEstudiante = "Juan",
                ApellidosEstudiante = "Pérez",
                Contrasena = "clave123",
                Email = "juan@example.com"
            },
            new Estudiante
            {
                IdEstudiante = "456",
                NombresEstudiante = "María",
                ApellidosEstudiante = "Gómez",
                Contrasena = "clave456",
                Email = "maria@example.com"
            }
);
            await context.SaveChangesAsync();

            Mock<IMapper> mapperMock = new Mock<IMapper>();

            var service = new EstudianteService(context, mapperMock.Object);

            // Act
            var resultado = await service.GetEstudiantes();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task CrearEstudianteUnitTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Datos.DBContext>()
                .UseInMemoryDatabase(databaseName: "CrearEstudianteTestDb")
                .Options;

            using var context = new Datos.DBContext(options);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Estudiante>(It.IsAny<EstudianteDto>()))
                      .Returns((EstudianteDto dto) => new Estudiante
                      {
                          IdEstudiante = dto.IdEstudiante,
                          NombresEstudiante = dto.NombresEstudiante,
                          ApellidosEstudiante = dto.ApellidosEstudiante,
                          Email = dto.Email,
                          Contrasena = dto.Contrasena
                      });

            var service = new EstudianteService(context, mapperMock.Object);

            var nuevoEstudiante = new EstudianteDto
            {
                IdEstudiante = "789",
                NombresEstudiante = "Luis",
                ApellidosEstudiante = "Ramírez",
                Email = "luis@example.com",
                Contrasena = "clave789"
            };

            // Act
            await service.CreateEstudiante(nuevoEstudiante);

            // Assert
            var estudianteEnDb = await context.Estudiantes.FindAsync("789");
            Assert.NotNull(estudianteEnDb);
            Assert.Equal("Luis", estudianteEnDb.NombresEstudiante);
        }
    }
}