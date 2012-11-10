using System.Collections.Generic;

namespace IrrBox
{
    /// <summary>
    /// Contains operation on a list of songs
    /// </summary>
    class SongList
    {
        private List<Song> songList = new List<Song>(); // the list of songs
        public int currentSong { get; set; }            // the position in the list of the currently playing song

        /// <summary>
        /// Adds a song to the list
        /// </summary>
        /// <param name="song"></param>
        public void addSong(Song song)
        {
            songList.Add(song);
        }

        /// <summary>
        /// Removes a song from the list
        /// </summary>
        /// <param name="song"></param>
        public void removeSong(Song song)
        {
            songList.Remove(song);
        }

        /// <summary>
        /// Gets a song from the list
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Song getSong(int position)
        {
            return songList[position];
        }

        /// <summary>
        /// Retrieve the number of songs in the list
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return songList.Count;
        }
    }
}
