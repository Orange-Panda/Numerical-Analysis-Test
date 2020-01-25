using System.Collections.Generic;

namespace Aptitude_Test
{
	/// <summary>
	/// Base class for an equation datatype
	/// </summary>
	public abstract class Equation
	{
		/// <summary>
		/// Get the answer value to the equation
		/// </summary>
		/// <returns>Answer as computed by the equation</returns>
		public abstract int GetValue();

		/// <summary>
		/// Get an string representing the equation. Does not include answer.
		/// </summary>
		/// <returns>Equation without the answer value</returns>
		public abstract string GetString();

		/// <summary>
		/// Get an equation string with a value hidden from the user (e.g 2 + __ = 5)
		/// </summary>
		/// <param name="mysteryIndex">The value to be hidden from equation</param>
		/// <returns>Equation with a single value missing</returns>
		public abstract string GetMysteryString(int mysteryIndex);

		/// <summary>
		/// Get the hidden number for a mystery string.
		/// </summary>
		/// <param name="mysteryIndex">Index of the value in equation</param>
		/// <returns>The number that would be hidden from a mystery string</returns>
		public abstract int GetMysteryValue(int mysteryIndex);
	}

	/// <summary>
	/// Type of equation that has two terms and an opperator (e.g 3 + 4 or 2 * 4) 
	/// </summary>
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

		public override string GetMysteryString(int mysteryIndex)
		{
			return string.Format("{0} {1} {2} = {3}",
				mysteryIndex != 0 ? num1.ToString() : "__",
				Calculation.GetOperationChar(op1).ToString(),
				mysteryIndex != 1 ? num2.ToString() : "__",
				mysteryIndex != 2 ? GetValue().ToString() : "__");
		}

		public override int GetMysteryValue(int mysteryIndex)
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

	/// <summary>
	/// Different types of operators that can be created.
	/// </summary>
	public enum Operator
	{
		Add, Subtract, Multiply, Divide, Mod
	}
}