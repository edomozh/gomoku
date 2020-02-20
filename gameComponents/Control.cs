namespace Gomoku
{
	using System;

	internal class Control : DrawableGameComponent
	{

		public event Action<Control> Click;
		public event Action<Control> Leave;
		public event Action<Control> Enter;

		private bool _hower;
		internal bool Hower
		{
			get { return _hower; }
			private set
			{
				if (!_hower && value) Enter?.Invoke(this);
				if (_hower && !value) Leave?.Invoke(this);
				_hower = value;
			}
		}

		private bool _pressed;
		internal bool Pressed
		{
			get { return _pressed; }
			private set
			{
				if (!_pressed && value) Click?.Invoke(this);
				_pressed = value;
			}
		}

		public Rectangle Rect { get; set; }
		public Texture2D Texture { get; set; }
		public SpriteBatch SpriteBatch { get; set; }
		public Color Color { get; set; }

		public Control(Gomoku game, Rectangle rectangle) : base(game)
		{
			SpriteBatch = game.SpriteBatch;
			Texture = new Texture2D(game.GraphicsDevice, 1, 1);
			Texture.SetData(new[] { Color.White });
			Rect = rectangle;
			Color = Color.Transparent;
		}

		public override void Update(GameTime gameTime)
		{
			var state = Mouse.GetState();
			if (Hower = Rect.Contains(state.Position))
			{
				if (state.LeftButton == ButtonState.Pressed) Pressed = true;
				if (state.LeftButton == ButtonState.Released) Pressed = false;
			}
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch.Draw(Texture, Rect, Color);
			base.Draw(gameTime);
		}
	}
}
