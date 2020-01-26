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
		public Problem LastProblem { get; private set; }
		public ProblemEvaluation LastEvaluation { get; private set; } = ProblemEvaluation.Null;
		public int LastScoreGain { get; private set; } = 0;
		public bool TestStarted => CurrentLevel != Difficulty.Introduction;
		public bool TestActive => TimeRemaining > 0;
		private int incrementingScore = 1;

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
				if (TimeRemaining <= 0) EndSession();
			}
		}

		/// <summary>
		/// Creates a new problem for the user to solve.
		/// </summary>
		/// <param name="testType">The type of problem to generate.</param>
		public void GenerateProblem(ProblemType testType)
		{
			if (Problem != null)
			{
				LastProblem = Problem;
			}

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
			LastEvaluation = outcome;
			LastScoreGain = 0;

			if (outcome == ProblemEvaluation.Correct)
			{
				Score += Math.Max((int)CurrentLevel, 1);
				LastScoreGain = Math.Max((int)CurrentLevel, 1);
				incrementingScore++;
				CheckDifficulty();
				GenerateProblem(Problem.GetType() == typeof(LEGProblem) ? ProblemType.MultipleChoice : ProblemType.LEG);
			}
			else if (outcome == ProblemEvaluation.Incorrect && CurrentLevel != Difficulty.Introduction)
			{
				TimeRemaining -= (int)CurrentLevel + 2;
				Score = Math.Max(0, Score - 2);
				LastScoreGain = -2;
				CurrentLevel = (Difficulty)Math.Max(1, (int)CurrentLevel - 1);
				incrementingScore = 0;
				GenerateProblem(Problem.GetType() == typeof(LEGProblem) ? ProblemType.LEG : ProblemType.MultipleChoice);
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
			if (TestActive)
			{
				ProcessResult(Problem.GradeResponse(number));
			}
		}

		public void EndSession()
		{
			TimeRemaining = 0;
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
