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

		/// <summary>
		/// Initialize a test session
		/// </summary>
		public TestSession()
		{
			Score = 0;
			TimeRemaining = 30;
			CurrentLevel = Difficulty.Introduction;
			GenerateProblem(ProblemType.LEG);
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
				Score++;
				CurrentLevel = Difficulty.Addition;
				GenerateProblem(Calculation.RandomRange(new Range(0, 1)) == 0 ? ProblemType.LEG : ProblemType.MultipleChoice);
			}
			else if (outcome == ProblemEvaluation.Incorrect)
			{
				TimeRemaining -= 5;
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
		Introduction, Addition, Multiplication, AddMultiply, four, five, six
	}
}
