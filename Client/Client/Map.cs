using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    class Map
    {
        public const int MAX_MAPS = 100;
        public const string MAP_PATH = "Data/Maps/";
        public const string MAP_EXP = ".map";

        public static MapStruct[] Maps = new MapStruct[MAX_MAPS];

        [Serializable]
        public struct TileDataStruct {
            public byte X;
            public byte Y;
            public byte Tileset;
            public byte AutoTile;

        }

        [Serializable]
        public struct TileStruct
        {
            public TileDataStruct[] Layer;
            public byte Type;
            public int Data1;
            public int Data2;
            public int Data3;
            public byte DirBlock;
        }

        [Serializable]
        public struct MapStruct
        {
            public string Name;
            public byte MaxX;
            public byte MaxY;

            public TileStruct[,] Tile;
        }

        [Serializable]
        public enum LayerType
        {
            Ground = 1,
            Mask,   //Draw House on the Ground and Ground is Disapear and House cover that region.
            Mask2,  //Another Mask ex) Draw Window on the House
            Fringe,
            Fringe2,
            Count
        }

        public static void CheckMaps()
        {
            Console.WriteLine("Checking Maps...");
            Array.Resize(ref Maps, MAX_MAPS);

            for(int i = 0; i < MAX_MAPS; i++)
            {
                if (!File.Exists(MAX_MAPS + "map" + i + MAP_EXP))
                {
                    ClearMap(i);
                    SaveMap(i);
                }
            }
        }

        public static void ClearMap(int mapnum)
        {
            int xx = 0;
            int yy = 0;

            Maps[mapnum].Name = "NoName";
            Maps[mapnum].MaxX = 10;
            Maps[mapnum].MaxY = 10;
            Maps[mapnum].Tile = new TileStruct[(Maps[mapnum].MaxX), (Maps[mapnum].MaxY)];

            var mapX = Maps[mapnum].Tile.GetLength(0);
            var mapY = Maps[mapnum].Tile.GetLength(1);

            for(int x = 0; x < Maps[mapnum].MaxX; x++)
            {
                mapX = x;
            }

            for(int y = 0; y < Maps[mapnum].MaxY; y++)
            {
                mapY = y;
            }

            for (xx = 0; xx < Maps[mapnum].Tile.GetLength(0); xx++)
            {
                for(yy = 0; yy < Maps[mapnum].Tile.GetLength(1); yy++)
                {
                    //Fill the array with the LayerType on each Layer.
                    Maps[mapnum].Tile[xx, yy].Layer = new TileDataStruct[(int)LayerType.Count - 1];
                    Array.Resize(ref Maps[mapnum].Tile[xx, yy].Layer, (int)LayerType.Count - 1);
                    //set Default tilesets each Layter
                    Maps[mapnum].Tile[xx, yy].Layer[1].Tileset = 2;
                }
            }
        }

        //How we save a file
        public static void SaveMap(int mapnum)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(MAP_PATH + "map" + mapnum + MAP_EXP, FileMode.OpenOrCreate);
            bf.Serialize(fs, Maps[mapnum]);
            fs.Close();
        }

        public static void LoadMap(int mapnum)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(MAP_PATH + "map" + mapnum + MAP_EXP, FileMode.OpenOrCreate);
            Maps[mapnum] = (MapStruct)bf.Deserialize(fs);
            fs.Close();
        }

        public static void LoadMaps()
        {
            Console.WriteLine("Loading Maps...");
            for(int i = 0; i < MAX_MAPS; i++)
            {
                LoadMap(i);
            }

            Console.WriteLine("Maps Loaded.");
        }
    }
}
