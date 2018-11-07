using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excepciones;

namespace Archivo
{
    public class Texto : IArchivo<string>
    {
        /// <summary>
        /// Guarda en formato  texto
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns>True si puede guardar, false de lo contrario</returns>
        public bool Guardar(string archivo, string datos)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(archivo, true))
                {
                    file.WriteLine(datos);
                }
                return true;
            }
            catch (ArchivosException e)
            {
                return false;
            }
        }

        /// <summary>
        /// Lee en formato texto
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns>True si pudo leer de lo contraio false</returns>
        public bool Leer(string archivo, out string datos)
        {
            bool retorno = false;

            try
            {
                using (StreamReader file = new StreamReader(archivo))
                {
                    datos = file.ReadToEnd();
                }
                retorno = true;
            }
            catch (ArchivosException e)
            {
                datos = "";
            }

            return retorno;
        }
    }
}
