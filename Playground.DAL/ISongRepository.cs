﻿using Playground.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.DAL
{
    public interface ISongRepository
    {
        Song GetSong(int songId);
        List<Song> GetAllSongs();
        void DeleteSong(Song song);
    }
}