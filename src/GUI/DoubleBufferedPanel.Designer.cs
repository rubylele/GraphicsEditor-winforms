﻿using Draw.src.Model;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Draw
{
	partial class DoubleBufferedPanel
	{
		//partial class for drawing a primitives
		public void AddRandomRectangle(DoubleBufferedPanel panel)
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			RectangleShape rect = new RectangleShape(new Rectangle(x, y, 200, 100));
			rect.FillColor = Color.White;
			rect.BorderColor = Color.Black;
			rect.BorderWidth = 10;
			rect.Transparency = 244;
			rect.Id = 1;

			panel.ShapeList.Add(rect);
		}

		public void AddRandomCircle(DoubleBufferedPanel panel)
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			CircleShape circle = new CircleShape(new Rectangle(x, y, 100, 100));
			circle.FillColor = Color.White;
			circle.BorderColor = Color.Black;
			circle.BorderWidth = 10;
			circle.Transparency = 244;
			circle.Id = 2;

			panel.ShapeList.Add(circle);
		}

		public void AddRandomSquare(DoubleBufferedPanel panel)
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			SquareShape square = new SquareShape(new Rectangle(x, y, 100, 100));
			square.FillColor = Color.White;
			square.BorderColor = Color.Black;
			square.BorderWidth = 10;
			square.Transparency = 244;
			square.Id = 3;
			panel.ShapeList.Add(square);
		}

		public void AddRandomMulti(DoubleBufferedPanel panel)
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			MultiShape multiShape = new MultiShape(new RectangleF(x, y, 65, 20));
			multiShape.FillColor = Color.White;
			multiShape.BorderColor = Color.Black;
			multiShape.BorderWidth = 10;
			multiShape.Transparency = 244;
			multiShape.Id = 4;
			panel.ShapeList.Add(multiShape);
		}

		public void AddRandomZadacha(DoubleBufferedPanel panel)
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			ZadachaShape zadachaShape = new ZadachaShape(new RectangleF(x, y, 65, 20));
			zadachaShape.FillColor = Color.White;
			zadachaShape.BorderColor = Color.Black;
			zadachaShape.BorderWidth = 10;
			zadachaShape.Transparency = 244;
			zadachaShape.Id = 4;
			panel.ShapeList.Add(zadachaShape);
		}
	}

	partial class DoubleBufferedPanel
	{

		public List<Shape> ShapeList { get; set; } = new List<Shape>();
		public new string Name { get; set; }

		public DoubleBufferedPanel(string name)
		{
			this.Name = name;
		}

		public DoubleBufferedPanel(List<Shape> list)
		{
			this.ShapeList = list;
		}

		public DoubleBufferedPanel(string name, List<Shape> list)
		{
			this.Name = name;
			this.ShapeList = list;
		}



		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		public void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// DoubleBufferedPanel
			// 
			this.AutoScroll = true;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.DoubleBuffered = true;
			this.BackColor = Color.White;
			this.ResumeLayout(false);
			this.Size = new System.Drawing.Size(1490, 700);
			this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.TabIndex = 6;
		}
	}
}
