using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntidadesInstanciables;
using EntidadesAbstractas;
using Excepciones;

namespace TestUniversidad
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DniInvalidoExceptionCargarDniIntInvalido()
        {
            Universidad gim = new Universidad();
            try
            {
                Alumno a2 = new Alumno(2, "Juana", "Martinez", "92234458",
                EntidadesAbstractas.Persona.ENacionalidad.Argentino, Universidad.EClases.SPD,
                Alumno.EEstadoCuenta.AlDia);
                gim += a2;
            }
            catch (NacionalidadInvalidaException e)
            {
                Assert.IsInstanceOfType(e, typeof(NacionalidadInvalidaException));
            }
        }

        [TestMethod]
        public void SinProfesorExceptionCargarSinProfesor()
        {
            Universidad gim = new Universidad();
            try
            {
                gim += Universidad.EClases.SPD;
            }
            catch (SinProfesorException e)
            {
                Assert.IsInstanceOfType(e, typeof(SinProfesorException));
            }
        }

        [TestMethod]
        public void ValidarValorNumerico()
        {
            Profesor p = new Profesor(1, "Julia", "Zuluaga", "98988733", Persona.ENacionalidad.Extranjero);
            Assert.IsInstanceOfType(p.DNI, typeof(int));
        }

        [TestMethod]
        public void ValidarLetrasEnNombres()
        {
            Alumno a = new Alumno(4, "Çesar", "Galvan", "84756123", Persona.ENacionalidad.Argentino,
                Universidad.EClases.Legislacion);
            Assert.IsNotNull(a.Nombre);
        }
    }
}
