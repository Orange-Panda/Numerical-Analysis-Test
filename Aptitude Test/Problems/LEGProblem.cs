using System.Collections.Generic;

namespace Aptitude_Test
{
	/// <summary>
	/// Problem where the user has to determine if the left side of the problem is greater than, less than, or equal to the right side.
	/// </summary>
	class LEGProblem : Problem
	{
		public Equation Left { get; private set; }
		public Equation Right { get; private set; }

		public LEGProblem(Difficulty difficulty)
		{
			Left = Calc.CreateEquation(difficulty);
			Right = Calc.CreateEquation(difficulty);
		}

		public override ProblemEvaluation GradeResponse(int response)
		{
			if (response < 0 || response > 2) return ProblemEvaluation.Invalid;
			return (LEGResponse)response == GetAnswer() ? ProblemEvaluation.Correct : ProblemEvaluation.Incorrect;
		}

		public LEGResponse GetAnswer()
		{
			if (Left.GetValue() > Right.GetValue()) return LEGResponse.GreaterThan;
			else if (Left.GetValue() == Right.GetValue()) return LEGResponse.EqualTo;
			else return LEGResponse.LessThan;
		}

		/// <summary>
		/// Types of user input for "Less than, Equal to, Greater than" questions.
		/// </summary>
		public enum LEGResponse
		{
			GreaterThan, LessThan, EqualTo
		}
	}
}
