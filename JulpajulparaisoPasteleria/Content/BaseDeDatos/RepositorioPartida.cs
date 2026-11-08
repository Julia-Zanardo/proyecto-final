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
        public static void CargarPartidaEnJuego(string nombreSlot)
        {
            using var conexion = GestorDeBaseDeDatos.ObtenerConexion();

            string consulta = @"
                SELECT P.TIEMPO_ENTRE_CLIENTES, P.CANTIDAD_CLIENTES, P.DIA_ACTUAL, P.PROMEDIO_PUNTUACION
                FROM PARTIDA P
                WHERE P.SLOT = @slot;";
            using var comando = new SQLiteCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@slot", nombreSlot);
            SQLiteDataReader lector = comando.ExecuteReader();
            if (lector.Read())
            {
                GestorDeJuego.DiaActual = Convert.ToInt32(lector["DIA_ACTUAL"]);
                GestorDeJuego.TiempoEntreClientes = Convert.ToSingle(lector["TIEMPO_ENTRE_CLIENTES"]);
                GestorDeJuego.CantidadDeClientesPorDia = Convert.ToInt32(lector["CANTIDAD_CLIENTES"]);
            }
        }

        public static List<DatosResumenPartida> ObtenerTodasLasPartidas()
        {
            List<DatosResumenPartida> listaPartidas = new List<DatosResumenPartida>();

            using var conexion = GestorDeBaseDeDatos.ObtenerConexion();
            string consulta = @"
                    SELECT SLOT, DIA_ACTUAL
                    FROM PARTIDA
                    ORDER BY SLOT ASC;";

            using var comando = new SQLiteCommand(consulta, conexion);
            SQLiteDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                listaPartidas.Add(
                    new DatosResumenPartida
                    {
                        NombreJugador = lector["SLOT"].ToString(),
                        DiaActual = Convert.ToInt32(lector["DIA_ACTUAL"]),
                    });
            }

            return listaPartidas;
        }
        public static int ObtenerSiguienteSlotDisponible()
        {
            List<DatosResumenPartida> partidasExistentes = ObtenerTodasLasPartidas();
            if (partidasExistentes.Count < 4)
            {
                return partidasExistentes.Count + 1;
            }
            using var conexion = GestorDeBaseDeDatos.ObtenerConexion();
            string consulta = "SELECT NOMBRE_SLOT FROM PARTIDA ORDER BY ID_PARTIDA ASC LIMIT 1;";
            using var comando = new SQLiteCommand(consulta, conexion);
            var resultado = comando.ExecuteScalar();
            string nombreMasVieja = resultado.ToString();
            if (int.TryParse(nombreMasVieja.Replace("Partida ", ""), out int numeroSlot))
            {
                BorrarPartidaPorNombre(nombreMasVieja, conexion);
                return numeroSlot;
            }
            return 1;
        }
        private static void BorrarPartidaPorNombre(string nombreSlot, SQLiteConnection conexion)
        {
            string consultaDias = "DELETE FROM DIA WHERE ID_PARTIDA IN (SELECT ID_PARTIDA FROM PARTIDA WHERE NOMBRE_SLOT = @slot);";
            using var comandoDias = new SQLiteCommand(consultaDias, conexion);
            comandoDias.Parameters.AddWithValue("@slot", nombreSlot);
            string consultaPartida = "DELETE FROM PARTIDA WHERE NOMBRE_SLOT = @slot;";
            using var comandoPartida = new SQLiteCommand(consultaPartida, conexion);
            comandoPartida.Parameters.AddWithValue("@slot", nombreSlot);
        }
    }
}
