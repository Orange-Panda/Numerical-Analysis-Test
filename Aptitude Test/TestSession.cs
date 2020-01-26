using System;
using System.Timers;

namespace Aptitude_Test
{
	/// <summary>
	/// Manages data related to the testing session
	/// </summary>
	public class TestSession
	{
		public int Score { get; private set; }
		public double TimeRemaining { get; private set; }
		public Difficulty CurrentLevel { get; private set; }
		public Problem Problem { get; private set; }
		public bool TestStarted { get; private set; }
		private int incrementingScore = 2;

		/// <summary>
		/// Initialize a test session
		/// </summary>
		public TestSession()
		{
			Score = 0;
			TimeRemaining = 120;
			CurrentLevel = Difficulty.Introduction;
			GenerateProblem(ProblemType.LEG);
			MainWindow.timer.Elapsed += Timer_Elapsed;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (TestStarted)
			{
				TimeRemaining -= 0.1;
			}
		}

		/// <summary>
		/// Creates a new problem for the user to solve.
		/// </summary>
		/// <param name="testType">The type of problem to generate.</param>
		public void GenerateProblem(ProblemType testType)
		{
			switch (testType)
			{
				case ProblemType.LEG:
					Problem = new LEGProblem(CurrentLevel);
					break;
				case ProblemType.MultipleChoice:
					Problem = new MultipleChoiceProblem(CurrentLevel);
					break;
			}
		}

		/// <summary>
		/// Takes action according to the outcome of the problem evaluation.
		/// </summary>
		/// <param name="outcome"></param>
		public void ProcessResult(ProblemEvaluation outcome)
		{
			if (outcome == ProblemEvaluation.Correct)
			{
				TestStarted = true;
				Score++;
				incrementingScore++;
				CheckDifficulty();
				GenerateProblem(incrementingScore % 2 == 0 ? ProblemType.LEG : ProblemType.MultipleChoice);
			}
			else if (outcome == ProblemEvaluation.Incorrect && CurrentLevel != Difficulty.Introduction)
			{
				TimeRemaining -= 5;
				CurrentLevel = (Difficulty)Math.Max(1, (int)CurrentLevel - 1);
				incrementingScore = 0;
				GenerateProblem(incrementingScore % 2 == 0 ? ProblemType.LEG : ProblemType.MultipleChoice);
			}
		}

		private void CheckDifficulty()
		{
			if (incrementingScore >= 3)
			{
				CurrentLevel = (Difficulty)Math.Min(Enum.GetNames(typeof(Difficulty)).Length - 1, (int)CurrentLevel + 1);
			}
		}

		/// <summary>
		/// Forwards user input to evaluate the problem
		/// </summary>
		/// <param name="number">The index of user input</param>
		public void UserInput(int number)
		{
			ProcessResult(Problem.GradeResponse(number));
		}
	}

	/// <summary>
	/// The escalating difficulties the assessment will reach
	/// </summary>
	public enum Difficulty
	{
		Introduction, Basic, Novice, Intermediate, Hard, Challenging, Maximum
	}
}
