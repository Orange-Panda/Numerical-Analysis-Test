using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptitude_Test
{
	/// <summary>
	/// Contains static methods for calculating.
	/// </summary>
	public static class Calculation
	{
		public static Random random = new Random();

		/// <summary>
		/// Calculates value for a two term expression
		/// </summary>
		/// <param name="num1">Left term of equation</param>
		/// <param name="num2">Right term of equation</param>
		/// <param name="operation">Operator performed on terms</param>
		/// <returns>Resulting integer</returns>
		public static int CalculateValue(int num1, int num2, Operator operation)
		{
			switch (operation)
			{
				case Operator.Add:
					return num1 + num2;
				case Operator.Subtract:
					return num1 - num2;
				case Operator.Multiply:
					return num1 * num2;
				case Operator.Divide:
					return num1 / num2;
				case Operator.Mod:
					return num1 % num2;
				default:
					return 0;
			}
		}

		/// <summary>
		/// Get a random number from a range's minimum and maximum value (inclusive)
		/// </summary>
		/// <param name="range">The range to calculate a number from</param>
		/// <returns>Random number from minimum value to maximum value (inclusive)</returns>
		public static int RandomRange(Range range)
		{
			return random.Next(range.min, range.max + 1);
		}

		/// <summary>
		/// Get a char value for an operator.
		/// </summary>
		/// <param name="operation">The operator to get the character of</param>
		/// <returns>The character the corresponds to an operator</returns>
		public static char GetOperationChar(Operator operation)
		{
			switch (operation)
			{
				case Operator.Add:
					return '+';
				case Operator.Subtract:
					return '-';
				case Operator.Multiply:
					return '*';
				case Operator.Divide:
					return '/';
				case Operator.Mod:
					return '%';
				default:
					return '?';
			}
		}
	}

	/// <summary>
	/// Range of value for random numbers
	/// </summary>
	public struct Range
	{
		public int min;
		public int max;

		/// <summary>
		/// Creates a constant range where min and max are identical
		/// </summary>
		/// <param name="num">The constant value of range</param>
		public Range(int num)
		{
			min = num;
			max = num;
		}

		/// <summary>
		/// Creates a range between two number values
		/// </summary>
		/// <param name="num1">First number of range</param>
		/// <param name="num2">Second number of range</param>
		public Range(int num1, int num2)
		{
			min = Math.Min(num1, num2);
			max = Math.Max(num1, num2);
		}
	}

	/// <summary>
	/// Different types of operators that can be created.
	/// </summary>
	public enum Operator
	{
		Add, Subtract, Multiply, Divide, Mod
	}

	/// <summary>
	/// Regions for the equation to be displayed at.
	/// </summary>
	public enum EquationTarget
	{
		Left, Right
	}

	/// <summary>
	/// Types of user input for "Less than, Equal to, Greater than" questions.
	/// </summary>
	public enum LEGResponse
	{
		Greater, Less, Equal
	}

	/// <summary>
	/// The escalating difficulties the assessment will reach
	/// </summary>
	public enum Difficulty
	{
		Introduction, Addition, Multiplication, AddMultiply, four, five, six
	}
}
