using System;
using System.Linq;
using System.Timers;
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
		public static Timer timer = new Timer(100);
		private WindowState windowState;

		public MainWindow()
		{
			InitializeComponent();

			windowState = Aptitude_Test.WindowState.Welcome;
			timer.Elapsed += Timer_Elapsed;
			timer.AutoReset = true;
			timer.Enabled = true;
			UpdateUI();
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (testSession == null) return;

			Dispatcher.Invoke(() =>
			{
				UpdateUI();
			});

		}

		/// <summary>
		/// Starts a new testing session
		/// </summary>
		public void Begin()
		{
			if (testSession == null || !testSession.TestActive)
			{
				testSession = new TestSession();
				windowState = Aptitude_Test.WindowState.Testing;
				UpdateUI();
			}
		}

		public void Stop()
		{
			if (testSession != null)
			{
				testSession.EndSession();
				UpdateUI();
			}
		}

		/// <summary>
		/// Draws the equations to the screen
		/// </summary>
		public void UpdateUI()
		{
			//Update timer, score, and difficulty
			TimeSpan timeSpan = TimeSpan.FromSeconds(testSession != null ? testSession.TimeRemaining : 120);
			Clock.Text = string.Format("{0}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
			Score.Text = testSession != null ? testSession.Score.ToString() : "0";
			Difficulty.Text = string.Concat(Enumerable.Repeat('★', testSession != null ? (int)testSession.CurrentLevel : 0));

			//Scoring feedback
			Scoring.Visibility = testSession != null && testSession.TestActive ? Visibility.Visible : Visibility.Hidden;
			Scoring.Text = testSession != null && testSession.LastEvaluation != ProblemEvaluation.Null ? string.Format("{0}! {1}", testSession.LastEvaluation.ToString(), GetScoringString()) : "";

			//Panel visibility
			if (windowState == Aptitude_Test.WindowState.Testing && !testSession.TestActive)
				windowState = Aptitude_Test.WindowState.Results;
			bool testing = windowState == Aptitude_Test.WindowState.Testing && testSession != null;
			WelcomePanel.Visibility = windowState == Aptitude_Test.WindowState.Welcome ? Visibility.Visible : Visibility.Hidden;
			LEGProblemPanel.Visibility = testing && testSession.Problem.GetType() == typeof(LEGProblem) ? Visibility.Visible : Visibility.Hidden;
			MultipleChoicePanel.Visibility = testing && testSession.Problem.GetType() == typeof(MultipleChoiceProblem) ? Visibility.Visible : Visibility.Hidden;
			ResultsPanel.Visibility = windowState == Aptitude_Test.WindowState.Results ? Visibility.Visible : Visibility.Hidden; 

			//Panel contents
			if (testSession != null && testSession.Problem.GetType() == typeof(LEGProblem))
			{
				LeftAnswer.Text = ((LEGProblem)testSession.Problem).Left.GetString();
				RightAnswer.Text = ((LEGProblem)testSession.Problem).Right.GetString();
				LEGInsutrction.Visibility = testSession.CurrentLevel == Aptitude_Test.Difficulty.Introduction ? Visibility.Visible : Visibility.Hidden;
			}
			else if (testSession != null && testSession.Problem.GetType() == typeof(MultipleChoiceProblem))
			{
				MultipleChoiceProblem problem = (MultipleChoiceProblem)testSession.Problem;
				Equation.Text = problem.Equation.GetMysteryString(problem.MysteryIndex);
				MC1.Text = problem.Answers[0].ToString();
				MC2.Text = problem.Answers[1].ToString();
				MC3.Text = problem.Answers[2].ToString();
				MC4.Text = problem.Answers[3].ToString();
				MCInstruction.Visibility = testSession.CurrentLevel == Aptitude_Test.Difficulty.Introduction ? Visibility.Visible : Visibility.Hidden;
			}
			else if (testSession != null)
			{

			}
		}

		private string GetScoringString()
		{
			if (testSession == null || testSession.LastEvaluation == ProblemEvaluation.Invalid) return "";
			else return string.Format("({0}{1})", testSession.LastScoreGain > 0 ? "+" : "", testSession.LastScoreGain.ToString()) ;
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
			else if (e.Key == Key.Enter)
			{
				Begin();
			}
			else if (e.Key == Key.Escape)
			{
				Stop();
			}

			UpdateUI();
		}

		private void Start_Click(object sender, RoutedEventArgs e)
		{
			Begin();
		}
		#endregion
	}

	public enum WindowState
	{
		Welcome, Testing, Results
	}
}