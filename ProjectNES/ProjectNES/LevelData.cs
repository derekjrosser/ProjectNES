using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
//using MetaTile;

namespace ProjectNES
{
    public class LevelData
    {
        int[] _tiles; // reduce tile size by metasprites
        const int _TileSize = 8;
        public Point levelsize = new Point(40,25);
        private SpriteBatch _s;
        private Texture2D _t;
        // fun painting
        public int _brush = 1; // test

        public LevelData (SpriteBatch s, Texture2D t)
        {
            _s = s;
            _t = t;

            _tiles = new int[levelsize.X * levelsize.Y];


            // Create Garbage
            /*
            Random r = new Random();
            for (int i = 0; i < _tiles.Length; i++)
                _tiles[i] = r.Next(1,10);
                */
        }

        public void Draw()
        {
            // draw level
            int x, y;
            for (int i = 0; i < _tiles.Length; i++)
            {
                y = i / levelsize.X;
                x = i - y * levelsize.X;

                if (_tiles[i] != 0)
                    DrawMini(_tiles[i], x * _TileSize, y * _TileSize);
            }

            // draw meta tiles for testing
            //DrawMetaMeta(MetaTile.metameta[0], 300, 200);
            //DrawMetaMeta(MetaTile.metameta[1], 400, 200);

            // draw brush
            DrawMini(_brush, 400,10);
        }

        public void DrawMeta(int[] meta, int x, int y)
        {
            DrawMini(meta[0], (0 * _TileSize) + x, (0 * _TileSize) + y);
            DrawMini(meta[1], (1 * _TileSize) + x, (0 * _TileSize) + y);
            DrawMini(meta[2], (0 * _TileSize) + x, (1 * _TileSize) + y);
            DrawMini(meta[3], (1 * _TileSize) + x, (1 * _TileSize) + y);
        }

        // kinda repeats drawmeta?
        public void DrawMetaMeta(int[] meta, int x, int y)
        {
            DrawMeta(MetaTile.meta[meta[0]], x, y);
            DrawMeta(MetaTile.meta[meta[1]], x + 16, y);
            DrawMeta(MetaTile.meta[meta[2]], x, y + 16);
            DrawMeta(MetaTile.meta[meta[3]], x + 16, y + 16);
        }

        // note, designed to locate at 
        public void DrawMini(int id, int x, int y)
        {
            // mini is 8x8 NES style
            Point p = IDToCoords(id, 4); // 4x4 tilesheet
            _s.Draw(_t, new Rectangle(x, y, _TileSize, _TileSize), new Rectangle(p.X * _TileSize, p.Y * _TileSize, _TileSize, _TileSize), Color.White);
        }

        // id to texture coords?
        public Point IDToCoords(int ID, int size)
        {
            // spritesheet is 4x4 tiles
            ID = ID - 1;
            int y = ID / size;
            int x = ID - y * size;
            return new Point(x, y);
        }

        // mouse coords to arrayid
        public int CoordsToID(Point p, int size)
        {
            // spritesheet is 4x4 tiles
            int ID;
            ID = p.Y * size; // was 4
            ID = ID + p.X;
            return ID;
        }

        // would be faster if was multidimensional array
        public int GetTile(int x, int y)
        {
            int index = CoordsToID(new Point(x, y), levelsize.X);
            if (index >= 0 && index < _tiles.Length)
                return _tiles[index];
            return -1;
        }

        public bool InsideRect(Point p, Rectangle r)
        {
            return (p.X >= r.X && p.Y >= r.Y && p.X < r.Width && p.Y < r.Height);
        }

        public void PlaceTile(Point p, int tile)
        {
            if (!InsideRect(p, new Rectangle(0, 0, levelsize.X * _TileSize, levelsize.Y * _TileSize)))
                return;
            p.X = p.X / _TileSize;
            p.Y = p.Y / _TileSize;
            _tiles[CoordsToID(p, levelsize.X)] = tile;
        }

        public void Save()
        {
            // Create Directory
            string directory = "maps";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Save File
            using (BinaryWriter writer = new BinaryWriter(File.Open("maps/levelone.map", FileMode.Create)))
            {
                for (int i = 0; i < _tiles.Length; i++)
                    writer.Write(_tiles[i]);
                writer.Close();
            }
        }

        public void Load()
        {
            // Create Directory
            string directory = "maps";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Save File
            if (File.Exists("maps/levelone.map"))
            {
                using (BinaryReader reader = new BinaryReader(File.OpenRead("maps/levelone.map")))
                {
                    for (int i = 0; i < _tiles.Length; i++)
                        _tiles[i] = reader.ReadInt32();
                    reader.Close();
                }
            }
        }
    }
}
