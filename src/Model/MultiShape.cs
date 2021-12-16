using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
	public class MultiShape : Shape
	{
		#region Constructor
		public MultiShape()
		{

		}

		public MultiShape(RectangleF rect) : base(rect)
		{
		}

		#endregion

		/// <summary>
		/// Проверка за принадлежност на точка point към правоъгълника.
		/// В случая на правоъгълник този метод може да не бъде пренаписван, защото
		/// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
		/// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
		/// елемента в този случай).
		/// </summary>
		public override bool Contains(PointF point)
		{
			if (base.Contains(point))
				// Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
				// В случая на правоъгълник - директно връщаме true
				return true;
			else
				// Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
				return false;
		}

		/// <summary>
		/// Частта което визуализира конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			PointF[] points = {
					new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y),
					new PointF((Rectangle.X + Rectangle.Width / 2) + Rectangle.Width/12, Rectangle.Y + Rectangle.Height/6 - Rectangle.Height/8),
					new PointF(Rectangle.Right, Rectangle.Y + Rectangle.Height/2.5f),
					new PointF((Rectangle.X + Rectangle.Width / 2) + Rectangle.Width/10, Rectangle.Y + Rectangle.Height/6 + Rectangle.Height/8),
					new PointF(Rectangle.X + Rectangle.Width/2 + Rectangle.Width/8, Rectangle.Bottom),
					new PointF(Rectangle.X + Rectangle.Width/2, Rectangle.Y + Rectangle.Height/2 + Rectangle.Height/6),
					new PointF(Rectangle.X + Rectangle.Width/2 - Rectangle.Width/8, Rectangle.Bottom),
					new PointF((Rectangle.X + Rectangle.Width / 2) - Rectangle.Width/8, Rectangle.Y + Rectangle.Height/6 + Rectangle.Height/8),
					new PointF(Rectangle.X, Rectangle.Y + Rectangle.Height/2.5f),
					new PointF((Rectangle.X + Rectangle.Width / 2) - Rectangle.Width/12, Rectangle.Y + Rectangle.Height/6 - Rectangle.Height/8),
					};
			if (BorderWidth != 0)
			{
				grfx.DrawPolygon(new Pen(Color.FromArgb(Transparency, BorderColor), BorderWidth), points);
			}
			grfx.FillPolygon(new SolidBrush(Color.FromArgb(Transparency, FillColor)), points);
		}
	}
}
