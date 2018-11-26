using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Excepciones;

namespace Entidades
{
    public class Correo : IMostrar<List<Paquete>>
    {
        private List<Thread> mockPaquetes;
        private List<Paquete> paquetes;

        #region Propiedades
        /// <summary>
        /// Retorna y establece la lista de paquetes.
        /// </summary>
        public List<Paquete> Paquetes
        {
            get
            {
                return this.paquetes;
            }
            set
            {
                this.paquetes = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de Clase.
        /// </summary>
        public Correo()
        {
            this.mockPaquetes = new List<Thread>();
            this.paquetes = new List<Paquete>();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Cierra todos los hilos activos.
        /// </summary>
        public void FinEntregas()
        {
            foreach(Thread hilo in this.mockPaquetes)
            {
                if(hilo.IsAlive)
                {
                    hilo.Abort();
                }
            }
        }

        /// <summary>
        /// Retorna los datos de la lista de paquetes de ese correo
        /// </summary>
        /// <param name="elementos"></param>
        /// <returns></returns>
        public string MostrarDatos(IMostrar<List<Paquete>> elementos)
        {
            List<Paquete> l = (List<Paquete>)((Correo)elementos).paquetes;
            StringBuilder sb = new StringBuilder();

            foreach(Paquete p in l)
            {
                sb.AppendFormat("{0} para {1} {2}\n", p.TrackingID, p.DireccionEntrega, p.Estado.ToString());
            }

            return sb.ToString();
        }
        #endregion

        #region Sobrecarga de Operador
        /// <summary>
        /// Agrega ese paquete en caso que no se encuentre en ese correo
        /// </summary>
        /// <param name="c">Correo donde se verifica la lista de paquetes</param>
        /// <param name="p">Paquete a agregar</param>
        /// <returns>Retorna un correo con el paquete agregado y lanza una excepcion en caso de que ya se encuentre</returns>
        public static Correo operator +(Correo c, Paquete p)
        {
            foreach(Paquete paq in c.Paquetes)
            {
                if(p == paq)
                {
                    throw new TrackingIdRepetidoException("El paquete ya está en la lista");
                }
            }
            c.Paquetes.Add(p);

            Thread hilo = new Thread(p.MockCicloDeVida);
            c.mockPaquetes.Add(hilo);
            hilo.Start();

            return c;
        }
        #endregion
    }
}
