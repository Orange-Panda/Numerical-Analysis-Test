namespace Aptitude_Test
{
	/// <summary>
	/// Base class for a assessment problem
	/// </summary>
	public abstract class Problem
	{
		/// <summary>
		/// Determines if the user input was correct or not.
		/// </summary>
		/// <param name="response">The index of user input</param>
		/// <returns>Correct, incorrect, or invalid</returns>
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
