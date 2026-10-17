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
        public static void GuardarProgreso(string nombreJugador, int promedioPuntaje, int clientesSatisfichos, int clientesEnojados)
        {
            SQLiteConnection conexion = JulpajulparaisoPasteleria.Content.BaseDeDatos.GestorDeBaseDeDatos.ObtenerConexion();
            SQLiteTransaction transaccion = conexion.BeginTransaction();

            try
            {
                GestorDeJuego.AvanzarDeDia();

                int idJugador = -1;

                SQLiteCommand cmdVerificarJugador = conexion.CreateCommand();
                try
                {
                    cmdVerificarJugador.Transaction = transaccion;
                    cmdVerificarJugador.CommandText = "SELECT ID_JUGADOR FROM JUGADOR WHERE NOMBRE = @nombre;";
                    cmdVerificarJugador.Parameters.AddWithValue("@nombre", nombreJugador);
                    object resultadoJugador = cmdVerificarJugador.ExecuteScalar();

                    if (resultadoJugador != null)
                    {
                        idJugador = Convert.ToInt32(resultadoJugador);
                    }
                }
                finally
                {
                    cmdVerificarJugador.Dispose();
                }

                if (idJugador == -1)
                {
                    SQLiteCommand cmdInsertarJugador = conexion.CreateCommand();
                    try
                    {
                        cmdInsertarJugador.Transaction = transaccion;
                        cmdInsertarJugador.CommandText = "INSERT INTO JUGADOR (NOMBRE) VALUES (@nombre); SELECT last_insert_rowid();";
                        cmdInsertarJugador.Parameters.AddWithValue("@nombre", nombreJugador);
                        idJugador = Convert.ToInt32(cmdInsertarJugador.ExecuteScalar());
                    }
                    finally
                    {
                        cmdInsertarJugador.Dispose();
                    }
                }

                int idPartida = -1;

                SQLiteCommand cmdVerificarPartida = conexion.CreateCommand();
                try
                {
                    cmdVerificarPartida.Transaction = transaccion;
                    cmdVerificarPartida.CommandText = "SELECT ID_PARTIDA FROM PARTIDA WHERE ID_JUGADOR = @idJugador ORDER BY ID_PARTIDA DESC LIMIT 1;";
                    cmdVerificarPartida.Parameters.AddWithValue("@idJugador", idJugador);
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
                    INSERT INTO PARTIDA (ID_JUGADOR, DIAS, TIEMPO_ENTRE_CLIENTES, CANTIDAD_CLIENTES, DIA_ACTUAL, PROMEDIO_PUNTUACION)
                    VALUES (@idJugador, @dias, @tiempo, @cantClientes, @diaActual, @promedio);
                    SELECT last_insert_rowid();";

                        cmdInsertPartida.Parameters.AddWithValue("@idJugador", idJugador);
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
                throw new Exception("Error al guardar la partida en la base de datos: " + ex.Message);
            }
            finally
            {
                transaccion.Dispose();
                conexion.Close();
                conexion.Dispose();
            }
        }
        public static DatosResumenPartida ObtenerResumenPartida(string nombreJugador)
        {
            SQLiteConnection conexion = GestorDeBaseDeDatos.ObtenerConexion();
            try
            {
                string query = @"
            SELECT J.NOMBRE, P.DIA_ACTUAL
            FROM PARTIDA P
            INNER JOIN JUGADOR J ON P.ID_JUGADOR = J.ID_JUGADOR
            WHERE J.NOMBRE = @nombre
            ORDER BY P.ID_PARTIDA DESC LIMIT 1;";

                SQLiteCommand comando = new SQLiteCommand(query, conexion);
                try
                {
                    comando.Parameters.AddWithValue("@nombre", nombreJugador);
                    SQLiteDataReader lector = comando.ExecuteReader();
                    try
                    {
                        if (lector.Read())
                        {
                            return new DatosResumenPartida
                            {
                                NombreJugador = lector["NOMBRE"].ToString(),
                                DiaActual =Convert.ToInt32(lector["DIA_ACTUAL"]),
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

        public static DatosResumenDia ObtenerUltimoDiaRegistrado(string nombreJugador)
        {
            SQLiteConnection conexion = GestorDeBaseDeDatos.ObtenerConexion();
            try
            {
                string query = @"
            SELECT P.DIA_ACTUAL, D.PUNTUACION, D.CANTIDAD_CLIENTES_SATISFECHOS, 
                   D.CANTIDAD_CLIENTES_ENOJADOS, P.PROMEDIO_PUNTUACION
            FROM DIA D
            INNER JOIN PARTIDA P ON D.ID_PARTIDA = P.ID_PARTIDA
            INNER JOIN JUGADOR J ON P.ID_JUGADOR = J.ID_JUGADOR
            WHERE J.NOMBRE = @nombre
            ORDER BY D.ID_DIA DESC LIMIT 1;";

                SQLiteCommand comando = new SQLiteCommand(query, conexion);
                try
                {
                    comando.Parameters.AddWithValue("@nombre", nombreJugador);
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
        public static void CargarPartidaEnJuego(string nombreJugador)
        {
            SQLiteConnection conexion = GestorDeBaseDeDatos.ObtenerConexion();
            try
            {
                string query = @"
            SELECT P.DIAS, P.TIEMPO_ENTRE_CLIENTES, P.CANTIDAD_CLIENTES, P.DIA_ACTUAL, P.PROMEDIO_PUNTUACION
            FROM PARTIDA P
            INNER JOIN JUGADOR J ON P.ID_JUGADOR = J.ID_JUGADOR
            WHERE J.NOMBRE = @nombre
            ORDER BY P.ID_PARTIDA DESC LIMIT 1;";

                SQLiteCommand comando = new SQLiteCommand(query, conexion);
                try
                {
                    comando.Parameters.AddWithValue("@nombre", nombreJugador);
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
            SELECT J.NOMBRE, P.DIA_ACTUAL
            FROM PARTIDA P
            INNER JOIN JUGADOR J ON P.ID_JUGADOR = J.ID_JUGADOR
            ORDER BY P.ID_PARTIDA DESC;";

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
                                NombreJugador = lector["NOMBRE"].ToString(),
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
    }
}