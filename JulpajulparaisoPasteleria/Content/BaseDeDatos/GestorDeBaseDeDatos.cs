using JulpajulparaisoPasteleria.Content.Gestores;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JulpajulparaisoPasteleria.Content.BaseDeDatos
{
    public static class GestorDeBaseDeDatos
    {
        private static string NombreArchivo = "julpajulparaiso.db";
        private static string CadenaConexión
        {
            get
            {
                string rutaEjecutable = AppDomain.CurrentDomain.BaseDirectory;
                string rutaCompletaBaseDatos = System.IO.Path.Combine(rutaEjecutable, NombreArchivo);
                return $"Data Source={rutaCompletaBaseDatos};Version=3;";
            }
        }
        public static SQLiteConnection ObtenerConexion()
        {
            SQLiteConnection conexion = new SQLiteConnection(CadenaConexión);
            conexion.Open();
            return conexion;
        }
        public static void InicializarBaseDeDatos()
        {
            using var conexion = ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
            CREATE TABLE IF NOT EXISTS PARTIDA (
                SLOT TEXT PRIMARY KEY,
                TIEMPO_ENTRE_CLIENTES REAL,
                CANTIDAD_CLIENTES INTEGER,
                DIA_ACTUAL INTEGER,
                PROMEDIO_PUNTUACION INTEGER
            );
            CREATE TABLE IF NOT EXISTS HISTORIAL_DIAS (
                ID_DIA INTEGER PRIMARY KEY AUTOINCREMENT,
                SLOT TEXT,
                PUNTUACION INTEGER,
                CANTIDAD_CLIENTES_SATISFECHOS INTEGER,
                CANTIDAD_CLIENTES_ENOJADOS INTEGER,
                FOREIGN KEY (SLOT) REFERENCES PARTIDA(SLOT)
            );";
            comando.ExecuteNonQuery();
        }
    }
}
