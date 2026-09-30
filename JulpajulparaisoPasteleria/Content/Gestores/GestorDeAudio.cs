using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Gestores
{
    public static class GestorDeAudio
    {
        private static SoundEffect sonidoClick;
        private static Song musicaFondo;
        public static bool musicaSonando = true;
        public static void LoadContent(ContentManager content) 
        {
            sonidoClick = content.Load<SoundEffect>("Sonidos/sonidoClick");
            musicaFondo = content.Load<Song>("Sonidos/musicaFondo");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.4f;
        }
        public static void ReproducirClick() 
        {
            sonidoClick.Play();
        }
        public static void ReproducirMusicaFondo()
        {
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(musicaFondo);
            }
        }
        public static void PausarMusica()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
                musicaSonando = false;
            }
        }
        public static void ReanudarMusica()
        {
            if (MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
                musicaSonando = true;
            }
        }
    }
}
