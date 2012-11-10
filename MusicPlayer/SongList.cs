using System.Collections.Generic;

namespace IrrBox
{
    class SongList
    {
        private List<Song> songList = new List<Song>();
        public int currentSong { get; set; }

        public void addSong(Song song)
        {
            songList.Add(song);
        }

        public void removeSong(Song song)
        {
            songList.Remove(song);
        }

        public Song getSong(int position)
        {
            return songList[position];
        }

        public int Count()
        {
            return songList.Count;
        }
    }
}
