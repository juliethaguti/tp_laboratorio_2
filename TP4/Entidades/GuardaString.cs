using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entidades
{
    public static class GuardaString
    {
        /// <summary>
        /// Guarda en un archivo de texto en el escritorio de la máquina
        /// </summary>
        /// <param name="texto">Contenido ha guardar</param>
        /// <param name="archivo">Nombre del archivo</param>
        /// <returns></returns>
        public static bool Guardar(this string texto, string archivo)
        {
            bool retorno = false;
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = folder + Path.DirectorySeparatorChar + archivo;
            StreamWriter sw = null;

            try
            {
                if (File.Exists(filePath))
                {
                    sw = new StreamWriter(filePath, true);
                }
                else
                {
                    sw = new StreamWriter(filePath);
                }

                sw.Write(texto);
                retorno = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sw.Close();
            }

            return retorno;
        }
    }
}
