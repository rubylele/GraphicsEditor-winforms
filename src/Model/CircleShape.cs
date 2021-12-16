using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
	public class CircleShape : Shape
	{
		#region Constructor
		public CircleShape()
		{

		}
		public CircleShape(Shape shape) : base(shape)
		{

		}

		public CircleShape(RectangleF rect) : base(rect)
		{
		}

		public CircleShape(RectangleShape rectangle) : base(rectangle)
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
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			if (BorderWidth != 0)
			{
				grfx.DrawEllipse(new Pen(Color.FromArgb(Transparency, BorderColor), BorderWidth), Rectangle);
			}
			grfx.DrawEllipse(new Pen(Color.Black), Rectangle.X - 3, Rectangle.Y - 3, Rectangle.Width + 6, Rectangle.Height + 6);
			grfx.FillEllipse(new SolidBrush(Color.FromArgb(Transparency, FillColor)), Rectangle);
		}
	}
}
