using System;
using System.Collections.Generic;

namespace Aptitude_Test
{
	/// <summary>
	/// Type of equation that has two terms and an opperator (e.g 3 + 4 or 2 * 4) 
	/// </summary>
	public class TwoTermEquation : Equation
	{
		public int num1;
		public int num2;
		public Operator op1;

		/// <summary>
		/// Randomly generates a two term equation.
		/// </summary>
		/// <param name="num1">The range of numbers that the first integer can be generated as</param>
		/// <param name="num2">The range of numbers that the second integer can be generated as</param>
		/// <param name="op1">The potential operators that can be generated for the first operator</param>
		public TwoTermEquation(Range num1, Range num2, List<Operator> op1)
		{
			this.num1 = Calc.RandomRange(num1);
			this.num2 = Calc.RandomRange(num2);
			this.op1 = op1[Calc.RandomRange(new Range(0, op1.Count - 1))];
		}

		public override double GetValue()
		{
			return Math.Round(Calc.CalculateValue(num1, num2, op1), 1);
		}

		public override string GetString()
		{
			return string.Format("{0} {1} {2}", num1, Calc.GetOperationChar(op1), num2);
		}

		public override string GetMysteryString(int mysteryIndex)
		{
			return string.Format("{0} {1} {2} = {3}",
				mysteryIndex != 0 ? num1.ToString() : "__",
				Calc.GetOperationChar(op1).ToString(),
				mysteryIndex != 1 ? num2.ToString() : "__",
				mysteryIndex != 2 ? GetValue().ToString() : "__");
		}

		public override double GetMysteryValue(int mysteryIndex)
		{
			switch (mysteryIndex)
			{
				case 0:
					return num1;
				case 1:
					return num2;
				case 2:
					return GetValue();
				default:
					return 0;
			}
		}
	}
}
