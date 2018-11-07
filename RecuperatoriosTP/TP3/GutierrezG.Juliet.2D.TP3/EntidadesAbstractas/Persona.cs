using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;

namespace EntidadesAbstractas
{
    public abstract class Persona
    {
        public enum ENacionalidad
        {
            Argentino, Extranjero
        }

        #region Atributos
        string apellido;
        int dni;
        string nombre;
        ENacionalidad nacionalidad;
        #endregion

        #region Propiedades
        /// <summary>
        /// Obtiene o establece el apellido de una persona
        /// </summary>
        public string Apellido
        {
            get
            {
                return this.apellido;
            }
            set
            {
                this.apellido = ValidarNombreApellido(value);
            }
        }

        /// <summary>
        /// Obtiene o establece el dni de una persona
        /// </summary>
        public int DNI
        {
            get
            {
                return this.dni;
            }
            set
            {
                this.dni = ValidarDni(this.nacionalidad, value);
            }
        }

        /// <summary>
        /// Obtiene o establece la nacionalidad de una persona
        /// </summary>
        public ENacionalidad Nacionalidad
        {
            get
            {
                return this.nacionalidad;
            }
            set
            {
                this.nacionalidad = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el nombre de una persona
        /// </summary>
        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = ValidarNombreApellido(value);
            }
        }

        /// <summary>
        /// Establece el dni
        /// </summary>
        public string StringToDNI
        {
            set
            {
                this.DNI = ValidarDni(this.nacionalidad, value);
            }
        }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Persona()
        { }

        /// <summary>
        /// Constructor de instancia sólo recibe tres atributos de la clase
        /// </summary>
        /// <param name="nombre">nombre de la persona</param>
        /// <param name="apellido">apellido de la persona</param>
        /// <param name="nacionalidad">nacionalidad de la persona, argentino ó extranjero</param>
        public Persona(string nombre, string apellido, ENacionalidad nacionalidad)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Nacionalidad = nacionalidad;
        }

        /// <summary>
        /// Constructor de instancia que recibe los cuatro atributos de la clase.
        /// </summary>
        /// <param name="nombre">nombre de la persona</param>
        /// <param name="apellido">apellido de la persona</param>
        /// <param name="dni">dni, en formato int, de la persona</param>
        /// <param name="nacionalidad"></param>
        public Persona(string nombre, string apellido, int dni, ENacionalidad nacionalidad)
            : this(nombre, apellido, nacionalidad)
        {
            this.DNI = dni;
        }

        /// <summary>
        /// Constructor de instancia que recibe los cuatro atributos de la clase.
        /// </summary>
        /// <param name="nombre">nombre de la persona</param>
        /// <param name="apellido">apellido de la persona</param>
        /// <param name="dni">dni, en formato string, de la persona</param>
        /// <param name="nacionalidad"></param>
        public Persona(string nombre, string apellido, string dni, ENacionalidad nacionalidad)
            : this(nombre, apellido, nacionalidad)
        {
            this.StringToDNI = dni;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Hace público los datos de la persona
        /// </summary>
        /// <returns>Los datos de la persona</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("NOMBRE COMPLETO: {0}, {1}", this.Apellido, this.Nombre);
            sb.AppendFormat("\nNACIONALIDAD: {0}", this.Nacionalidad);
            //sb.AppendFormat("\nDNI: {0}", this.DNI);

            return sb.ToString();
        }

        /// <summary>
        /// Valida el dni en formato int
        /// </summary>
        /// <param name="nacionalidad">Nacionalidad de la persona</param>
        /// <param name="dato">número de dni</param>
        /// <returns>dni validado de otra forma lanza la excepción NacionalidadInvalidaException</returns>
        int ValidarDni(ENacionalidad nacionalidad, int dato)
        {

            if (nacionalidad == ENacionalidad.Extranjero && dato > 90000000)
            {
                return dato;
            }
            else if (nacionalidad == ENacionalidad.Argentino && dato > 1 && dato < 89999999)
            {
                return dato;
            }
            else
            {
                throw new NacionalidadInvalidaException("La nacionalidad no coincide con el numero de DNI");
            }
        }

        /// <summary>
        /// Valida el dni en formato string.
        /// </summary>
        /// <param name="nacionalidad">Nacionalidad de la persona actual</param>
        /// <param name="dato">dni, en formato string, de la persona actual</param>
        /// <returns>dato en int, de otra forma lanzará una excepción DniIvalidoException</returns>
        int ValidarDni(ENacionalidad nacionalidad, string dato)
        {
            int dni;

            if (!(dato.Length <= 8 && int.TryParse(dato, out dni)))
            {
                throw new DniInvalidoException();
            }

            return this.ValidarDni(nacionalidad, dni);
        }

        /// <summary>
        /// Válida el nombre ó el apellido
        /// </summary>
        /// <param name="dato">Dato a validar</param>
        /// <returns>El dato validado sino un string vacio ""</returns>
        string ValidarNombreApellido(string dato)
        {
            var charDato = dato.ToCharArray();

            for (int i = 0; i < charDato.Length; i++)
            {
                if (char.IsLetter(charDato[i]))
                {
                    continue;
                }
                else
                {
                    dato = "";
                    break;
                }
            }
            return dato;
        }
        #endregion
    }
}
