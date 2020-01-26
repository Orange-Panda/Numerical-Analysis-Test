using System;
using System.Collections.Generic;

namespace Aptitude_Test
{
	/// <summary>
	/// An equation with three terms and two operators (e.g 4 + 3 * 2)
	/// </summary>
	public class ThreeTermEquation : Equation
	{
		public int num1;
		public int num2;
		public int num3;
		public Operator op1;
		public Operator op2;

		/// <summary>
		/// Randomly generates a three term equation
		/// </summary>
		/// <param name="num1">The range of numbers that the first integer can be generated as</param>
		/// <param name="num2">The range of numbers that the second integer can be generated as</param>
		/// <param name="num3">The range of numbers that the third integer can be generated as</param>
		/// <param name="op1">The potential operators that can be generated for the first operator</param>
		/// <param name="op2">The potential operators that can be generated for the second operator</param>
		public ThreeTermEquation(Range num1, Range num2, Range num3, List<Operator> op1, List<Operator> op2)
		{
			this.num1 = Calc.RandomRange(num1);
			this.num2 = Calc.RandomRange(num2);
			this.num3 = Calc.RandomRange(num3);
			this.op1 = op1[Calc.RandomRange(new Range(0, op1.Count - 1))];
			this.op2 = op2[Calc.RandomRange(new Range(0, op2.Count - 1))];
		}

		public override double GetValue()
		{
			if ((int)op1 < 2 && (int)op2 >= 2)
			{
				double rightResult = Calc.CalculateValue(num2, num3, op2);
				return Math.Round(Calc.CalculateValue(num1, rightResult, op1), 1);
			}
			else
			{
				double leftResult = Calc.CalculateValue(num1, num2, op1);
				return Math.Round(Calc.CalculateValue(leftResult, num3, op2), 1);
			}
		}

		public override string GetString()
		{
			return string.Format("{0} {1} {2} {3} {4}", num1, Calc.GetOperationChar(op1), num2, Calc.GetOperationChar(op2), num3);
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
					return num3;
				case 3:
					return GetValue();
				default:
					return 0;
			}
		}

		public override string GetMysteryString(int mysteryIndex)
		{
			return string.Format("{0} {1} {2} {3} {4} = {5}",
				mysteryIndex != 0 ? num1.ToString() : "__",
				Calc.GetOperationChar(op1).ToString(),
				mysteryIndex != 1 ? num2.ToString() : "__",
				Calc.GetOperationChar(op2).ToString(),
				mysteryIndex != 2 ? num3.ToString() : "__",
				mysteryIndex != 3 ? GetValue().ToString() : "__");
		}
	}
}
