using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;


namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;
        private static int CELL_SIZE = 15;
        int total_points = 0;
        int LASTROW = 40;
        
        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = keyboardService.GetDirection();
            robot.SetVelocity(velocity);  
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> gems = cast.GetActors("gems");
            List<Actor> rocks = cast.GetActors("rocks");

            

            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            //GEMS & ROCKS
            Random random = new Random();
            int x = random.Next(1, 60);
            Point postition = new Point(x,0);
            postition = postition.Scale(CELL_SIZE);

            int r = random.Next(0, 256);
            int g = random.Next(0, 256);
            int b = random.Next(0, 256);
            Color color = new Color(r, g, b);

            Point speed = new Point(0, 15);

            

            foreach (Actor actor in gems)
            {
                actor.SetVelocity(speed);
                actor.MoveNext(maxX,maxY);
                
                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                    Artifact artifact = (Artifact) actor;
                    artifact.SetPoints(100);
                    int points = artifact.GetPoints();
                    total_points = points + total_points;
                    banner.SetText($"score: {total_points}");
                    actor.SetPosition(postition);
                    actor.SetColor(color);
                }
                //else if (actor.GetPosition().Equals(LASTROW)){
                //    actor.SetPosition(postition);
                //    actor.SetColor(color);
                //}
            } 
            foreach (Actor rock in rocks)
            {
                
                rock.SetVelocity(speed);
                rock.MoveNext(maxX,maxY);
                if (robot.GetPosition().Equals(rock.GetPosition()))
                {
                    
                    Artifact artifact = (Artifact) rock;
                    artifact.SetPoints(-75);
                    int points = artifact.GetPoints();
                    total_points = points + total_points;
                    banner.SetText($"score: {total_points}");
                    artifact.SetPosition(postition);
                    artifact.SetColor(color);
                }
            } 
            
        }

        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }

    }
}