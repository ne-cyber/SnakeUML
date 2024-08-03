using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SnakeUML
{
    public static class SoundOutput
    {
        static MediaPlayer player = new MediaPlayer();
        static MediaPlayer playerFaster = new MediaPlayer();

        static SoundOutput()
        {
            Inbitialize();
        }

        static void Inbitialize()
        {
            string path = Path.GetFullPath(".");
            path = path.Replace("\\", "/");
            player.Open(new Uri("file:///" + path + "/../../music.mp3"));
            playerFaster.Open(new Uri("file:///" + path + "/../../music.mp3"));

            player.Play();
            player.Stop();
            playerFaster.Play();
            playerFaster.Stop();

            player.SpeedRatio = 1;
            playerFaster.SpeedRatio = 1.4;

            isNormalSpeed = true;
        }

        static bool isNormalSpeed;
        
        public static void Start()
        {
            player.Play();
            isNormalSpeed = true;
        }

        public static void Fast()
        {
            if(isNormalSpeed)
            {
                playerFaster.Position = player.Position;
                playerFaster.Play();
                player.Stop();

                isNormalSpeed = false;
            }
        }

        public static void Normal()
        {
            if(!isNormalSpeed)
            {
                player.Position = playerFaster.Position;
                player.Play();
                playerFaster.Stop();

                isNormalSpeed = true;
            }
        }

        public static void CheckLoopPlay()
        {
            if(isNormalSpeed && player.Position == player.NaturalDuration)
            {
                player.Stop();
                player.Position = new TimeSpan(0);
                player.Play();
            }

            if (!isNormalSpeed && playerFaster.Position == playerFaster.NaturalDuration)
            {
                playerFaster.Stop();
                playerFaster.Position = new TimeSpan(0);
                playerFaster.Play();
            }
        }
    }

}