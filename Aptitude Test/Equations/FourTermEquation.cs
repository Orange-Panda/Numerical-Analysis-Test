using System;
using System.Collections.Generic;

namespace Aptitude_Test
{
	public class FourTermEquation : Equation
	{
		public int num1;
		public int num2;
		public int num3;
		public int num4;
		public Operator op1;
		public Operator op2;
		public Operator op3;

		public FourTermEquation(Range num1, Range num2, Range num3, Range num4, List<Operator> op1, List<Operator> op2, List<Operator> op3)
		{
			this.num1 = Calc.RandomRange(num1);
			this.num2 = Calc.RandomRange(num2);
			this.num3 = Calc.RandomRange(num3);
			this.num4 = Calc.RandomRange(num4);
			this.op1 = op1[Calc.RandomRange(new Range(0, op1.Count - 1))];
			this.op2 = op2[Calc.RandomRange(new Range(0, op2.Count - 1))];
			this.op3 = op3[Calc.RandomRange(new Range(0, op3.Count - 1))];
		}

		public override double GetValue()
		{
			return 0;


			if ((int)op3 >= 2)
			{
				if ((int)op2 >= 2)
				{
					double second = Calc.CalculateValue(num2, num3, op2);
					double third = Calc.CalculateValue(second, num4, op3);
					return Math.Round(Calc.CalculateValue(num1, third, op1), 1);
				}
				else
				{
					double third = Calc.CalculateValue(num3, num4, op3);
					double first = Calc.CalculateValue(num1, num2, op1);
					return Math.Round(Calc.CalculateValue(first, third, op2), 1);
				}
			}
			else
			{
				if ((int)op2 >= 2)
				{
					double second = Calc.CalculateValue(num2, num3, op2);
					double first = Calc.CalculateValue(num1, second, op1);
					return Math.Round(Calc.CalculateValue(first, num4, op3), 1);
				}
				else
				{
					double first = Calc.CalculateValue(num1, num2, op1);
					double second = Calc.CalculateValue(first, num3, op2);
					return Math.Round(Calc.CalculateValue(second, num4, op3), 1);
				}
			}
		}

		public override string GetString()
		{
			return string.Format("{0} {1} {2} {3} {4} {5} {6}", num1, Calc.GetOperationChar(op1), num2, Calc.GetOperationChar(op2), num3, Calc.GetOperationChar(op3), num4);
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
					return num4;
				case 4:
					return GetValue();
				default:
					return 0;
			}
		}

		public override string GetMysteryString(int mysteryIndex)
		{
			return string.Format("{0} {1} {2} {3} {4} {5} {6} = {7}",
				mysteryIndex != 0 ? num1.ToString() : "__",
				Calc.GetOperationChar(op1).ToString(),
				mysteryIndex != 1 ? num2.ToString() : "__",
				Calc.GetOperationChar(op2).ToString(),
				mysteryIndex != 2 ? num3.ToString() : "__",
				Calc.GetOperationChar(op3).ToString(),
				mysteryIndex != 3 ? num4.ToString() : "__",
				mysteryIndex != 4 ? GetValue().ToString() : "__");
		}
	}
}
