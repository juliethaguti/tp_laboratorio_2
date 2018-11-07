using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archivo;
using Excepciones;

namespace EntidadesInstanciables
{
    [Serializable]
    public class Universidad
    {
        #region Enumeraciones
        public enum EClases
        {
            Programacion, Laboratorio, Legislacion, SPD
        }
        #endregion

        #region Atributos
        List<Alumno> alumnos;
        List<Jornada> jornada;
        List<Profesor> profesores;
        #endregion

        #region Propiedades
        /// <summary>
        /// Obtiene y establece la lista de alumnos de la universidad
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
        /// Obtiene y establece la lista de profesores de la universidad
        /// </summary>
        public List<Profesor> Instructores
        {
            get
            {
                return this.profesores;
            }
            set
            {
                this.profesores = value;
            }
        }

        /// <summary>
        /// Obtiene y establece la lista de Jornadas de la universidad
        /// </summary>
        public List<Jornada> Jornadas
        {
            get
            {
                return this.jornada;
            }
            set
            {
                this.jornada = value;
            }
        }


        /// <summary>
        /// Obtiene y establece una jornada específica
        /// </summary>
        /// <param name="i">índice donde se encuentra la jornada</param>
        /// <returns></returns>
        public Jornada this[int i]
        {
            get
            {
                return jornada[i];
            }
            set
            {
                jornada[i] = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor por defecto, inicializa todas las listas.
        /// </summary>
        public Universidad()
        {
            this.Alumnos = new List<Alumno>();
            this.Instructores = new List<Profesor>();
            this.Jornadas = new List<Jornada>();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Serializa una universidad en xml
        /// </summary>
        /// <param name="uni">Universidad a serializar</param>
        /// <returns>True si logra guardar de lo contrario false</returns>
        public static bool Guardar(Universidad uni)
        {
            bool retorno = false;
            IArchivo<Universidad> archivosXml = new Xml<Universidad>();
            string ruta = AppDomain.CurrentDomain.BaseDirectory + "Universidad.xml";

            try
            {
                retorno = archivosXml.Guardar(ruta, uni);
            }
            catch (ArchivosException e)
            {
                throw e;
            }
            return retorno;
        }

        /// <summary>
        /// Concatena todos los datos de la universidad.
        /// </summary>
        /// <param name="uni">Universidad</param>
        /// <returns>Retorna todos los datos de la univeridad</returns>
        private static string MostrarDatos(Universidad uni)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < uni.Jornadas.Count; i++)
            {
                sb.AppendLine("JORNADA:");
                sb.AppendLine(uni.Jornadas[i].ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Hace público los datos de la universidad.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Universidad.MostrarDatos(this);
        }
        #endregion

        #region Sobrecargas de Operadores
        /// <summary>
        /// Determina si un alumno no está inscripto en la universidad
        /// </summary>
        /// <param name="g">Universidad</param>
        /// <param name="a">Alumno</param>
        /// <returns>Si ese alumno no está inscripto en esa universidad retornará true,
        /// de lo contrario false</returns>
        public static bool operator !=(Universidad g, Alumno a)
        {
            return !(g == a);
        }

        /// <summary>
        /// Determina si un profesor no da clases en la universidad
        /// </summary>
        /// <param name="g"><Universidad/param>
        /// <param name="i">Pofesor</param>
        /// <returns>True si no da clases en esa universidad de lo contrario false</returns>
        public static bool operator !=(Universidad g, Profesor i)
        {
            return !(g == i);
        }

        /// <summary>
        /// Retorna el primer profesor que no pueda dar la clase
        /// </summary>
        /// <param name="u">Universidad</param>Universidad en la que está el profesor
        /// <param name="clase"></param>La clase que no puede dar.
        /// <returns>Primer profesor que no pueda dar esa clase en ea universidad</returns>
        public static Profesor operator !=(Universidad u, Universidad.EClases clase)
        {
            Profesor profe = null;
            foreach (Profesor p in u.Instructores)
            {
                if (!(p == clase))
                {
                    profe = p;
                }
            }
            return profe;
        }

        /// <summary>
        /// Agregará una clase a la universidad
        /// </summary>
        /// <param name="g">Universidad</param>
        /// <param name="clase"></param>
        /// <returns>Universidad con la clase agregada</returns>
        public static Universidad operator +(Universidad g, Universidad.EClases clase)
        {
            Jornada j = new Jornada(clase, g == clase);

            foreach (Alumno a in g.Alumnos)
            {
                if (a == clase)
                {
                    j.Alumnos.Add(a);
                }
            }

            g.Jornadas.Add(j);

            return g;
        }

        /// <summary>
        /// Agregará un alumno a la universidad si este todavía no está agregado
        /// </summary>
        /// <param name="u">Unniversidad</param>
        /// <param name="a">Alumno</param>
        /// <returns>Esa universidad con ese alumno agregado en caso que no esté inscripto</returns>
        public static Universidad operator +(Universidad u, Alumno a)
        {
            if (u == a)
            {
                throw new AlumnoRepetidoException();
            }
            else
            {
                u.Alumnos.Add(a);
            }
            return u;
        }

        /// <summary>
        /// Agregará un profesor a la universidad si este todavía no está agregado
        /// </summary>
        /// <param name="u">Universidad</param>
        /// <param name="i">Profesor</param>
        /// <returns>Esa universidad con ese profesor agregado</returns>
        public static Universidad operator +(Universidad u, Profesor i)
        {
            if (u != i)
            {
                u.Instructores.Add(i);
            }

            return u;
        }

        /// <summary>
        /// Determina si un alumno está inscripto a la universidad.
        /// </summary>
        /// <param name="g">Universidad</param>
        /// <param name="a">Alumno</param>
        /// <returns>True si ese alumno está inscripto en la universidad, de lo contrario false</returns>
        public static bool operator ==(Universidad g, Alumno a)
        {
            bool retorno = false;

            foreach (Alumno alumno in g.Alumnos)
            {
                if (a == alumno)
                {
                    retorno = true;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Determina si un profesor está dando clases en esa universidad
        /// </summary>
        /// <param name="g">Universidad</param>
        /// <param name="i">Profesor</param>
        /// <returns>True si ese profesor da clases en esa universidad
        /// de lo contrario false</returns>
        public static bool operator ==(Universidad g, Profesor i)
        {
            bool retorno = false;

            if (g.Instructores.Contains(i))
            {
                retorno = true;
            }

            return retorno;
        }

        /// <summary>
        /// Encuentra el primer profesor en poder dar esa clase
        /// </summary>
        /// <param name="u">Universidad</param>
        /// <param name="clase"></param>
        /// <returns>El primer profesor de esa universidad que pueda dar esa clase</returns>
        public static Profesor operator ==(Universidad u, Universidad.EClases clase)
        {
            foreach (Profesor p in u.Instructores)
            {
                if (p == clase)
                {
                    return p;
                }
            }

            throw new SinProfesorException();
        }
        #endregion
    }
}
