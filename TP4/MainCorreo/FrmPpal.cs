using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Excepciones;

namespace MainCorreo
{
    public partial class Form1 : Form
    {
        Correo correo = new Correo();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Crea un nuevo paquete y asocia al evento InformaEstado con el método paq_InformaEstado.
        /// Agrega el paquete al correo y llama al método ActualizarDatos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Paquete p = new Paquete(this.txtDireccion.Text, this.mtxtTrackingID.Text);
            p.EnvetoInformar += this.paq_InformaEstado;
            p.EventoException += this.paq_InformaExcepcion;

            try
            {                
                correo += p;
            }
            catch(TrackingIdRepetidoException ex)
            {
                MessageBox.Show("El número de tracking ingresado ya se encuentra en el sistema", "Información!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.ActualizarEstados();
            }
        }

        /// <summary>
        /// Limpia los tres ListBox y lugo recorre la lista de paquetes agregando cada uno de ellos
        /// a la lista que corresponda
        /// </summary>
        private void ActualizarEstados()
        {
            this.lstEstadoIngresado.Items.Clear();
            this.lstEstadoEnViaje.Items.Clear();
            this.lstEstadoEntregado.Items.Clear();

            foreach(Paquete p in correo.Paquetes)
            {
                switch(p.Estado)
                {
                    case Paquete.EEstado.Ingresado:
                        this.lstEstadoIngresado.Items.Add(p);
                        break;
                    case Paquete.EEstado.EnViaje:
                        this.lstEstadoEnViaje.Items.Add(p);
                        break;
                    case Paquete.EEstado.Entregado:
                        this.lstEstadoEntregado.Items.Add(p);
                        break;
                }
            }
        }

        /// <summary>
        /// Actualiza el estado del paquete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void paq_InformaEstado(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                Paquete.DelegadoEstado d = new Paquete.DelegadoEstado(paq_InformaEstado);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                this.ActualizarEstados();
            }
        }

        private void  paq_InformaExcepcion(Exception e)
        {
            MessageBox.Show(e.Message, "Importante!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        /// <summary>
        /// Cierra el formulario y además cierra todos los hilos abiertos en el proyecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            correo.FinEntregas();
        }

        /// <summary>
        /// Muestra la informacion de todos los elementos en el rtbMostrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<List<Paquete>>((IMostrar<List<Paquete>>)correo);
        }

        /// <summary>
        /// Muestra el menu Mostrar al seleccionar un item de lstEstadoEntregado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<Paquete>((IMostrar<Paquete>)lstEstadoEntregado.SelectedItem);
        }

        /// <summary>
        /// Evalua que el atributo elemento no sea nulo y muestra los datos de éste en el rtbMostrar.
        /// Después guarda los datos en un archivo de texto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elemento"></param>
        private void MostrarInformacion<T>(IMostrar<T> elemento)
        {
            this.rtbMostrar.Clear();
            if(!(Object.ReferenceEquals(elemento, null)))
            {
                this.rtbMostrar.Text = elemento.MostrarDatos(elemento).ToString();
                try
                {
                    GuardaString.Guardar(this.rtbMostrar.Text, "salida.txt");
                }
                catch(Exception)
                {
                    MessageBox.Show("No se pudo guradar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
