namespace IrrBox
{
    abstract class Handler
    {
        protected Handler successor;

        /// <summary>
        /// Set the next handler of the current handler
        /// </summary>
        /// <param name="successor"></param>
        public void SetSuccessor(Handler successor)
        {
            this.successor = successor;
        }

        /// <summary>
        /// Get the tag
        /// </summary>
        /// <param name="path"></param>
        public abstract void handleRequest(string path);
    }
}
