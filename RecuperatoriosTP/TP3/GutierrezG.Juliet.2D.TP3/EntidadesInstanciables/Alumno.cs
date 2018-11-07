using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;

namespace EntidadesInstanciables
{
    public sealed class Alumno : Universitario
    {
        #region Enumeraciones
        public enum EEstadoCuenta
        {
            AlDia, Deudor, Becado
        }
        #endregion

        private Universidad.EClases claseQueToma;
        private EEstadoCuenta estadoCuenta;

        #region Constructores
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Alumno()
        { }

        /// <summary>
        /// Constructor de instancia
        /// </summary>
        /// <param name="id">legajo del alumno</param>
        /// <param name="nombre">nombre del alumno</param>
        /// <param name="apellido">apellido del alumno</param>
        /// <param name="dni">dni del alumno</param>
        /// <param name="nacionalidad">nacionalidad del alumno, extranjero ó argentino</param>
        /// <param name="clasesQueToma">Clase que toma el alumno</param>
        public Alumno(int id, string nombre, string apellido, string dni, Persona.ENacionalidad nacionalidad,
            Universidad.EClases clasesQueToma)
            : base(id, nombre, apellido, dni, nacionalidad)
        {
            this.claseQueToma = clasesQueToma;
        }

        /// <summary>
        /// Constructor de instancia
        /// </summary>
        /// <param name="id">legajo del alumno</param>
        /// <param name="nombre">nombre del alumno</param>
        /// <param name="apellido">apellido del alumno</param>
        /// <param name="dni">dni del alumno</param>
        /// <param name="nacionalidad">nacionalidad del alumno, extranjero o Argentino</param>
        /// <param name="estadoCuenta">Estado de cuenta del alumno: al dia, deudor ó becado</param>
        public Alumno(int id, string nombre, string apellido, string dni, Persona.ENacionalidad nacionalidad,
            Universidad.EClases claseQueToma, EEstadoCuenta estadoCuenta)
            : this(id, nombre, apellido, dni, nacionalidad, claseQueToma)
        {
            this.estadoCuenta = estadoCuenta;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Concatena todos los datos del alumno.
        /// </summary>
        /// <returns>Retorna los datos del alumno</returns>
        protected override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.MostrarDatos());
            sb.Append(this.ParticiparEnClase());
            sb.AppendFormat("\nESTADO DE CUENTA: {0}", this.estadoCuenta);

            return sb.ToString();
        }

        /// <summary>
        /// Informa la clase que toma el alumno actual.
        /// </summary>
        /// <returns>Retornará la clase que toma el alumno</returns>
        protected override string ParticiparEnClase()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("\nTOMA CLASE DE: {0}\n", this.claseQueToma);

            return sb.ToString();
        }

        /// <summary>
        /// Hace público los datos del alumno
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.MostrarDatos();
        }
        #endregion

        #region Sobrecargas de Operadores
        /// <summary>
        /// Determina si el alumno toma la clase y su estado de cuenta es diferente a deudor.
        /// </summary>
        /// <param name="a">alumno</param> 
        /// <param name="clase"></param>
        /// <returns>True si ese alumno toma esa clase y su estado es diferente a deudor de lo
        /// contrario false</returns>
        public static bool operator ==(Alumno a, Universidad.EClases clase)
        {
            bool retorno = false;
            if ((!(a != clase)) && a.estadoCuenta != EEstadoCuenta.Deudor)
            {
                retorno = true;
            }

            return retorno;
        }

        /// <summary>
        /// Determina si el alumno no toma esa clase
        /// </summary>
        /// <param name="a">alumno</param>
        /// <param name="clase"></param>
        /// <returns>True si ese alumno no toma esa clase, de lo contraio false</returns>
        public static bool operator !=(Alumno a, Universidad.EClases clase)
        {
            bool retorno = false;

            if (a.claseQueToma != clase)
            {
                retorno = true;
            }
            return retorno;
        }
        #endregion
    }
}
