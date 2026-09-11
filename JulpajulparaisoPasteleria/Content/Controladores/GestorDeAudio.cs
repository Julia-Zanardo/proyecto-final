using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Controladores
{
    public static class GestorDeAudio
    {
        private static SoundEffect sonidoClick;
        private static Song musicaFondo;
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
            }
        }
        public static void ReanudarMusica()
        {
            if (MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
            }
        }
    }
}
