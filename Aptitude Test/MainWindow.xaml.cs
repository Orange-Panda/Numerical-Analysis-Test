using System.Windows;
using System.Windows.Input;

namespace Aptitude_Test
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private TestSession testSession;

		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Starts a new testing session
		/// </summary>
		public void Begin()
		{
			testSession = new TestSession();
			UpdateEquations();
		}

		/// <summary>
		/// Draws the equations to the screen
		/// </summary>
		public void UpdateEquations()
		{
			LEGProblemPanel.Visibility = testSession.Problem.GetType() == typeof(LEGProblem) ? Visibility.Visible : Visibility.Hidden;
			MultipleChoicePanel.Visibility = testSession.Problem.GetType() == typeof(MultipleChoiceProblem) ? Visibility.Visible : Visibility.Hidden;

			if (testSession.Problem.GetType() == typeof(LEGProblem))
			{
				LeftAnswer.Text = ((LEGProblem)testSession.Problem).Left.GetString();
				RightAnswer.Text = ((LEGProblem)testSession.Problem).Right.GetString();
			}
			else if (testSession.Problem.GetType() == typeof(MultipleChoiceProblem))
			{
				MultipleChoiceProblem problem = (MultipleChoiceProblem)testSession.Problem;
				Equation.Text = problem.Equation.GetMysteryString(problem.MysteryIndex);
				MC1.Text = problem.Answers[0].ToString();
				MC2.Text = problem.Answers[1].ToString();
				MC3.Text = problem.Answers[2].ToString();
				MC4.Text = problem.Answers[3].ToString();
			}
		}

		#region Events
		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.D1 || e.Key == Key.NumPad1)
			{
				testSession.UserInput(0);
			}
			else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
			{
				testSession.UserInput(1);
			}
			else if (e.Key == Key.D3 || e.Key == Key.NumPad3)
			{
				testSession.UserInput(2);
			}
			else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
			{
				testSession.UserInput(3);
			}

			Console.Text = string.Format("{0} {1}", testSession.Score, testSession.TimeRemaining);
			UpdateEquations();
		}

		private void Start_Click(object sender, RoutedEventArgs e)
		{
			Begin();
		}
		#endregion
	}
}