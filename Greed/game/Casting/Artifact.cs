namespace Unit04.Game.Casting
{
    // TODO: Implement the Artifact class here

    // 1) Add the class declaration. Use the following class comment. Make sure you
    //    inherit from the Actor class.
    public class Artifact : Actor
    {
        private int points = 0;
        private string message = "";

        /// <summary>
        /// <para>An item of cultural or historical interest.</para>
        /// <para>
        /// The responsibility of an Artifact is to provide a message about itself.
        /// </para>
        /// </summary>


    // 2) Create the class constructor. Use the following method comment.
        public Artifact(){}

        /// <summary>
        /// Constructs a new instance of Artifact.
        /// </summary>
       

    // 3) Create the GetMessage() method. Use the following method comment.
        public int GetPoints(){
            return points;
        }
        /// <summary>
        /// Gets the artifact's message.
        /// </summary>
        /// <returns>The message as a string.</returns>
        public string GetMessage(){
            return message;
        }

    // 4) Create the SetMessage(string message) method. Use the following method comment.
        //public void SetPoints(int points)
        //{
        //    this.points = points;
        //}

        public void SetMessage(string message)
        {
            this.message = message;
        }
        /// <summary>
        /// Sets the artifact's message to the given value.
        /// </summary>
        /// <param name="message">The given message.</param>
        public void SetPoints(int points)
        {
            if (points == 0)
            {
                throw new ArgumentException("points can't be 0 with collision");
            }
            this.points = points;
        }
        public List<Artifact> CopyArtifact(List<Artifact> artifact)
        {
            List<Artifact> artifact1 = artifact;
            return artifact1;
        }         
    }

    
}