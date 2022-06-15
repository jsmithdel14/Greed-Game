using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unit04.Game.Casting;
using Unit04.Game.Directing;
using Unit04.Game.Services;


namespace Unit04
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int MIN_Y = -15;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Greed";
        //private static string DATA_PATH = "Data/messages.txt";
        private static Color WHITE = new Color(255, 255, 255);
        Random new_r = new Random();
        private static Random DEFAULT_ARTIFACTS = new Random();


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {

            // create the cast
            Cast cast = new Cast();

            // create the banner
            Actor banner = new Actor();
            banner.SetText("");
            banner.SetFontSize(FONT_SIZE);
            banner.SetColor(WHITE);
            banner.SetPosition(new Point(CELL_SIZE, 0));
            cast.AddActor("banner", banner);

            
            // create the robot
            Actor robot = new Actor();
            robot.SetText("#");
            robot.SetFontSize(FONT_SIZE);
            robot.SetColor(WHITE);
            robot.SetPosition(new Point(MAX_X / 2, MIN_Y));
            cast.AddActor("robot", robot);

            // load the messages
            //List<string> messages = File.ReadAllLines(DATA_PATH).ToList<string>();

            // create the artifacts

            Random random = new Random();
            for (int i = 0; i < DEFAULT_ARTIFACTS.Next(20,50); i++)
            {
                //string text = ((char)random.Next(33, 126)).ToString();
                //string message = messages[i];
                int x = random.Next(1, COLS);
                int x2 = random.Next(1, COLS);
                int y = random.Next(-40, 0);
                Point position = new Point(x, y);
                position = position.Scale(CELL_SIZE);
                Point position2 = new Point(x2, y);
                position2 = position2.Scale(CELL_SIZE);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);


                Artifact gem = new Artifact();
                gem.SetText("*");
                gem.SetFontSize(FONT_SIZE);
                gem.SetColor(color);
                gem.SetPosition(position);
                cast.AddActor("gems", gem);


                Artifact rock = new Artifact();
                rock.SetText("o");
                rock.SetFontSize(FONT_SIZE);
                rock.SetColor(color);
                rock.SetPosition(position2);
                cast.AddActor("rocks", rock);
            }

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);

            // test comment
        }
    }
}
