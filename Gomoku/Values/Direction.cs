namespace Gomoku
{
	using Microsoft.Xna.Framework;
	using System.Collections.Generic;

	internal static class Direction
	{
		public static readonly Point Up = new Point(0, -1);
		public static readonly Point UpRight = new Point(1, -1);
		public static readonly Point Right = new Point(1, 0);
		public static readonly Point RightDown = new Point(1, 1);
		public static readonly Point Down = new Point(0, 1);
		public static readonly Point DownLeft = new Point(-1, 1);
		public static readonly Point Left = new Point(-1, 0);
		public static readonly Point LeftUp = new Point(-1, -1);

		public static readonly List<Point> Values =
			new List<Point> { Up, UpRight, Right, RightDown, Down, DownLeft, Left, LeftUp };
	}
}
