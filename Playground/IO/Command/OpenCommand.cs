﻿namespace Playground.IO.Command
{
    using System;
    using System.Text;
    using System.Windows.Controls;
    using Microsoft.Win32;
    using Microsoft.WindowsAPICodePack.Shell;
    using Playground.Core;
    using Playground.Enums;

    public class OpenCommand : Command
    {
        public OpenCommand(MediaElement mediaElement, ListBox playList)
            : base(mediaElement, playList)
        {
        }

        public override void Execute()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.DefaultExt = ".mp3";
            dlg.Filter = this.AudioFormater();
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string[] filename = dlg.FileNames;
                foreach (var songPath in filename)
                {
                    ShellFile songFile = ShellFile.FromFilePath(songPath);
                    int extensionDotIndex = songFile.Name.LastIndexOf('.');
                    string songName = songFile.Name.Substring(0, extensionDotIndex);
                    string extName = songFile.Name.Substring(extensionDotIndex + 1).ToUpper();
                    if (!Enum.IsDefined(typeof(AudioFormats), extName))
                    {
                        throw new ArgumentException();
                    }
                    // System.Media.Duration is in 100nS (hundreds of nanoseconds) => 1sec = 10 000 000 100nS
                    int songDuraitonSeconds = (int)(songFile.Properties.System.Media.Duration.Value / 10000000);

                    TimeSpan songDuration = TimeSpan.FromSeconds(songDuraitonSeconds);

                    Song song = new Song(songName, songDuration, songPath);

                    PlayList.Items.Add(song);
                }
            }
        }

        private string AudioFormater()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("All Supported Audio | ");
            foreach (AudioFormats type in Enum.GetValues(typeof(AudioFormats)))
            {
                sb.Append($"*.{type}; ");
            }

            foreach (AudioFormats type in Enum.GetValues(typeof(AudioFormats)))
            {
                sb.Append($"|{type}s |*.{type}");
            }

            return sb.ToString().Trim();
        }
    }
}