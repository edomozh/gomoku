using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gomoku
{
	public class Gomoku : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public Gomoku()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void UnloadContent() { }

		protected override void Update(GameTime gameTime)
		{
			var state = Keyboard.GetState();
			if (state.IsKeyDown(Keys.Escape)) Exit();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();


			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
