using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archivo;
using Excepciones;

namespace EntidadesInstanciables
{
    public class Jornada
    {
        private List<Alumno> alumnos;
        private Universidad.EClases clase;
        private Profesor instructor;

        #region Propiedades
        /// <summary>
        /// Obtiene y esstablece la lista de alumnos
        /// </summary>
        public List<Alumno> Alumnos
        {
            get
            {
                return this.alumnos;
            }
            set
            {
                this.alumnos = value;
            }
        }

        /// <summary>
        /// Obtiene y establece la clase
        /// </summary>
        public Universidad.EClases Clase
        {
            get
            {
                return this.clase;
            }
            set
            {
                this.clase = value;
            }
        }

        /// <summary>
        /// Obtiene y establece el profesor
        /// </summary>
        public Profesor Instructor
        {
            get
            {
                return this.instructor;
            }
            set
            {
                this.instructor = value;
            }
        }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por defecto, inicializa la lista de alumnos
        /// </summary>
        public Jornada()
        {
            this.Alumnos = new List<Alumno>();
        }

        /// <summary>
        /// Constructor de instancia, recibe los otros dos atributos de la clase
        /// </summary>
        /// <param name="clase">nombre de la clase</param>
        /// <param name="instructor">Profesor que dará la clase</param>
        public Jornada(Universidad.EClases clase, Profesor instructor)
            : this()
        {
            this.Clase = clase;
            this.Instructor = instructor;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Guarda una jornada en un archivo de texto
        /// </summary>
        /// <param name="jornada"></param>
        /// <returns>True si puede guardar la jornada</returns>
        public static bool Guardar(Jornada jornada)
        {
            bool retorno = false;
            IArchivo<string> archivosTexto = new Texto();
            string ruta = AppDomain.CurrentDomain.BaseDirectory + "Jornada.txt";

            try
            {
                retorno = archivosTexto.Guardar(ruta, jornada.ToString());
            }
            catch (ArchivosException e)
            {
                throw e;
            }

            return retorno;
        }

        /// <summary>
        /// Lee un archivo de texto
        /// </summary>
        /// <returns>Los dato de lectura</returns>
        public string Leer()
        {
            IArchivo<string> archivosTexto = new Texto();
            string lectura;

            try
            {
                archivosTexto.Leer("Jornada.txt", out lectura);
            }
            catch (ArchivosException e)
            {
                throw e;
            }

            return lectura;
        }

        /// <summary>
        /// Mostrará todos los datos de la jornada
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\nCLASE DE: {0} POR {1}", this.Clase, this.Instructor.ToString());
            //sb.AppendFormat(" POR NOMBRE COMPLETO: {0}, {1}", this.Instructor.Apellido,this.Instructor.Nombre);
            sb.AppendLine("\nALUMNOS:");
            foreach (Alumno a in this.Alumnos)
            {
                sb.AppendLine(a.ToString());
            }
            sb.AppendLine("-------------------------------------------------->");
            return sb.ToString();
        }
        #endregion

        #region Sobrecargas de operadores
        /// <summary>
        /// Determina si el alumno no participa de la clase.
        /// </summary>
        /// <param name="j">Jornada</param>
        /// <param name="a">Alumno</param>
        /// <returns>True si ese alumno no participa en clase, de lo contrario false</returns>
        public static bool operator !=(Jornada j, Alumno a)
        {
            return !(j == a);
        }

        /// <summary>
        /// Determina si el alumno participa de la clase
        /// </summary>
        /// <param name="j">Jornada</param>
        /// <param name="a">Alumno</param>
        /// <returns>True si ese alumno participa en la clase, de lo contrario false</returns>
        public static bool operator ==(Jornada j, Alumno a)
        {
            return j.Alumnos.Contains(a);
        }

        /// <summary>
        /// Agrega un alumno a la clase si éste todavía no está en ella.
        /// </summary>
        /// <param name="j">Jornada</param>
        /// <param name="a">Alumno</param>
        /// <returns>Devuelve la jornada con ese alumno agregado en caso de que no esté</returns>
        public static Jornada operator +(Jornada j, Alumno a)
        {
            if (j != a)
            {
                j.Alumnos.Add(a);
            }
            return j;
        }
        #endregion
    }
}
