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
			List<Operator> operators;

			switch (difficulty)
			{
				case Difficulty.Introduction:
					operators = new List<Operator>() { Operator.Add };
					Left = new TwoTermEquation(new Range(2, 4), new Range(1, 3), operators);
					Right = new TwoTermEquation(new Range(2, 4), new Range(1, 3), operators);
					break;
				case Difficulty.Addition:
					operators = new List<Operator>() { Operator.Add, Operator.Subtract };
					Left = new TwoTermEquation(new Range(1, 9), new Range(1, 9), operators);
					Right = new TwoTermEquation(new Range(1, 9), new Range(1, 9), operators);
					break;
				default:
					operators = new List<Operator>() { Operator.Add };
					Left = new TwoTermEquation(new Range(1, 1), new Range(1, 1), operators);
					Right = new TwoTermEquation(new Range(1, 1), new Range(1, 1), operators);
					break;
			}
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
