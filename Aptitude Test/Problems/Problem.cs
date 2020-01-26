namespace Aptitude_Test
{
	/// <summary>
	/// Base class for a assessment problem
	/// </summary>
	public abstract class Problem
	{
		public abstract ProblemEvaluation GradeResponse(int response);
	}

	/// <summary>
	/// The types of problems the application can generate
	/// </summary>
	public enum ProblemType
	{
		LEG, MultipleChoice
	}

	/// <summary>
	/// The outcomes of a grading evaluation
	/// </summary>
	public enum ProblemEvaluation
	{
		Correct, Incorrect, Invalid, Null
	}
}
