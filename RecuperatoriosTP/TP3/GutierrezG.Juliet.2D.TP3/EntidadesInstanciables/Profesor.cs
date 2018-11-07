using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;

namespace EntidadesInstanciables
{
    public class Profesor : Universitario
    {
        #region Atributos
        private Queue<Universidad.EClases> clasesDelDia;
        private static Random random;
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor estático, inicializa el atributo random
        /// </summary>
        static Profesor()
        {
            random = new Random();
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public Profesor()
        {

        }

        /// <summary>
        /// Constructor de instancia
        /// </summary>
        /// <param name="id">Legajo del profesor</param>
        /// <param name="nombre">Nombre del profesor</param>
        /// <param name="apellido">Apellido del profesor</param>
        /// <param name="dni">dni del profesor</param>
        /// <param name="nacionalidad">Nacionalidad del profesor Argentino o extranjero</param>
        public Profesor(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad)
            : base(id, nombre, apellido, dni, nacionalidad)
        {
            this.clasesDelDia = new Queue<Universidad.EClases>();
            this._randomClases();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Asigna dos clases al profesor de forma aleatoria
        /// </summary>
        private void _randomClases()
        {
            clasesDelDia.Enqueue((Universidad.EClases)random.Next(0, 4));
            clasesDelDia.Enqueue((Universidad.EClases)random.Next(0, 4));
        }

        /// <summary>
        /// Concatena los datos del profesor
        /// </summary>
        /// <returns>Retornará todos los datos del profesor</returns>
        protected override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.MostrarDatos());
            sb.AppendLine(this.ParticiparEnClase());

            return sb.ToString();
        }

        /// <summary>
        /// Concatena las clases que da el profesor
        /// </summary>
        /// <returns>Retorna las clases que da el profesor</returns>
        protected override string ParticiparEnClase()
        {
            StringBuilder sb = new StringBuilder();

            //sb.Append(base.ToString());
            sb.AppendLine("\nCLASES DEL DIA");
            foreach (Universidad.EClases c in clasesDelDia)
            {
                sb.AppendLine(this.clasesDelDia.Peek().ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Hace público los datos del profesor
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.MostrarDatos();
        }
        #endregion

        #region Sobrecargas de operadores
        /// <summary>
        /// Determina si el profesor no da la clase
        /// </summary>
        /// <param name="i">profesor</param>
        /// <param name="clase"></param>
        /// <returns>Retorna true si el profesor no da la clase, caso contrario false</returns>
        public static bool operator !=(Profesor i, Universidad.EClases clase)
        {
            return !(i == clase);
        }

        /// <summary>
        /// Determina si un profesor da la clase
        /// </summary>
        /// <param name="i">profesor</param>
        /// <param name="clase"></param>
        /// <returns>Retorna true si el profesor da la clase, caso contrario false</returns>
        public static bool operator ==(Profesor i, Universidad.EClases clase)
        {
            bool retorno = false;

            foreach (Universidad.EClases c in i.clasesDelDia)
            {
                if (c == clase)
                {
                    retorno = true;
                }
            }
            return retorno;
        }
        #endregion
    }
}
