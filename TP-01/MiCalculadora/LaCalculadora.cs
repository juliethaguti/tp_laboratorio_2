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

namespace MiCalculadora
{
    public partial class LaCalculadora : Form
    {
        public LaCalculadora()
        {
            InitializeComponent();
            this.Limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConvertirABinario_Click(object sender, EventArgs e)
        {
            Numero numero = new Numero();
            lblResultado.Text = numero.DecimalBinario(lblResultado.Text.ToString());
        }

        private void btnConvertirADecimal_Click(object sender, EventArgs e)
        {
            Numero numero = new Numero();
            lblResultado.Text = numero.BinarioDecimal(lblResultado.Text);
        }

        private void Limpiar()
        {
            txtNumero2.Text = "";
            cmbOperador.Text = "";
            txtNumero1.Text = "";
            lblResultado.Text = "0";
            btnConvertirABinario.Enabled = false;
            btnConvertirADecimal.Enabled = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
        }

        public static double Operar(Numero numero1, Numero numero2, string operador)
        {
            double retorno;
            Calculadora calculadora = new Calculadora();
            retorno = calculadora.Operar(numero1, numero2, operador);
            return retorno;

        }

        private void btnOperar_Click(object sender, EventArgs e)
        {
            Numero numero1 = new Numero(txtNumero1.Text);
            Numero numero2 = new Numero(txtNumero2.Text);

            string operador = cmbOperador.Text;
            lblResultado.Text = Operar(numero1, numero2, operador).ToString();
            btnConvertirABinario.Enabled = true;
            btnConvertirADecimal.Enabled = true;
        }
    }
}
