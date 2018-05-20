using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using System.IO;

namespace Client
{
    class Graphic
    {
        public const string DATA_PATH = "Data/";
        public const string GRAPHIC_PATH = "Data/Graphics/";
        public const string FILE_EXT = ".png";

        //Tileset Graphics
        static Sprite[] TilesetSprite;
        static int numTilesets;
   
        public static void LoadGameAssets()
        {
            CheckTilesets();
        }

        public static void Render_Graphics()
        {
            DrawPicture();
        }

        public static void RenderSprite(Sprite tmpSprite, RenderWindow target, int destX,int destY, int sourceX, int sourceY,int sourcewidth, int sourceheight)
        {
            tmpSprite.TextureRect = new IntRect(sourceX, sourceY, sourcewidth, sourceheight);
            tmpSprite.Position = new SFML.System.Vector2f(destX, destY);
            target.Draw(tmpSprite);
        }

        public static void DrawPicture()
        {
            RenderSprite(TilesetSprite[1], Program.gameWindow, (1 * 64), (1 * 64), 0, 0, 64, (256 / 4));
        }

        static void CheckTilesets()
        {
            Console.WriteLine("Checking Tilesets...");
            numTilesets = 1;
            while (File.Exists(GRAPHIC_PATH + "Tilesets/" + numTilesets + FILE_EXT)){
                numTilesets += 1;
            }

            Array.Resize(ref TilesetSprite, numTilesets);
            
            for(int i = 1; i<numTilesets; i++)
            {
                TilesetSprite[i] = new Sprite(new Texture(GRAPHIC_PATH + "Tilesets/" + i + FILE_EXT));
            }

            Console.WriteLine("Loaded Tilesets");
        }
    }
}
