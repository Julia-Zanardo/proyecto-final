using System;
using System.Data.SQLite;
using System.IO;

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
                string rutaCompletaBaseDatos = Path.Combine(rutaEjecutable, NombreArchivo);
                return $"Data Source={rutaCompletaBaseDatos};Version=3;";
            }
        }

        public static SQLiteConnection ObtenerConexion()
        {
            SQLiteConnection conexion = new SQLiteConnection(CadenaConexión);
            conexion.Open();
            using var comandoPragma = new SQLiteCommand("PRAGMA foreign_keys = ON;", conexion);
            comandoPragma.ExecuteNonQuery();

            return conexion;
        }

        public static void InicializarBaseDeDatos()
        {
            using var conexion = ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
            CREATE TABLE IF NOT EXISTS PARTIDAS (
                SLOT TEXT PRIMARY KEY,
                TIEMPO_ENTRE_CLIENTES REAL,
                CANTIDAD_CLIENTES INTEGER,
                DIA_ACTUAL INTEGER,
                FECHA_CREACION DATETIME DEFAULT CURRENT_TIMESTAMP 
            );

            CREATE TABLE IF NOT EXISTS HISTORIAL_DIAS (
                ID_DIA INTEGER PRIMARY KEY AUTOINCREMENT,
                SLOT TEXT NOT NULL,
                PUNTUACION INTEGER NOT NULL,
                CANTIDAD_CLIENTES_SATISFECHOS INTEGER NOT NULL,
                CANTIDAD_CLIENTES_ENOJADOS INTEGER NOT NULL,
                FOREIGN KEY (SLOT) REFERENCES PARTIDAS(SLOT)
                ON DELETE CASCADE
                ON UPDATE RESTRICT
            );";

            comando.ExecuteNonQuery();
        }
    }
}