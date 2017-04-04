﻿namespace Gomoku
{
	using System;
	using Microsoft.Xna.Framework;

	internal class Bot
	{
		private readonly Random _rnd;
		private readonly Board _board;
		private readonly Gomoku _game;

		public Bot(Gomoku game, Board board)
		{
			_game = game;
			_board = board;
			_rnd = new Random();
			_board.OneMoreStep += (p, v) => { if (v == CellValue.User) MakeMove(p); };
		}
		public void MakeMove(Point lastMove)
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
				pos = GetBestMove(lastMove);
			}
			_board.MakeMove(pos, CellValue.Bot);
		}

		private Point GetBestMove(Point lastMove)
		{
			var max = 0;
			var res = new Point();
			for (var y = 0; y < _board.Dimension; y++)
				for (var x = 0; x < _board.Dimension; x++)
				{
					var pos = new Point(x, y);
					if (!_board.IsNone(pos)) continue;
					var buf = GetWeight(pos);
					if (buf > max) { max = buf; res = pos; }
				}
			return res;
		}

		private int GetWeight(Point pos)
		{
			var weight = 0;
			weight += GetNeighborWeight(pos);
			if (_board.IsConnectAmount(pos, CellValue.Bot, 2)) weight += 4;
			if (_board.IsConnectAmount(pos, CellValue.Bot, 3)) weight += 1000;
			if (_board.IsConnectAmount(pos, CellValue.Bot, 4)) weight += 3000;
			if (_board.IsConnectAmount(pos, CellValue.Bot, 5)) weight += 6000;

			if (_board.IsConnectAmount(pos, CellValue.User, 3)) weight += 500;
			if (_board.IsConnectAmount(pos, CellValue.User, 4)) weight += 1500;
			if (_board.IsConnectAmount(pos, CellValue.User, 5)) weight += 3500;

			return weight;
		}

		private int GetNeighborWeight(Point pos)
		{
			var weight = 0;
			foreach (var dir in Direction.Values)
			{
				weight += GetOneNeighborWeight(pos, dir);
				weight += GetOneNeighborWeight(pos, dir * new Point(2)) * 2;
			}
			return weight;
		}

		private int GetOneNeighborWeight(Point pos, Point dir)
		{
			var weight = 0;

			var newp = pos + dir;
			if (!_board.InBourd(newp)) return weight;

			if (_board.GetValue(newp) == CellValue.Bot) weight = 2;
			if (_board.GetValue(newp) == CellValue.User) weight = 1;
			if (_board.GetValue(newp) == CellValue.None) weight = 0;
			return weight;
		}
	}
}
