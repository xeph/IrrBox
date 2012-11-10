namespace IrrBox
{
    /// <summary>
    /// Contains information about a song
    /// </summary>
    class Song
    {
        public string path     { get; set; } // path of the song
        public string title    { get; set; } // song title
        public string artist   { get; set; } // name of the artist
        public string album    { get; set; } // album title
        public string track    { get; set; } // song number
        public string albumArt { get; set; } // path of the album art
    }
}
