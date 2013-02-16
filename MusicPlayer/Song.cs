namespace IrrBox
{
    /// <summary>
    /// Contains information about a song
    /// </summary>
    class Song
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path"></param>
        public Song(string path)
        {
            this.path = path;

            Handler mp3 = new Mp3Handler();
            Handler oggVorbis = new OggVorbisHandler();
            mp3.SetSuccessor(oggVorbis);
            
            mp3.handleRequest(path);
        }

        public string path     { get; set; } // path of the song
        public string title    { get; set; } // song title
        public string artist   { get; set; } // name of the artist
        public string album    { get; set; } // album title
        public string track    { get; set; } // song number
        public string albumArt { get; set; } // path of the album art
    }
}
