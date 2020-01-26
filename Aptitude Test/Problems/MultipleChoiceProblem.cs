using System;
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
		private static int falseType = 0;

		public MultipleChoiceProblem(Difficulty difficulty)
		{
			Equation = Calc.CreateEquation(difficulty);
			MysteryIndex = Calc.RandomRange(Equation.GetType() == typeof(TwoTermEquation) ? new Range(0, 2) : new Range(2, 3));
			CorrectIndex = Calc.RandomRange(new Range(0, 3));

			for (int i = 0; i < 4; i++)
			{
				Answers[i] = i == CorrectIndex ? Equation.GetMysteryValue(MysteryIndex) : Math.Round(GetFalseValue(Equation.GetMysteryValue(MysteryIndex)), 1);
			}
		}

		private double GetFalseValue(double trueValue)
		{
			falseType += Calc.RandomRange(new Range(1, 2));

			switch (falseType % 8)
			{
				case 0:
					return trueValue + Calc.RandomRange(new Range(1, 5));
				case 1:
					return trueValue + Calc.RandomRange(new Range(6, 10));
				case 2:
					return trueValue + Calc.RandomRange(new Range(11, 15));
				case 3:
					return trueValue - Calc.RandomRange(new Range(1, 5));
				case 4:
					return trueValue - Calc.RandomRange(new Range(6, 10));
				case 5:
					return trueValue - Calc.RandomRange(new Range(11, 15));
				case 6:
					return trueValue * Calc.RandomRange(new Range(1, 5)) / 6;
				case 7:
					return trueValue * Calc.RandomRange(new Range(7, 12)) / 6;
				default:
					return trueValue * Calc.RandomRange(new Range(-5, 5));
			}
		}

		public override ProblemEvaluation GradeResponse(int response)
		{
			return response == CorrectIndex ? ProblemEvaluation.Correct : ProblemEvaluation.Incorrect;
		}
	}
}
