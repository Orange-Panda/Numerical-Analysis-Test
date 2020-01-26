using System;
using System.Collections.Generic;

namespace Aptitude_Test
{
	public class ThreeTermEquation : Equation
	{
		public int num1;
		public int num2;
		public int num3;
		public Operator op1;
		public Operator op2;

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
			if ((int)op2 >= 2)
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
