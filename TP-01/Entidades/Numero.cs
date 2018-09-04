using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Numero
    {
        private double numero;

        #region Propiedades
        public string SetNumero {
            set
            {
                numero = ValidarNumero(value);
            }
        }
        #endregion

        #region Constructores
        public Numero()
        {
            this.numero = 0;
        }

        public Numero(double numero) : this()
        {
            this.numero = numero;
        }

        public Numero(string numero) : this()
        {
            this.SetNumero = numero;
        }
        #endregion

        #region Métodos
        private double ValidarNumero(string strNumero)
        {
            double retorno;

            if(!(double.TryParse(strNumero, out retorno)))
            {
                retorno = 0;
            }

            return retorno;
        }

        public string BinarioDecimal(string binario)
        {
            string retorno;
            char caracterBinario;
            double auxBinario;
            int i;
            double suma = 0;
            string numeroBinarioReverse = Numero.Reverse(binario);

            if (ValidarBinario(binario))
            {
                for (i = binario.Length - 1; i >= 0; i--)
                {
                    auxBinario = Math.Pow(2, i);

                    caracterBinario = numeroBinarioReverse[i];

                    if (caracterBinario == '1')
                    {
                        suma = auxBinario + suma;
                    }
                }
                retorno = Convert.ToString(suma);
            }
            else
            {
                retorno = "Valor inválido";
            }
            return retorno;

        }

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private bool ValidarBinario(string Binario)
        {
            var charBinario = Binario.ToCharArray();
            int i = 0;
            bool retorno = false;

            for (; i < charBinario.Length; i++)
            {
                if (charBinario[i] == '1' || charBinario[i] == '0')
                {
                    retorno = true;
                }
                else
                {
                    retorno = false;
                    break;
                }
            }
            return retorno;
        }

        public string DecimalBinario(double Numero)
        {
            string binario = "";
            while (Numero > 1)
            {

                if (Numero % 2 == 0)
                {
                    binario = binario + "0";
                }
                else
                {
                    binario = binario + "1";

                }
                Numero = (int)Numero / 2;
            }
            binario += "1";

            return Reverse(binario);
        }

        public string DecimalBinario(string Numero)
        {
            double numeroDecimal;
            string retorno;

            if (double.TryParse(Numero, out numeroDecimal))
            {
                retorno = DecimalBinario(numeroDecimal);
            }
            else
            {
                retorno = "Valor inválido";
            }
            return retorno;
        }
        #endregion

        #region Sobrecargas
        public static double operator -(Numero numero1, Numero numero2)
        {
            return numero1.numero - numero2.numero;
        }

        public static double operator *(Numero numero1, Numero numero2)
        {
            return numero1.numero * numero2.numero;
        }

        public static double operator /(Numero numero1, Numero numero2)
        {
            return numero1.numero / numero2.numero;
        }

        public static double operator +(Numero numero1, Numero numero2)
        {
            return numero1.numero + numero2.numero;
        }
        #endregion
    }
}
