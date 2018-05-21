using SFML.Graphics;
using System;
using System.IO;

namespace Client
{
    class Graphic
    {
        public const string DATA_PATH = "Data/";
        public const string GRAPHIC_PATH = "Data/Graphics/";
        public const string FILE_EXT = ".png";

        public struct Rect
        {
            public int Top;
            public int Left;
            public int Right;
            public int Bottom;
        }

        public static Rect TileView;


        //Tileset Graphics
        static Sprite[] TilesetSprite;
        static int numTilesets;
   
        public static void LoadGameAssets()
        {
            CheckTilesets();
        }

        public static void Render_Graphics()
        {
           for(int x = TileView.Left; x <= TileView.Right; x++)
            {
                for(int y = TileView.Top; y <= TileView.Bottom; y++)
                {
                    //DrawMapTile(1, x, y);
                }
            }

            DrawMapGrid(); //for Test
        }

        public static void RenderSprite(Sprite tmpSprite, RenderWindow target, int destX,int destY, int sourceX, int sourceY,int sourcewidth, int sourceheight)
        {
            tmpSprite.TextureRect = new IntRect(sourceX, sourceY, sourcewidth, sourceheight);
            tmpSprite.Position = new SFML.System.Vector2f(destX, destY);
            target.Draw(tmpSprite);
        }

        static void DrawMapTile(int mapnum, int x, int y)
        {
            int i = 0;
            RectangleShape srcrect = new RectangleShape(new SFML.System.Vector2f(0, 0));
            srcrect.Size = new SFML.System.Vector2f(0, 0);

            TileView.Top = 0;
            TileView.Bottom = Map.Maps[1].MaxY - 1;
            TileView.Left = 0;
            TileView.Right = Map.Maps[1].MaxX - 1;

            for(i = (int)Map.LayerType.Ground; i <= (int)Map.LayerType.Mask2; i++)
            {
                if(Map.Maps[mapnum].Tile[x,y].Layer[i].Tileset > 0 & Map.Maps[mapnum].Tile[x,y].Layer[i].Tileset <= numTilesets)
                {
                    RenderSprite(TilesetSprite[1], Program.gameWindow, (x * 16), (y * 16), 0, 0, 16,(64 / 4));
                }
            }
        }

        static void DrawMapGrid()
        {
            RectangleShape rec = new RectangleShape();
            for(int x= TileView.Left; x <= TileView.Right+1; x++)
            {
                for(int y = TileView.Top; y <= TileView.Bottom+1; y++)
                {
                    rec.OutlineColor = new Color(Color.Red);
                    rec.OutlineThickness = 0.8f;
                    rec.FillColor = new Color(Color.Transparent);
                    rec.Size = new SFML.System.Vector2f((x * 16), (y * 16));
                    rec.Position = new SFML.System.Vector2f((TileView.Left * 16), (TileView.Top * 16));
                    Program.gameWindow.Draw(rec);
                }
            }
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
