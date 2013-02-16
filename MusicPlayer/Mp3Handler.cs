namespace IrrBox
{
    class Mp3Handler : Handler
    {
        /// <summary>
        /// Get the tag
        /// </summary>
        /// <param name="path"></param>
        public override void handleRequest(string path)
        {
            if (System.IO.Path.GetExtension(path) == ".mp3")
                System.IO.Path.GetExtension(path);
            else if (successor != null)
                successor.handleRequest(path);
        }
    }
}
