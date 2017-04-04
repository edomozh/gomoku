namespace Gomoku
{
	using System;
	using System.Linq;
	using Microsoft.Xna.Framework;

	internal class Board : DrawableGameComponent
	{
		public event Action<Point, CellValue> OneMoreStep;

		private readonly Cell[,] _cells;

		internal int Dimension => _cells.GetLength(0);
		internal bool IsEmpty => _cells.Cast<Cell>().All(cell => cell.Value == CellValue.None);
		internal bool IsFull => _cells.Cast<Cell>().All(cell => cell.Value != CellValue.None);
		internal bool InBourd(Point pos) => pos.X >= 0 && pos.X < _cells.GetLength(0) && pos.Y >= 0 && pos.Y < _cells.GetLength(1);
		internal bool IsNone(Point pos) => _cells[pos.X, pos.Y].Value == CellValue.None;
		internal CellValue GetValue(Point pos) => _cells[pos.X, pos.Y].Value;

		public Board(Gomoku game, int dimension, int cellsize, int border) : base(game)
		{
			_cells = new Cell[dimension, dimension];
			var sellSpace = cellsize + border;
			for (var y = 0; y < dimension; y++)
				for (var x = 0; x < dimension; x++)
					_cells[x, y] = new Cell(game,
						new Rectangle(
							new Point(x * sellSpace + border, y * sellSpace + border),
							new Point(cellsize, cellsize)));

			foreach (var cell in _cells) cell.Click += control => MakeMove(GetPosition((Cell)control), CellValue.User);
		}

		public void MakeMove(Point pos, CellValue value)
		{
			if (!IsNone(pos)) return;
			_cells[pos.X, pos.Y].Value = value;
			OneMoreStep?.Invoke(pos, value);
		}

		private Point GetPosition(Cell cell)
		{
			for (var y = 0; y < Dimension; y++)
				for (var x = 0; x < Dimension; x++)
					if (_cells[x, y] == cell) return new Point(x, y);
			throw new ArgumentOutOfRangeException();
		}

		public bool IsConnectAmount(Point pos, CellValue val, int numb)
		{
			return CheckAmount(pos, val, Direction.Down, Direction.Up, numb)
				|| CheckAmount(pos, val, Direction.Right, Direction.Left, numb)
				|| CheckAmount(pos, val, Direction.DownLeft, Direction.UpRight, numb)
				|| CheckAmount(pos, val, Direction.LeftUp, Direction.RightDown, numb);
		}

		private bool CheckAmount(Point pos, CellValue value, Point dir1, Point dir2, int numb)
		{
			var count = 1;
			Point check;
			for (var i = 1; i < numb; i++)
			{
				check = pos + dir1 * new Point(i);
				if (!InBourd(check)) break;
				if (_cells[check.X, check.Y].Value == value) count++;
				else break;
			}

			for (var i = 1; i < numb; i++)
			{
				check = pos + dir2 * new Point(i);
				if (!InBourd(check)) break;
				if (_cells[check.X, check.Y].Value == value) count++;
				else break;
			}
			return count > numb - 1;
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var cell in _cells) cell.Update(gameTime);
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			foreach (var cell in _cells) cell.Draw(gameTime);
			base.Draw(gameTime);
		}

	}
}
