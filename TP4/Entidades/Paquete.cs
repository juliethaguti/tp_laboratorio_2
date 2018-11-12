using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesDAO;

namespace Entidades
{
    public class Paquete : IMostrar<Paquete>
    {
        private string direccionEntrega;
        private EEstado estado;
        private string trackingID;
        public delegate void DelegadoEstado(object sender, EventArgs e);
        public event DelegadoEstado EnvetoInformar;
        public delegate void DelegadoException(Exception e);
        public event DelegadoException EventoException;


        #region Enumeracion
        public enum EEstado
        {
            Ingresado, EnViaje, Entregado
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Retorna y estalece la direccion de Entrega.
        /// </summary>
        public string DireccionEntrega
        {
            get
            {
                return this.direccionEntrega;
            }
            set
            {
                this.direccionEntrega = value;
            }
        }

        /// <summary>
        /// Retorna y establece el estado del envio del paquete
        /// </summary>
        public EEstado Estado
        {
            get
            {
                return this.estado;
            }
            set
            {
                this.estado = value;
            }
        }

        /// <summary>
        /// Retorna y establece el numero del envio
        /// </summary>
        public string TrackingID
        {
            get
            {
                return this.trackingID;
            }
            set
            {
                this.trackingID = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de instancia
        /// </summary>
        /// <param name="direccionEntrega">Direccion donde se entregará el paquete</param>
        /// <param name="trackingID">Número de envío del paquete</param>
        public Paquete(string direccionEntrega, string trackingID)
        {
            this.TrackingID = trackingID;
            this.DireccionEntrega = direccionEntrega;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Cambia de estado hasta ser entregado, informa ese cambio por InformaEstado.
        /// Guarda los datos del paquete en una base de datos
        /// </summary>
        public void MockCicloDeVida()
        {
            do
            {
                System.Threading.Thread.Sleep(10000);
                this.Estado += 1;
                this.EnvetoInformar(this.Estado, EventArgs.Empty);
            } while (this.Estado != EEstado.Entregado);

            try
            {
                PaqueteDAO.Insertar(this);
            }
            catch(Exception ex)
            {
                EventoException.Invoke(ex);
            }
        }

        /// <summary>
        /// Compila la informacion del paquete
        /// </summary>
        /// <param name="elemento">paquete</param>
        /// <returns>La información del paquete</returns>
        public string MostrarDatos(IMostrar<Paquete> elemento)
        {
            Paquete p = (Paquete)elemento;

            return string.Format("{0} para {1}", p.TrackingID, p.DireccionEntrega);
        }

        /// <summary>
        /// Retorna la informacion del paquete.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.MostrarDatos(this);
        }
        #endregion

        #region Sobrecargas de operadores
        /// <summary>
        /// Compara dos paquetes comparando su número trackingID.
        /// </summary>
        /// <param name="p1">Paquete a comparar</param>
        /// <param name="p2">Paquete a comparar</param>
        /// <returns>Retorna true si los paquetes son iguales, de lo contrario false</returns>
        public static bool operator !=(Paquete p1,Paquete p2)
        {
            return !(p1 == p2);
        }

        /// <summary>
        /// Dos paquetes serán iguales siempre y cuando su Tracking ID sea el mismo.
        /// </summary>
        /// <param name="p1">Paquete a comparar</param>
        /// <param name="p2">Paquete a comparar</param>
        /// <returns>Retorna true si los paquetes son iguales, de lo contrario false</returns>
        public static bool operator == (Paquete p1, Paquete p2)
        {
            bool retorno = false;

            if(p1.TrackingID == p2.TrackingID)
            {
                retorno = true;
            }

            return retorno;
        }
        #endregion
    }
}
