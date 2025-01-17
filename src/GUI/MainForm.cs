﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		/// 

		private DialogProcessor dialogProcessor = new DialogProcessor();
		public static List<DoubleBufferedPanel> viewPortList { get; set; } = new List<DoubleBufferedPanel>();
		public static DoubleBufferedPanel CurrentViewPort { get; set; } = new DoubleBufferedPanel("New project");
		public static Color BorderColor { get; set; } = new Color();
		public static int untitledTabs = 1;
		int width;
		int height;
		int borderWidth;

		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			CurrentViewPort.InitializeComponent();
			tabMenu.SelectedTab.Text = "New project";
			viewPortList.Add(CurrentViewPort);
			CurrentViewPort.Load += new EventHandler(ViewPortLoad);
			panel1.Controls.Add(CurrentViewPort);
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		void ViewPortLoad(object sender, EventArgs e)
		{
			CurrentViewPort.Paint += new PaintEventHandler(ViewPortPaint);
			CurrentViewPort.MouseDown += new MouseEventHandler(ViewPortMouseDown);
			CurrentViewPort.MouseMove += new MouseEventHandler(ViewPortMouseMove);
			CurrentViewPort.KeyDown += new KeyEventHandler(ViewPortKeyPress);
			CurrentViewPort.MouseUp += new MouseEventHandler(ViewPortMouseUp);
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		private void openToolStripMenuItemClick(object sender, EventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog();
			open.Filter = "Image Files(*.Jpeg;)| *.Jpeg; ";
			if (open.ShowDialog() == DialogResult.OK)
			{
				DialogProcessor.OpenAsFileJpeg(open.FileName, CurrentViewPort);
				tabMenu.SelectedTab.Text = Path.GetFileNameWithoutExtension(open.FileName);
			}
			statusBar.Items[0].Text = "Last action: Uploading project file as Jpeg";
			CurrentViewPort.Refresh();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog save = new SaveFileDialog();
			save.Filter = "Image Files(*.Jpeg;)| *.Jpeg; ";
			save.FileName = tabMenu.SelectedTab.Text;
			if (save.ShowDialog() == DialogResult.OK)
			{
				DialogProcessor.SaveAsFileJpeg(save.FileName, CurrentViewPort.ShapeList);
				tabMenu.SelectedTab.Text = Path.GetFileNameWithoutExtension(save.FileName);
			}
			statusBar.Items[0].Text = "Последно действие : сохранение на файла";
		}
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e, CurrentViewPort);
		}

		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>


		/// <summary>
		/// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
		/// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
		/// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
		/// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
		/// </summary>
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked)
			{
				dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location, CurrentViewPort);

				if (dialogProcessor.Selection != null)
				{
					statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
					dialogProcessor.IsDragging = true;
					dialogProcessor.LastLocation = e.Location;
				}
			}
			if (deleteBtn.Checked)
			{
				if (dialogProcessor.Selection != null)
				{
					CurrentViewPort.ShapeList.Remove(dialogProcessor.Selection);
					statusBar.Items[0].Text = "Последно действие : изтриване на примитив";
					CurrentViewPort.Refresh();
				}
			}
		}

		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging)
			{
				if (dialogProcessor.Selection != null)
				{
					statusBar.Items[0].Text = "Последно действие: Влачене";
					dialogProcessor.TranslateTo(e.Location);
				}
				CurrentViewPort.Refresh();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
			CurrentViewPort.Refresh();
		}

		private void ViewPortKeyPress(object sender, KeyEventArgs e)
		{
			if (e.KeyData.ToString() == "S, Control")
			{
				saveToolStripMenuItem_Click(sender, e);
				statusBar.Items[0].Text = "Последно действие: Сохранение";
			}

			if (e.KeyData.ToString() == "O, Control")
			{
				openToolStripMenuItemClick(sender, e);
				statusBar.Items[0].Text = "Последно действие: Отваряне";
			}
		}

		private void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			CurrentViewPort.AddRandomRectangle(CurrentViewPort);

			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

			CurrentViewPort.Refresh();
		}

		private void DrawCircleSpeedButtonClick(object sender, EventArgs e)
		{
			CurrentViewPort.AddRandomCircle(CurrentViewPort);

			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

			CurrentViewPort.Refresh();
		}

		private void ColorButtonClick(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				dialogProcessor.Selection.FillColor = colorDialog1.Color;
				statusBar.Items[0].Text = "Последно действие: Промяна на цвят";
				CurrentViewPort.Refresh();
			}
		}

		private void AddSquareSpeedButton_Click(object sender, EventArgs e)
		{
			CurrentViewPort.AddRandomSquare(CurrentViewPort);

			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

			CurrentViewPort.Refresh();
		}

		private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
		{
			width = Convert.ToInt32(numericUpDown1.Value);
			dialogProcessor.SetNewWidth(width);
			statusBar.Items[0].Text = "Последно действие: увеличаване на широчина.";
			CurrentViewPort.Refresh();
		}

		private void numericUpDown2_ValueChanged(object sender, EventArgs e)
		{
			height = Convert.ToInt32(numericUpDown2.Value);
			dialogProcessor.SetNewHeight(height);
			statusBar.Items[0].Text = "Последно действие: смена на височина.";
			CurrentViewPort.Refresh();
		}

		private void transparencyUpDown_ValueChanged(object sender, EventArgs e)
		{
			dialogProcessor.SetNewTransparency((int)transparencyUpDown.Value);
			statusBar.Items[0].Text = "Последно действие: смена на прозрачност. ";
			CurrentViewPort.Refresh();
		}

		private void borderWidthUpDown_ValueChanged(object sender, EventArgs e)
		{
			borderWidth = Convert.ToInt32(borderWidthUpDown.Value);
			dialogProcessor.SetNewBorderWidth(borderWidth, BorderColor);
			CurrentViewPort.Refresh();
		}

		private void rotationUpDown_ValueChanged(object sender, EventArgs e)
		{
			dialogProcessor.SetNewRotation((float)rotationUpDown.Value);
			statusBar.Items[0].Text = "Последно действие: смена на завъртане.";
			CurrentViewPort.Refresh();
		}


		private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string input = dialogProcessor.OpenDialog("Project Name: ", tabMenu.SelectedTab.Text);

			if (input != "" && input != null)
			{
				tabMenu.TabPages.Add(new TabPage(input));
			}
			else
			{
				tabMenu.TabPages.Add(new TabPage("New project" + untitledTabs));
				untitledTabs++;
			}
			tabMenu.SelectedIndex = tabMenu.TabPages.Count - 1;
		}

		private void AddMultiButton_Click(object sender, EventArgs e)
		{
			CurrentViewPort.AddRandomMulti(CurrentViewPort);

			statusBar.Items[0].Text = "Последно действие: Рисуване на къща";

			CurrentViewPort.Refresh();
		}
		private void AddZadachaButton_Click(object sender, EventArgs e)
		{
			CurrentViewPort.AddRandomZadacha(CurrentViewPort);

			statusBar.Items[0].Text = "Последно действие: Рисуване на тръгълник";

			CurrentViewPort.Refresh();
		}

		private void tabMenu_SelectedIndexChanged(object sender, EventArgs e)
		{
			panel1.Controls.Clear();

			if (!viewPortList.Exists(v => v.Name == tabMenu.SelectedTab.Text))
			{
				CurrentViewPort = new DoubleBufferedPanel(tabMenu.SelectedTab.Text);
				CurrentViewPort.InitializeComponent();
				viewPortList.Add(CurrentViewPort);
				CurrentViewPort.Load += new EventHandler(ViewPortLoad);
			}

			foreach (var viewPort in viewPortList)
			{
				if (viewPort.Name == tabMenu.SelectedTab.Text)
				{
					CurrentViewPort = viewPort;
					panel1.Controls.Add(CurrentViewPort);
					break;
				}
			}
		}

		private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void helpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("You can use this commands to perform a different actions. \n" +
				"Follow this commands: \n\n" +
				"Ctrl + S - allow you to save your current view" +
				"Ctrl + O - allow yout to open saved file");
		}

        private void deleteBtn_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

				CurrentViewPort.AddRandomZadacha(CurrentViewPort);

				statusBar.Items[0].Text = "Последно действие: Рисуване на тригълник";

				CurrentViewPort.Refresh();
			}


		}
    }
