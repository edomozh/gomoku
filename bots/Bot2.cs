namespace Gomoku.Bots
{
	using System;
	using System.Linq;
	using Microsoft.Xna.Framework;

	internal class Bot2 : IBot
	{
		private readonly Random _rnd;
		private readonly Board _board;
		private readonly Gomoku _game;

		private bool InRange(Point pos, CellValue[,] map)
			=> pos.X > 0 && pos.Y > 0 && pos.X < map.GetLength(0) && pos.Y < map.GetLength(1);

		public Bot2(Gomoku game, Board board)
		{
			_game = game;
			_board = board;
			_rnd = new Random();
			_board.OneMoreStep += (p, v) => { if (v == CellValue.User) MakeMove(); };
		}

		public void MakeMove()
		{
			if (_game.State != GameState.Continues) return;

			var pos = new Point();
			if (_board.IsEmpty)
			{
				pos.X = _rnd.Next((int)(1f / 3 * _board.Dimension), (int)(2f / 3 * _board.Dimension));
				pos.Y = _rnd.Next((int)(1f / 3 * _board.Dimension), (int)(2f / 3 * _board.Dimension));
			}
			else
			{
				pos = GetBestMove(_board.GetMap());
			}
			_board.MakeMove(pos, CellValue.Bot);
		}

		private Point GetBestMove(CellValue[,] map)
		{
			var max = 0;
			var resp = new Point();
			for (var y = 0; y < map.GetLength(0); y++)
				for (var x = 0; x < map.GetLength(1); x++)
				{
					var pos = new Point(x, y);
					if (map[x, y] != CellValue.None) continue;
					var buf = GetWeight(pos, map);
					if (buf > max) { max = buf; resp = pos; }
				}
			return resp;
		}

		private int GetWeight(Point pos, CellValue[,] map)
		{
			var weight = Direction.Values.Sum(dir => GetOneNeighborWeight(pos, dir, map));
			weight += (int)Math.Pow(ConnectInRange(CellValue.Bot, map, pos, 2), 3);
			weight += (int)Math.Pow(ConnectInRange(CellValue.User, map, pos, 2), 2);
			return weight;
		}

		private int GetOneNeighborWeight(Point pos, Point dir, CellValue[,] map)
		{
			var weight = 0;
			pos += dir;
			if (!InRange(pos, map)) return 0;

			if (map[pos.X, pos.Y] == CellValue.User) weight += 1;
			if (map[pos.X, pos.Y] == CellValue.Bot) weight += 2;
			return weight;
		}

		private int ConnectInRange(CellValue val, CellValue[,] map, Point pos, int range)
		{
			return
				CountAmount(val, map, pos, Direction.Down, Direction.Up, range) +
				CountAmount(val, map, pos, Direction.Right, Direction.Left, range) +
				CountAmount(val, map, pos, Direction.DownLeft, Direction.UpRight, range) +
				CountAmount(val, map, pos, Direction.LeftUp, Direction.RightDown, range);
		}

		private int CountAmount(CellValue value, CellValue[,] map, Point pos, Point dir1, Point dir2, int range)
		{
			var count = 1;
			Point check;
			for (var i = 1; i <= range; i++)
			{
				check = pos + dir1 * new Point(i);
				if (!InRange(check, map)) break;
				if (map[check.X, check.Y] == value) count++;
				else break;
			}

			for (var i = 1; i <= range; i++)
			{
				check = pos + dir2 * new Point(i);
				if (!InRange(check, map)) break;
				if (map[check.X, check.Y] == value) count++;
				else break;
			}
			return count;
		}
	}
}
