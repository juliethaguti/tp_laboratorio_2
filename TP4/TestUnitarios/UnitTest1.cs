using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entidades;
using Excepciones;

namespace TestUnitarios
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestListaDePaquetesInstanciada()
        {
            Correo c = new Correo();
            Assert.IsNotNull(c.Paquetes);
        }

        [TestMethod]
        public void TestCargaDePaquetesAUnSoloTracking()
        {
            Correo c = new Correo();
            Paquete p1 = new Paquete("Direccion", "0000000000");
            Paquete p2 = new Paquete("Direccion2", "0000000000");
            try
            {
                c += p1;
                c += p2;
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(TrackingIdRepetidoException));
            }
        }
    }
}
