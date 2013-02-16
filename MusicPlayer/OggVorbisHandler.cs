using System;

namespace IrrBox
{
    class OggVorbisHandler : Handler
    {
        private string artist;
        private string album;
        private string title;
        private string track;
        private object picture;

        /// <summary>
        /// Get the tag
        /// </summary>
        /// <param name="path"></param>
        public override void handleRequest(string path)
        {
            try
            {
                if (System.IO.Path.GetExtension(path) == ".flac")
                    readTagsFlac(path);
                else if (System.IO.Path.GetExtension(path) == ".ogg")
                    readTagsOgg(path);
                else if (successor != null)
                    successor.handleRequest(path);
                else
                    Console.WriteLine("Could not determine filetype.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not read tag.");
            }
        }

        /// <summary>
        /// Get tag of flac file.
        /// </summary>
        /// <param name="path"></param>
        private void readTagsFlac(string path)
        {
            Luminescence.Xiph.FlacTagger flacTag = new Luminescence.Xiph.FlacTagger(path);
            artist = flacTag.Artist;
            album = flacTag.Album;
            title = flacTag.Title;
            track = flacTag.TrackNumber;
            picture = flacTag.Arts[0].Picture;
        }

        /// <summary>
        /// Get tag of ogg file.
        /// </summary>
        /// <param name="path"></param>
        private void readTagsOgg(string path)
        {
            Luminescence.Xiph.OggTagger oggTag = new Luminescence.Xiph.OggTagger(path);
            artist = oggTag.Artist;
            album = oggTag.Album;
            title = oggTag.Title;
            track = oggTag.TrackNumber;
            picture = oggTag.FlacArts[0].Picture;
        }


        /// <summary>
        /// Get Artist
        /// </summary>
        /// <returns></returns>
        public string getArtist()
        {
            return artist;
        }

        /// <summary>
        /// Get Album
        /// </summary>
        /// <returns></returns>
        public string getAlbum()
        {
            return album;
        }

        /// <summary>
        /// Get Title
        /// </summary>
        /// <returns></returns>
        public string getTitle()
        {
            return title;
        }

        /// <summary>
        /// Get Track
        /// </summary>
        /// <returns></returns>
        public string getTrack()
        {
            return track;
        }

        /// <summary>
        /// Get Picture
        /// </summary>
        /// <returns></returns>
        public object getPicture()
        {
            return picture;
        }
    }
}
