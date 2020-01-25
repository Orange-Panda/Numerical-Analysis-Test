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
		public LEGResponse CorrectResponse { get; private set; }

		public MainWindow()
		{
			InitializeComponent();
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
			Equation left, right;
			List<Operator> operators;

			switch (CurrentLevel)
			{
				case Difficulty.Introduction:
					operators = new List<Operator>() { Operator.Add };
					left = new TwoTermEquation(new Range(2, 4), new Range(1, 3), operators);
					right = new TwoTermEquation(new Range(2, 4), new Range(1, 3), operators);
					break;
				case Difficulty.Addition:
					operators = new List<Operator>() { Operator.Add, Operator.Subtract };
					left = new TwoTermEquation(new Range(1, 9), new Range(1, 9), operators);
					right = new TwoTermEquation(new Range(1, 9), new Range(1, 9), operators);
					break;
				default:
					operators = new List<Operator>() { Operator.Add };
					left = new TwoTermEquation(new Range(1, 1), new Range(1, 1), operators);
					right = new TwoTermEquation(new Range(1, 1), new Range(1, 1), operators);
					break;
			}

			LeftAnswer.Text = left.GetString();
			RightAnswer.Text = right.GetString();
			if (left.GetValue() > right.GetValue()) CorrectResponse = LEGResponse.Greater;
			else if (left.GetValue() == right.GetValue()) CorrectResponse = LEGResponse.Equal;
			else CorrectResponse = LEGResponse.Less;
		}

		public void GradeResponse(LEGResponse response)
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

		#region Events
		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.D1 || e.Key == Key.NumPad1)
			{
				GradeResponse(LEGResponse.Greater);
			}
			else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
			{
				GradeResponse(LEGResponse.Less);
			}
			else if (e.Key == Key.D3 || e.Key == Key.NumPad3)
			{
				GradeResponse(LEGResponse.Equal);
			}
			else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
			{
				GradeResponse(LEGResponse.Greater);
			}
		}

		private void Start_Click(object sender, RoutedEventArgs e)
		{
			Begin();
		}
		#endregion
	}
}