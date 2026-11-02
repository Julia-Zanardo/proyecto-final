using JulpajulparaisoPasteleria.Content.Gestores;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.BaseDeDatos
{
    public static class RepositorioPartida
    {
        public static void GuardarProgreso(string nombreSlot, int promedioPuntaje, int clientesSatisfichos, int clientesEnojados)
        {
            using var conexion = JulpajulparaisoPasteleria.Content.BaseDeDatos.GestorDeBaseDeDatos.ObtenerConexion();
            using var transaccion = conexion.BeginTransaction();

            try
            {
                string consultaPartida = @"
                INSERT OR REPLACE INTO PARTIDA (SLOT, TIEMPO_ENTRE_CLIENTES, CANTIDAD_CLIENTES, DIA_ACTUAL, PROMEDIO_PUNTUACION)
                VALUES (@slot, @tiempo, @cantClientes, @diaActual, @promedio);";

                using var ComandoPartida = new SQLiteCommand(consultaPartida, conexion, transaccion);
                ComandoPartida.Parameters.AddWithValue("@slot", nombreSlot);
                ComandoPartida.Parameters.AddWithValue("@tiempo", GestorDeJuego.TiempoEntreClientes);
                ComandoPartida.Parameters.AddWithValue("@cantClientes", GestorDeJuego.CantidadDeClientesPorDia);
                ComandoPartida.Parameters.AddWithValue("@diaActual", GestorDeJuego.DiaActual);
                ComandoPartida.Parameters.AddWithValue("@promedio", promedioPuntaje);
                ComandoPartida.ExecuteNonQuery();
                string consultaDia = @"
                INSERT INTO HISTORIAL_DIAS (SLOT, PUNTUACION, CANTIDAD_CLIENTES_SATISFECHOS, CANTIDAD_CLIENTES_ENOJADOS)
                VALUES (@slot, @puntuacion, @satisfechos, @enojados);";

                using var ComandoDia = new SQLiteCommand(consultaDia, conexion, transaccion);
                ComandoDia.Parameters.AddWithValue("@slot", nombreSlot);
                ComandoDia.Parameters.AddWithValue("@puntuacion", promedioPuntaje);
                ComandoDia.Parameters.AddWithValue("@satisfechos", clientesSatisfichos);
                ComandoDia.Parameters.AddWithValue("@enojados", clientesEnojados);
                ComandoDia.ExecuteNonQuery();
                transaccion.Commit();
            }
            catch (System.Exception ex)
            {
                transaccion.Rollback();
                throw new Exception("Error al guardar la partida: " + ex.Message);
            }
        }
        public static DatosResumenDia ObtenerUltimoDiaRegistrado(string nombreSlot)
        {
            SQLiteConnection conexion = GestorDeBaseDeDatos.ObtenerConexion();
            try
            {
                string query = @"
                    SELECT P.DIA_ACTUAL, D.PUNTUACION, D.CANTIDAD_CLIENTES_SATISFECHOS, 
                           D.CANTIDAD_CLIENTES_ENOJADOS, P.PROMEDIO_PUNTUACION
                    FROM DIA D
                    INNER JOIN PARTIDA P ON D.ID_PARTIDA = P.ID_PARTIDA
                    WHERE P.NOMBRE_SLOT = @slot
                    ORDER BY D.ID_DIA DESC LIMIT 1;";

                SQLiteCommand comando = new SQLiteCommand(query, conexion);
                try
                {
                    comando.Parameters.AddWithValue("@slot", nombreSlot);
                    SQLiteDataReader lector = comando.ExecuteReader();
                    try
                    {
                        if (lector.Read())
                        {
                            return new DatosResumenDia
                            {
                                Dia = Convert.ToInt32(lector["DIA_ACTUAL"]),
                                Puntuacion = Convert.ToInt32(lector["PUNTUACION"]),
                                ClientesSatisfechos = Convert.ToInt32(lector["CANTIDAD_CLIENTES_SATISFECHOS"]),
                                ClientesEnojados = Convert.ToInt32(lector["CANTIDAD_CLIENTES_ENOJADOS"]),
                                PromedioPuntuacion = Convert.ToInt32(lector["PROMEDIO_PUNTUACION"]),
                            };
                        }
                    }
                    finally
                    {
                        lector.Dispose();
                    }
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

            return null;
        }

        public static void CargarPartidaEnJuego(string nombreSlot)
        {
            SQLiteConnection conexion = GestorDeBaseDeDatos.ObtenerConexion();
            try
            {
                string query = @"
                    SELECT P.DIAS, P.TIEMPO_ENTRE_CLIENTES, P.CANTIDAD_CLIENTES, P.DIA_ACTUAL, P.PROMEDIO_PUNTUACION
                    FROM PARTIDA P
                    WHERE P.NOMBRE_SLOT = @slot;";

                SQLiteCommand comando = new SQLiteCommand(query, conexion);
                try
                {
                    comando.Parameters.AddWithValue("@slot", nombreSlot);
                    SQLiteDataReader lector = comando.ExecuteReader();
                    try
                    {
                        if (lector.Read())
                        {
                            GestorDeJuego.DiaActual = Convert.ToInt32(lector["DIA_ACTUAL"]);
                            GestorDeJuego.TiempoEntreClientes = Convert.ToSingle(lector["TIEMPO_ENTRE_CLIENTES"]);
                            GestorDeJuego.CantidadDeClientesPorDia = Convert.ToInt32(lector["CANTIDAD_CLIENTES"]);
                        }
                    }
                    finally
                    {
                        lector.Dispose();
                    }
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

        public static List<DatosResumenPartida> ObtenerTodasLasPartidas()
        {
            List<DatosResumenPartida> listaPartidas = new List<DatosResumenPartida>();

            SQLiteConnection conexion = GestorDeBaseDeDatos.ObtenerConexion();
            try
            {
                string query = @"
                    SELECT NOMBRE_SLOT, DIA_ACTUAL
                    FROM PARTIDA
                    ORDER BY ID_PARTIDA ASC;";

                SQLiteCommand comando = new SQLiteCommand(query, conexion);
                try
                {
                    SQLiteDataReader lector = comando.ExecuteReader();
                    try
                    {
                        while (lector.Read())
                        {
                            listaPartidas.Add(new DatosResumenPartida
                            {
                                NombreJugador = lector["NOMBRE_SLOT"].ToString(),
                                DiaActual = Convert.ToInt32(lector["DIA_ACTUAL"]),
                            });
                        }
                    }
                    finally
                    {
                        lector.Dispose();
                    }
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

            return listaPartidas;
        }
        public static int ObtenerSiguienteSlotDisponible()
        {
            var partidasExistentes = ObtenerTodasLasPartidas();

            // 1. Revisar del 1 al 4 cuál falta
            for (int i = 1; i <= 4; i++)
            {
                string nombreBuscado = "Partida " + i;
                bool ocupado = partidasExistentes.Any(p => p.NombreJugador == nombreBuscado);

                if (!ocupado)
                {
                    return i; // Hay un slot libre
                }
            }

            // 2. Si los 4 están ocupados, buscamos la partida más vieja (menor ID_PARTIDA)
            using (SQLiteConnection conexion = GestorDeBaseDeDatos.ObtenerConexion())
            {
                string query = "SELECT NOMBRE_SLOT FROM PARTIDA ORDER BY ID_PARTIDA ASC LIMIT 1;";
                using (SQLiteCommand comando = new SQLiteCommand(query, conexion))
                {
                    object resultado = comando.ExecuteScalar();
                    if (resultado != null)
                    {
                        string nombreMasVieja = resultado.ToString(); // Ejemplo: "Partida 3"
                                                                      // Extraemos el número del slot del nombre (asumiendo formato "Partida X")
                        if (int.TryParse(nombreMasVieja.Replace("Partida ", ""), out int numeroSlot))
                        {
                            // Opcional: Si quieres borrar físicamente la vieja para que no queden huerfanas las tablas DIA:
                            BorrarPartidaPorNombre(nombreMasVieja, conexion);
                            return numeroSlot;
                        }
                    }
                }
            }

            return 1; // Fallback por seguridad
        }

        // Método auxiliar recomendado para limpiar la vieja y sus días asociados:
        private static void BorrarPartidaPorNombre(string nombreSlot, SQLiteConnection conexion)
        {
            // 1. Borrar los días asociados primero
            string queryDias = "DELETE FROM DIA WHERE ID_PARTIDA IN (SELECT ID_PARTIDA FROM PARTIDA WHERE NOMBRE_SLOT = @slot);";
            using (SQLiteCommand cmdDias = new SQLiteCommand(queryDias, conexion))
            {
                cmdDias.Parameters.AddWithValue("@slot", nombreSlot);
                cmdDias.ExecuteNonQuery();
            }

            // 2. Borrar la partida
            string queryPartida = "DELETE FROM PARTIDA WHERE NOMBRE_SLOT = @slot;";
            using (SQLiteCommand cmdPartida = new SQLiteCommand(queryPartida, conexion))
            {
                cmdPartida.Parameters.AddWithValue("@slot", nombreSlot);
                cmdPartida.ExecuteNonQuery();
            }
        }
    }
}