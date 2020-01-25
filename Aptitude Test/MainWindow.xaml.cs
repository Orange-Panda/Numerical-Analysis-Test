using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aptitude_Test
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public int Score { get; private set; }
		public double TimeRemaining { get; private set; }
		public Difficulty CurrentLevel { get; private set; } 
		public Response CorrectResponse { get; private set; }

		private Random random;

		public MainWindow()
		{
			InitializeComponent();
			random = new Random();
		}

		public void Begin()
		{
			Score = 0;
			TimeRemaining = 30;
			CurrentLevel = Difficulty.Introduction;
			GeneratePuzzle();
		}

		public void GeneratePuzzle()
		{
			Random random = new Random();
			switch (CurrentLevel)
			{
				case Difficulty.Introduction:
					LeftAnswer.Text = "1 + 2";
					RightAnswer.Text = "4 - 3";
					CorrectResponse = Response.Greater;
					break;
				case Difficulty.Addition:
					int left = AdditionPuzzle(EquationTarget.Left);
					int right = AdditionPuzzle(EquationTarget.Right);
					if (left > right) CorrectResponse = Response.Greater;
					else if (left == right) CorrectResponse = Response.Equal;
					else CorrectResponse = Response.Less;
					break;
				default:
					break;
			}
		}

		public void GradeResponse(Response response)
		{
			if (response == CorrectResponse)
			{
				CurrentLevel = Difficulty.Addition;
				GeneratePuzzle();
				Console.Text = "Correct!";
			}
			else
			{
				TimeRemaining -= 5;
				Console.Text = "Incorrect";
			}
		}

		#region Equation Generators
		public int AdditionPuzzle(EquationTarget target)
		{
			int num1 = random.Next(1, 10);
			int num2 = random.Next(1, 10);
			int operand = random.Next(0, 2);
			TextBlock textBlock = target == EquationTarget.Left ? LeftAnswer : RightAnswer;
			textBlock.Text = string.Format("{0} {1} {2}", num1, operand == 0 ? '+' : '-', num2);
			return operand == 0 ? num1 + num2 : num1 - num2;
		}
		#endregion

		#region Events
		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.D1 || e.Key == Key.NumPad1)
			{
				GradeResponse(Response.Greater);
			}
			else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
			{
				GradeResponse(Response.Less);
			}
			else if (e.Key == Key.D3 || e.Key == Key.NumPad3)
			{
				GradeResponse(Response.Equal);
			}
			else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
			{
				GradeResponse(Response.Greater);
			}
		}

		private void Start_Click(object sender, RoutedEventArgs e)
		{
			Begin();
		}
		#endregion
	}
}

public enum EquationTarget
{
	Left, Right
}

public enum Response
{
	Greater, Less, Equal
}

public enum Difficulty
{
	Introduction, Addition, Multiplication, AddMultiply, four, five, six
}