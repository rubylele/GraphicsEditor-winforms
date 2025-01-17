﻿using Draw.src.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor

		public DialogProcessor()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Избран елемент.
		/// </summary>
		private Shape selection;
		public Shape Selection
		{
			get { return selection; }
			set { selection = value; }
		}

		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging
		{
			get { return isDragging; }
			set { isDragging = value; }
		}

		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation
		{
			get { return lastLocation; }
			set { lastLocation = value; }
		}

		#endregion

		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>

		/// <summary>
		/// Проверява дали дадена точка е в елемента.
		/// Обхожда в ред обратен на визуализацията с цел намиране на
		/// "най-горния" елемент т.е. този който виждаме под мишката.
		/// </summary>
		/// <param name="point">Указана точка</param>
		/// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
		public Shape ContainsPoint(PointF point, DoubleBufferedPanel panel)
		{
			for (int i = panel.ShapeList.Count - 1; i >= 0; i--)
			{
				if (panel.ShapeList[i].Contains(point))
				{
					return panel.ShapeList[i];
				}
			}
			return null;
		}

		/// <summary>
		/// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
		/// </summary>
		/// <param name="p">Вектор на транслация.</param>
		public void TranslateTo(PointF p)
		{
			if (Selection != null)
			{
				Selection.Location = new PointF(Selection.Location.X + p.X - lastLocation.X, Selection.Location.Y + p.Y - lastLocation.Y);
				LastLocation = p;
			}
		}

		public void SetNewWidth(int width)
		{
			if (Selection != null)
			{
				Selection.Width = width;
			}
		}

		public void SetNewHeight(int height)
		{
			if (Selection != null)
			{
				Selection.Height = height;
			}
		}

		public void SetNewTransparency(int transparency)
		{
			if (Selection != null)
			{
				Selection.Transparency = transparency;
			}
		}

		public void SetNewBorderWidth(int borderWidth, Color updateColor)
		{
			if (Selection != null)
			{
				Selection.BorderWidth = borderWidth;
				Selection.BorderColor = updateColor;
			}
		}

		public void SetNewRotation(float rotation)
		{
			if (Selection != null)
			{
				Selection.Rotation = rotation;
			}
		}


		public static void SaveAsFileJpeg(string path, List<Shape> data)
		{
			var json = JsonConvert.SerializeObject(data);
			File.WriteAllText(path, json);
		}

		public static void OpenAsFileJpeg(string path, DoubleBufferedPanel panel)
		{
			var uploadList = JsonConvert.DeserializeObject<List<Shape>>(File.ReadAllText(path));

			foreach (var item in uploadList)
			{
				switch (item.Id)
				{
					case 1:
						panel.AddRandomRectangle(panel);
						break;
					case 2:
						panel.AddRandomCircle(panel);
						break;
					case 3:
						panel.AddRandomSquare(panel);
						break;
					case 4:
						panel.AddRandomMulti(panel);
						break;
				}
			}
		}

		public string OpenDialog(string text, string caption)
		{
			Form form = new Form()
			{
				Width = 500,
				Height = 200,
				FormBorderStyle = FormBorderStyle.FixedDialog,
				Text = caption,
				StartPosition = FormStartPosition.CenterScreen
			};

			Label label = new Label() { Left = 50, Top = 20, Text = text };
			TextBox box = new TextBox() { Left = 50, Top = 50, Width = 400 };
			Button confirm = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
			confirm.Click += (sender, e) => { form.Close(); };
			form.Controls.Add(box);
			form.Controls.Add(confirm);
			form.Controls.Add(label);
			form.AcceptButton = confirm;

			return form.ShowDialog() == DialogResult.OK ? box.Text : "";
		}

	}
}