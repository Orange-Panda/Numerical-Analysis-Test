using System.Collections.Generic;

namespace Aptitude_Test
{
	/// <summary>
	/// Problem where the user has to select a correct answer from an equation that is missing a value.
	/// </summary>
	class MultipleChoiceProblem : Problem
	{
		public Equation Equation { get; private set; }
		public double[] Answers { get; private set; } = new double[4];
		public int CorrectIndex { get; private set; } = 0;
		public int MysteryIndex { get; private set; } = 0;

		public MultipleChoiceProblem(Difficulty difficulty)
		{
			Equation = Calc.CreateEquation(difficulty);
			MysteryIndex = Calc.RandomRange(new Range(0, 2));
			CorrectIndex = Calc.RandomRange(new Range(0, 3));

			for (int i = 0; i < 4; i++)
			{
				Answers[i] = i == CorrectIndex ? Equation.GetMysteryValue(MysteryIndex) : Equation.GetMysteryValue(MysteryIndex) + 3;
			}
		}

		public override ProblemEvaluation GradeResponse(int response)
		{
			return response == CorrectIndex ? ProblemEvaluation.Correct : ProblemEvaluation.Incorrect;
		}
	}
}
