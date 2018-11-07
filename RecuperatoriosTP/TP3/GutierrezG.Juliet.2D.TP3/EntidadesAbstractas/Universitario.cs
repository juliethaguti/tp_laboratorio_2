using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesAbstractas
{
    public abstract class Universitario : Persona
    {
        private int legajo;

        #region Propiedades
        /// <summary>
        /// Otiene el numero de identificación del universitario
        /// </summary>
        public int Legajo
        {
            get
            {
                return this.legajo;
            }
        }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Universitario()
        {

        }

        /// <summary>
        /// Constructor de instancia, recibe el atributo de la clase
        /// </summary>
        /// <param name="legajo">Atributo de la clase, número de identificación del universitario</param>
        /// <param name="nombre">nombre del universitario</param>
        /// <param name="apellido">apellido del universitario</param>
        /// <param name="dni">dni del universitario</param>
        /// <param name="nacionalidad">nacionalidad del universitario</param>
        public Universitario(int legajo, string nombre, string apellido, string dni, Persona.ENacionalidad nacionalidad)
            : base(nombre, apellido, dni, nacionalidad)
        {
            this.legajo = legajo;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Determina si dos objeto tienen el mismo valor
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj.GetType() == this.GetType());
        }

        /// <summary>
        /// Concatena todos los datos del universitario
        /// </summary>
        /// <returns>Retorna los datos del universitario</returns>
        protected virtual string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendFormat("\nLEGAJO NÚMERO: {0}", this.legajo);
            //sb.AppendFormat("\nNACIONALIDAD: {0}", this.Nacionalidad);

            return sb.ToString();

        }

        /// <summary>
        /// Método abstracto
        /// </summary>
        /// <returns></returns>
        protected abstract string ParticiparEnClase();
        #endregion

        #region sobrecargas de Operadores
        /// <summary>
        /// Determina si dos universitarios no son del mismo tipo y su número de identificacion son diferentes.
        /// </summary>
        /// <param name="pg1">Universitario a comparar</param>
        /// <param name="pg2">Universitario a comparar</param>
        /// <returns>True si esos dos universitarios no son del mismo tipo, de lo contrario false</returns>
        public static bool operator !=(Universitario pg1, Universitario pg2)
        {
            return !(pg1 == pg2);
        }

        /// <summary>
        /// Determina si dos universitarios son del mismo tipo y su numero de identificacion son iguales.
        /// </summary>
        /// <param name="pg1">Universitario a comparar</param>
        /// <param name="pg2">Universitario a comparar</param>
        /// <returns>True si esos dos universitarios son del mismo tipo, de lo contrario false</returns>
        public static bool operator ==(Universitario pg1, Universitario pg2)
        {
            bool retorno = false;

            if (pg1.Equals(pg2) && pg1.DNI == pg2.DNI)
            {
                retorno = true;
            }

            return retorno;
        }
        #endregion
    }
}
