using System.Collections.Generic;

namespace Aptitude_Test
{
	/// <summary>
	/// Problem where the user has to select a correct answer from an equation that is missing a value.
	/// </summary>
	class MultipleChoiceProblem : Problem
	{
		public Equation Equation { get; private set; }
		public int[] Answers { get; private set; } = new int[4];
		public int CorrectIndex { get; private set; } = 0;
		public int MysteryIndex { get; private set; } = 0;

		public MultipleChoiceProblem(Difficulty difficulty)
		{
			switch (difficulty)
			{
				default:
					Equation = new TwoTermEquation(new Range(1, 9), new Range(1, 9), new List<Operator>() { Operator.Add });
					MysteryIndex = Calculation.RandomRange(new Range(0, 2));
					CorrectIndex = Calculation.RandomRange(new Range(0, 3));
					for (int i = 0; i < 4; i++)
					{
						Answers[i] = i == CorrectIndex ? Equation.GetMysteryValue(MysteryIndex) : Equation.GetMysteryValue(MysteryIndex) + 3;
					}
					break;
			}
		}

		public override ProblemEvaluation GradeResponse(int response)
		{
			return response == CorrectIndex ? ProblemEvaluation.Correct : ProblemEvaluation.Incorrect;
		}
	}
}
