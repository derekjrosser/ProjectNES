using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectNES
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Global Variables
        private Texture2D spritesheet;
        private SpriteFont font;
        private LevelData levelone;
        private Player player;
        private Texture2D mapBorder;
        public Texture2D playerTex;
        int debug;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            levelone = new LevelData(spriteBatch, spritesheet);
            player = new Player();

            // create basic textures
            mapBorder = Pixel(Color.White);
            playerTex = Pixel(Color.Red);

            // stuff I wish I didnt need to transfer/reference
            player.playerTex = playerTex;
            player._s = spriteBatch;

        }

        // custom func
        public Texture2D Pixel (Color c)
        {
            Texture2D tex = new Texture2D(graphics.GraphicsDevice, 1, 1);
            tex.SetData(new Color[] { c });
            return tex;
            /*Color[] data = new Color[(levelone.levelsize.X * levelone.levelsize.Y) * 8];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.White;
                mapBorder.SetData(data);
            }*/
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spritesheet = Content.Load<Texture2D>("spritesheet2");
            font = Content.Load<SpriteFont>("FontA");
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            // Close Game
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
            }

            // Map Editor
            {
                // paint tiles
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    levelone.PlaceTile(Mouse.GetState().Position, levelone._brush);
                }
                // remove tile
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    levelone.PlaceTile(Mouse.GetState().Position, 0);
                }
                // Change tile
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    levelone._brush--;
                }
                // Change tile
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    levelone._brush++;
                }
                // Save
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    levelone.Save();
                }
                // Load
                if (Keyboard.GetState().IsKeyDown(Keys.L))
                {
                    levelone.Load();
                }
            }

            // Player Controls
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.position.X -= player.movespeed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.position.X += player.movespeed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    player.position.Y -= 20;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    player.position.Y += player.movespeed;
                }

                // Gravity
                int floor = levelone.GetTile(player.position.X / 8, player.position.Y / 8);
                if (floor == 0)
                {
                    if (player.gravity < player.gravityMax)
                    {
                        player.gravity += 0.1f;
                    }
                }
                else
                {
                    player.gravity = 0;
                }
                player.position.Y += (int)player.gravity;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(samplerState: SamplerState.PointWrap);
            // Begin Draw
            {

                // draw map border
                spriteBatch.Draw(mapBorder, new Rectangle(0, 0, levelone.levelsize.X * 8, levelone.levelsize.Y * 8), Color.White);

                // draw map
                levelone.Draw();

                // player
                player.Draw();

                // Debug Text
                {
                    string m1 = string.Format("x{0} / y{1}", Mouse.GetState().Position.X / 8, Mouse.GetState().Position.Y / 8);
                    spriteBatch.DrawString(font, m1, new Vector2(10, 400), Color.White);

                    string m2 = string.Format("expected : {0}", (Mouse.GetState().Position.X / 8) + (Mouse.GetState().Position.Y / 8) * 10);
                    spriteBatch.DrawString(font, m2, new Vector2(10, 380), Color.White);

                    string m3 = string.Format("actual : {0}", debug);
                    spriteBatch.DrawString(font, m3, new Vector2(10, 360), Color.White);

                    string m4 = string.Format("mouse : x{0} / y{1}", Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
                    spriteBatch.DrawString(font, m4, new Vector2(10, 340), Color.White);

                    //string get_tile = levelone.GetTile(0, 0).ToString();
                    string get_tile = levelone.GetTile(player.position.X / 8, player.position.Y / 8).ToString();
                    spriteBatch.DrawString(font, string.Format("tile: {0}",get_tile), new Vector2(10, 320), Color.White);
                }

            }
            // End Draw
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
