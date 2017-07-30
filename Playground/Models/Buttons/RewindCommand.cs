﻿using Playground.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Playground.Models.Buttons
{
    class RewindCommand : ICommand
    {
        private MediaElement mediaElement;

        public RewindCommand(MediaElement mediaElement)
        {
            this.mediaElement = mediaElement;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Execute(ListBox Playlist)
        {
            if (Playlist.SelectedIndex > 0)
            {
                Playlist.SelectedIndex--;
            }
        }
    }
}
