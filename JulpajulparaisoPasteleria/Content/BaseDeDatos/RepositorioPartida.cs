using JulpajulparaisoPasteleria.Content.Gestores;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace JulpajulparaisoPasteleria.Content.BaseDeDatos
{
    public static class RepositorioPartida
    {
        public const int CantidadMaximaSlots = 4;

        public static string NombreSlot(int numero)
        {
            return $"Partida {numero}";
        }

        public static int NumeroDeSlot(string nombre)
        {
            return int.Parse(nombre.Replace("Partida ", "").Trim());
        }

        
        public static void GuardarProgresoCompleto(int numeroSlot)
        {
            string nombreSlot = NombreSlot(numeroSlot);
            using var conexion = GestorDeBaseDeDatos.ObtenerConexion();
            using var transaccion = conexion.BeginTransaction();

            try
            {
                string consultaPartida = @"
                    INSERT INTO PARTIDAS (SLOT, TIEMPO_ENTRE_CLIENTES, CANTIDAD_CLIENTES, DIA_ACTUAL)
                    VALUES (@slot, @tiempo, @cantClientes, @diaActual)
                    ON CONFLICT(SLOT) DO UPDATE SET
                        TIEMPO_ENTRE_CLIENTES = excluded.TIEMPO_ENTRE_CLIENTES,
                        CANTIDAD_CLIENTES     = excluded.CANTIDAD_CLIENTES,
                        DIA_ACTUAL            = excluded.DIA_ACTUAL;";

                using (var cmd = new SQLiteCommand(consultaPartida, conexion, transaccion))
                {
                    cmd.Parameters.AddWithValue("@slot", nombreSlot);
                    cmd.Parameters.AddWithValue("@tiempo", GestorDeJuego.TiempoEntreClientes);
                    cmd.Parameters.AddWithValue("@cantClientes", GestorDeJuego.CantidadDeClientesPorDia);
                    cmd.Parameters.AddWithValue("@diaActual", GestorDeJuego.DiaActual);
                    cmd.ExecuteNonQuery();
                }

                string consultaDia = @"
                    INSERT INTO HISTORIAL_DIAS (SLOT, PUNTUACION, CANTIDAD_CLIENTES_SATISFECHOS, CANTIDAD_CLIENTES_ENOJADOS)
                    VALUES (@slot, @puntuacion, @satisfechos, @enojados);";

                foreach (var dia in GestorDeJuego.DiasPendientesDeGuardado)
                {
                    using var cmd = new SQLiteCommand(consultaDia, conexion, transaccion);
                    cmd.Parameters.AddWithValue("@slot", nombreSlot);
                    cmd.Parameters.AddWithValue("@puntuacion", dia.Puntuacion);
                    cmd.Parameters.AddWithValue("@satisfechos", dia.Satisfechos);
                    cmd.Parameters.AddWithValue("@enojados", dia.Enojados);
                    cmd.ExecuteNonQuery();
                }

                transaccion.Commit();
                GestorDeJuego.DiasPendientesDeGuardado.Clear();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                throw new Exception("Error al guardar: " + ex.Message, ex);
            }
        }

        public static void CargarPartidaEnJuego(int numeroSlot)
        {
            using var conexion = GestorDeBaseDeDatos.ObtenerConexion();
            using var comando = new SQLiteCommand(@"
                SELECT TIEMPO_ENTRE_CLIENTES, CANTIDAD_CLIENTES, DIA_ACTUAL
                FROM PARTIDAS
                WHERE SLOT = @slot;", conexion);
            comando.Parameters.AddWithValue("@slot", NombreSlot(numeroSlot));

            using var lector = comando.ExecuteReader();
            if (lector.Read())
            {
                GestorDeJuego.DiaActual = Convert.ToInt32(lector["DIA_ACTUAL"]);
                GestorDeJuego.TiempoEntreClientes = Convert.ToSingle(lector["TIEMPO_ENTRE_CLIENTES"]);
                GestorDeJuego.CantidadDeClientesPorDia = Convert.ToInt32(lector["CANTIDAD_CLIENTES"]);
                GestorDeJuego.SlotActual = numeroSlot;
                GestorDeJuego.DiasPendientesDeGuardado.Clear();
            }
        }

        public static List<DatosResumenPartida> ObtenerTodasLasPartidas()
        {
            var lista = new List<DatosResumenPartida>();

            using var conexion = GestorDeBaseDeDatos.ObtenerConexion();
            using var comando = new SQLiteCommand(
                "SELECT SLOT, DIA_ACTUAL FROM PARTIDAS ORDER BY SLOT ASC;", conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new DatosResumenPartida
                {
                    NombreJugador = lector["SLOT"].ToString(),
                    DiaActual = Convert.ToInt32(lector["DIA_ACTUAL"]),
                });
            }
            return lista;
        }
        private static List<string> ObtenerSlotsPorAntiguedad()
        {
            var nombres = new List<string>();

            using var conexion = GestorDeBaseDeDatos.ObtenerConexion();
            using var comando = new SQLiteCommand(
                "SELECT SLOT FROM PARTIDAS ORDER BY FECHA_CREACION ASC;", conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                nombres.Add(lector["SLOT"].ToString());
            }
            return nombres;
        }

        public static void BorrarPartidaPorSlot(string nombreSlot)
        {
            using var conexion = GestorDeBaseDeDatos.ObtenerConexion();
            using var comando = new SQLiteCommand("DELETE FROM PARTIDAS WHERE SLOT = @slot;", conexion);
            comando.Parameters.AddWithValue("@slot", nombreSlot);
            comando.ExecuteNonQuery();
        }

        public static int ObtenerNumeroDeSlotParaNuevaPartida()
        {
            List<string> ocupados = ObtenerSlotsPorAntiguedad();
            var usados = new HashSet<string>(ocupados);

            if (ocupados.Count >= CantidadMaximaSlots)
            {
                string masAntiguo = ocupados[0];
                BorrarPartidaPorSlot(masAntiguo);
                usados.Remove(masAntiguo);
            }

            for (int numero = 1; numero <= CantidadMaximaSlots; numero++)
            {
                if (!usados.Contains(NombreSlot(numero)))
                {
                    return numero;
                }
            }

            throw new InvalidOperationException("No se encontró un slot libre.");
        }
    }
}