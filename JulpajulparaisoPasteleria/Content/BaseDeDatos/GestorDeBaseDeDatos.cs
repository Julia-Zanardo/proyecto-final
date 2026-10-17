using JulpajulparaisoPasteleria.Content.Gestores;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.BaseDeDatos
{
    public static class GestorDeBaseDeDatos
    {
        private static string NombreArchivoDB = "julpajulparaiso.db";
        private static string CadenaConexión
        {
            get
            {
                return $"Data Source={NombreArchivoDB}";
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
            System.Data.IDbConnection conexion = ObtenerConexion();
            try
            {
                System.Data.IDbCommand comando = conexion.CreateCommand();
                try
                {
                    comando.CommandText = @"
                CREATE TABLE IF NOT EXISTS JUGADOR (
                    ID_JUGADOR INTEGER PRIMARY KEY AUTOINCREMENT,
                    NOMBRE TEXT NOT NULL UNIQUE
                );
                CREATE TABLE IF NOT EXISTS PARTIDA (
                    ID_PARTIDA INTEGER PRIMARY KEY AUTOINCREMENT,
                    ID_JUGADOR INTEGER,
                    DIAS INTEGER,
                    TIEMPO_ENTRE_CLIENTES REAL,
                    CANTIDAD_CLIENTES INTEGER,
                    DIA_ACTUAL INTEGER,
                    PROMEDIO_PUNTUACION INTEGER,
                    FOREIGN KEY (ID_JUGADOR) REFERENCES JUGADOR(ID_JUGADOR)
                );
                CREATE TABLE IF NOT EXISTS DIA (
                    ID_DIA INTEGER PRIMARY KEY AUTOINCREMENT,
                    ID_PARTIDA INTEGER,
                    PUNTUACION INTEGER,
                    CANTIDAD_CLIENTES_SATISFECHOS INTEGER,
                    CANTIDAD_CLIENTES_ENOJADOS INTEGER,
                    FOREIGN KEY (ID_PARTIDA) REFERENCES PARTIDA(ID_PARTIDA)
                );
            ";
                    comando.ExecuteNonQuery();
                }
                finally
                {
                    comando.Dispose();
                }
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
            }
        }
    }
}
