using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;


namespace Client
{
    class Program
    {

        public static RenderWindow gameWindow;

        static void Main(string[] args)
        {
            gameWindow = new RenderWindow(new SFML.Window.VideoMode(800, 600), "Youtube 2D MMORPG");

            //Load Game Data.
            Graphic.LoadGameAssets();
            Map.CheckMaps();
            Map.LoadMaps();
            Console.WriteLine("Game has started.");
            Game();
        }

        static void Game()
        {

            while (gameWindow.IsOpen)
            {
                gameWindow.DispatchEvents();    //what is this?
                gameWindow.Clear(Color.Black);

                Graphic.Render_Graphics();

                gameWindow.Display();
            }
        }
    }
}
