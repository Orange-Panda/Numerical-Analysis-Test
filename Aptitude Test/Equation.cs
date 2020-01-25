using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptitude_Test
{
	public abstract class Equation
	{
		public abstract int GetValue();
		public abstract string GetString();
	}

	public class TwoTermEquation : Equation
	{
		public int num1;
		public int num2;
		public Operator op1;

		public TwoTermEquation(Range num1, Range num2, List<Operator> operators)
		{
			this.num1 = Calculation.RandomRange(num1);
			this.num2 = Calculation.RandomRange(num2);
			op1 = operators[Calculation.RandomRange(new Range(0, operators.Count - 1))];
		}

		public override int GetValue()
		{
			return Calculation.CalculateValue(num1, num2, op1);
		}

		public override string GetString()
		{
			return string.Format("{0} {1} {2}", num1, Calculation.GetOperationChar(op1), num2);
		}
	}
}