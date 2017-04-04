namespace Gomoku
{
#if WINDOWS || LINUX
	using System;

	public static class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			var dimension = (args.Length > 0) ? int.Parse(args[0]) : 15;
			var cellSize = (args.Length > 1) ? int.Parse(args[1]) : 30;
			using (var game = new Gomoku(dimension, cellSize))
			{
				game.Run();
			}
		}
	}
#endif
}
