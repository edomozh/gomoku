namespace Gomoku
{
	using System;
	using Microsoft.Xna.Framework;

	internal class Cell : Control
	{
		private CellValue _value;
		public CellValue Value
		{
			get { return _value; }
			set
			{
				switch (value)
				{
					case CellValue.None:
						Color = Hower ? Light(Color.Gray, 40) : Color.Gray;
						break;
					case CellValue.User:
						Color = Hower ? Light(Color.Purple, 40) : Color.Purple;
						break;
					case CellValue.Bot:
						Color = Hower ? Light(Color.Green, 40) : Color.Green;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(value), value, null);
				}
				_value = value;
			}
		}

		public Cell(Gomoku game, Rectangle rectangle) : base(game, rectangle)
		{
			Enter += control => Color = Light(Color, 40);
			Leave += control => Color = Dark(Color, 40);
			Value = CellValue.None;
		}

		private static Color Light(Color color, byte val)
		{
			color.R += val;
			color.G += val;
			color.B += val;
			return color;
		}
		private static Color Dark(Color color, byte val)
		{
			color.R -= val;
			color.G -= val;
			color.B -= val;
			return color;
		}
	}
}