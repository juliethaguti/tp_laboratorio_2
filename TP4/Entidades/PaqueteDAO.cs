using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using Entidades;

namespace EntidadesDAO
{
    public static class PaqueteDAO
    {
        private static SqlCommand _comando;
        private static SqlConnection _conexion;

        #region Constructor
        /// <summary>
        /// Constructor estático, inicializa sqlCommand y SqlConnection
        /// </summary>
        static PaqueteDAO()
        {
            _comando = new SqlCommand();
            _conexion = new SqlConnection(@"Data Source=JULIETA-PC\SQLEXPRESS;Initial Catalog=correo-sp-2017;Integrated Security=True");
            PaqueteDAO._comando.CommandType = System.Data.CommandType.Text;
            PaqueteDAO._comando.Connection = PaqueteDAO._conexion;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Guarda los datos de un paquete en la base de datos generada.
        /// </summary>
        /// <param name="p">Paquete ha guardar</param>
        /// <returns>True si guarda exitosamente, de lo contrario false</returns>
        public static bool Insertar(Paquete p)
        {
            string sql = "INSERT INTO Paquetes (direccionEntrega,trackingID, alumno) VALUES(";
            sql = sql + "'" + p.DireccionEntrega + "','" + p.TrackingID + "','" + "Juliet Andrea Gutierrez'" + ")";
            return EjecutarNonQuery(sql);
        }

        private static bool EjecutarNonQuery(string sql)
        {
            bool retorno = false;

            try
            {
                PaqueteDAO._comando.CommandText = sql;
                PaqueteDAO._conexion.Open();
                PaqueteDAO._comando.ExecuteNonQuery();
                retorno = true;
            }
            catch(Exception e)
            {
                retorno = false;
            }
            finally
            {
                if(retorno)
                {
                    PaqueteDAO._conexion.Close();
                }
            }
            return retorno;
        }
        #endregion
    }
}
