namespace Gomoku
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;

	public class Gomoku : Game
	{
		public GraphicsDeviceManager Graphics;
		public SpriteBatch SpriteBatch;
		public GameState State;

		private Cursor _cursor;
		private Board _board;
		private IBot _bot;

		private readonly int _dimension;
		private readonly int _cellsize;

		public Gomoku(int dimension = 15, int cellsize = 30)
		{
			_dimension = dimension;
			_cellsize = cellsize;
			var formSize = (_cellsize + 1) * _dimension + 1;
			Graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = formSize,
				PreferredBackBufferHeight = formSize
			};
			Content.RootDirectory = "Content";
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			_cursor = new Cursor(this, "cursor", 20, 20);
			_board = new Board(this, _dimension, _cellsize, 1);
			_board.OneMoreStep += CheckForWin;
			_bot = new Bot1(this, _board);
			NewGame();
		}

		private void NewGame()
		{
			_board.Clear();
			State = GameState.Continues;
			_bot.MakeMove();
		}

		private void CheckForWin(Point pos, CellValue value)
		{
			if (_board.IsConnectAmount(pos, value, 5))
			{
				if (value == CellValue.Bot) State = GameState.BotWins;
				else if (value == CellValue.User) State = GameState.UserWins;
			}
			else if (_board.IsFull) State = GameState.Standoff;
			else State = GameState.Continues;

			switch (State)
			{
				case GameState.Continues: Window.Title = "Play"; break;
				case GameState.UserWins: Window.Title = "User wins: right click for restart."; break;
				case GameState.BotWins: Window.Title = "Bot wins: right click for restart."; break;
				case GameState.Standoff: Window.Title = "Standoff: right click for restart."; break;
			}
		}

		protected override void Update(GameTime gameTime)
		{
			if (Mouse.GetState().RightButton == ButtonState.Pressed) NewGame();

			if (State == GameState.Continues) _board.Update(gameTime);
			_cursor.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			SpriteBatch.Begin();

			_board.Draw(gameTime);
			_cursor.Draw(gameTime);

			SpriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
