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
            SQLiteConnection conexion = JulpajulparaisoPasteleria.Content.BaseDeDatos.GestorDeBaseDeDatos.ObtenerConexion();
            SQLiteTransaction transaccion = conexion.BeginTransaction();

            try
            {
                int idPartida = -1;

                // 1. Verificar si ya existe la partida por el texto (ej: "Partida 1")
                SQLiteCommand cmdVerificarPartida = conexion.CreateCommand();
                try
                {
                    cmdVerificarPartida.Transaction = transaccion;
                    cmdVerificarPartida.CommandText = "SELECT ID_PARTIDA FROM PARTIDA WHERE NOMBRE_SLOT = @slot;";
                    cmdVerificarPartida.Parameters.AddWithValue("@slot", nombreSlot);
                    object resultadoPartida = cmdVerificarPartida.ExecuteScalar();

                    if (resultadoPartida != null)
                    {
                        idPartida = Convert.ToInt32(resultadoPartida);
                    }
                }
                finally
                {
                    cmdVerificarPartida.Dispose();
                }

                // 2. Actualizar o insertar según corresponda
                if (idPartida != -1)
                {
                    SQLiteCommand cmdUpdatePartida = conexion.CreateCommand();
                    try
                    {
                        cmdUpdatePartida.Transaction = transaccion;
                        cmdUpdatePartida.CommandText = @"
                            UPDATE PARTIDA 
                            SET DIAS = @dias, 
                                TIEMPO_ENTRE_CLIENTES = @tiempo, 
                                CANTIDAD_CLIENTES = @cantClientes, 
                                DIA_ACTUAL = @diaActual, 
                                PROMEDIO_PUNTUACION = @promedio
                            WHERE ID_PARTIDA = @idPartida;";

                        cmdUpdatePartida.Parameters.AddWithValue("@idPartida", idPartida);
                        cmdUpdatePartida.Parameters.AddWithValue("@dias", GestorDeJuego.DiaActual);
                        cmdUpdatePartida.Parameters.AddWithValue("@tiempo", GestorDeJuego.TiempoEntreClientes);
                        cmdUpdatePartida.Parameters.AddWithValue("@cantClientes", GestorDeJuego.CantidadDeClientesPorDia);
                        cmdUpdatePartida.Parameters.AddWithValue("@diaActual", GestorDeJuego.DiaActual);
                        cmdUpdatePartida.Parameters.AddWithValue("@promedio", promedioPuntaje);

                        cmdUpdatePartida.ExecuteNonQuery();
                    }
                    finally
                    {
                        cmdUpdatePartida.Dispose();
                    }
                }
                else
                {
                    SQLiteCommand cmdInsertPartida = conexion.CreateCommand();
                    try
                    {
                        cmdInsertPartida.Transaction = transaccion;
                        cmdInsertPartida.CommandText = @"
                            INSERT INTO PARTIDA (NOMBRE_SLOT, DIAS, TIEMPO_ENTRE_CLIENTES, CANTIDAD_CLIENTES, DIA_ACTUAL, PROMEDIO_PUNTUACION)
                            VALUES (@slot, @dias, @tiempo, @cantClientes, @diaActual, @promedio);
                            SELECT last_insert_rowid();";

                        cmdInsertPartida.Parameters.AddWithValue("@slot", nombreSlot);
                        cmdInsertPartida.Parameters.AddWithValue("@dias", GestorDeJuego.DiaActual);
                        cmdInsertPartida.Parameters.AddWithValue("@tiempo", GestorDeJuego.TiempoEntreClientes);
                        cmdInsertPartida.Parameters.AddWithValue("@cantClientes", GestorDeJuego.CantidadDeClientesPorDia);
                        cmdInsertPartida.Parameters.AddWithValue("@diaActual", GestorDeJuego.DiaActual);
                        cmdInsertPartida.Parameters.AddWithValue("@promedio", promedioPuntaje);

                        idPartida = Convert.ToInt32(cmdInsertPartida.ExecuteScalar());
                    }
                    finally
                    {
                        cmdInsertPartida.Dispose();
                    }
                }

                // 3. Registrar el día
                SQLiteCommand cmdDia = conexion.CreateCommand();
                try
                {
                    cmdDia.Transaction = transaccion;
                    cmdDia.CommandText = @"
                        INSERT INTO DIA (ID_PARTIDA, PUNTUACION, CANTIDAD_CLIENTES_SATISFECHOS, CANTIDAD_CLIENTES_ENOJADOS)
                        VALUES (@idPartida, @puntuacion, @satisfechos, @enojados);";

                    cmdDia.Parameters.AddWithValue("@idPartida", idPartida);
                    cmdDia.Parameters.AddWithValue("@puntuacion", promedioPuntaje);
                    cmdDia.Parameters.AddWithValue("@satisfechos", clientesSatisfichos);
                    cmdDia.Parameters.AddWithValue("@enojados", clientesEnojados);

                    cmdDia.ExecuteNonQuery();
                }
                finally
                {
                    cmdDia.Dispose();
                }

                transaccion.Commit();
            }
            catch (System.Exception ex)
            {
                transaccion.Rollback();
                throw new Exception("Error al guardar la partida: " + ex.Message);
            }
            finally
            {
                transaccion.Dispose();
                conexion.Close();
                conexion.Dispose();
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
            var partidasExistentes = ObtenerTodasLasPartidas(); // Trae las partidas guardadas

            // Revisamos del 1 al 4 cuál falta
            for (int i = 1; i <= 4; i++)
            {
                string nombreBuscado = "Partida " + i;
                // Si no encuentra ninguna partida con este nombre, este slot está libre
                bool ocupado = partidasExistentes.Any(p => p.NombreJugador == nombreBuscado);

                if (!ocupado)
                {
                    return i; // ¡Encontró un slot libre! (1, 2, 3 o 4)
                }
            }

            // Si los 4 slots están ocupados, por defecto devolvemos el 1 (o el que prefieras sobrescribir)
            return 1;
        }
    }
}