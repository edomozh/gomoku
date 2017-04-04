namespace Gomoku
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;

	internal class Cursor : DrawableGameComponent
	{
		public SpriteBatch SpriteBatch;
		public Texture2D Texture;
		public Rectangle Rectangle;

		public Cursor(Gomoku game, string filename, int width, int height) : base(game)
		{
			SpriteBatch = game.SpriteBatch;
			Texture = game.Content.Load<Texture2D>(filename);
			Rectangle = new Rectangle(0, 0, width, height);
		}

		public override void Update(GameTime gameTime)
		{
			Rectangle.Location = Mouse.GetState().Position;
		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch.Draw(Texture, Rectangle, Color.White);
		}

	}
}
