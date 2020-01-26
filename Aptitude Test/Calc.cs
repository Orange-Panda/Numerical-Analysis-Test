using System;
using System.Collections.Generic;

namespace Aptitude_Test
{
	/// <summary>
	/// Contains static methods for calculating.
	/// </summary>
	public static class Calc
	{
		private static readonly Random random = new Random();
		public static readonly Dictionary<OperatorCategory, List<Operator>> operators = new Dictionary<OperatorCategory, List<Operator>>()
		{
			{ OperatorCategory.AddSub, new List<Operator>() { Operator.Add, Operator.Subtract } },
			{ OperatorCategory.MultiDivid, new List<Operator>() { Operator.Multiply, Operator.Divide } },
			{ OperatorCategory.Mod, new List<Operator>() { Operator.Mod } },
			{ OperatorCategory.Arithmetic, new List<Operator>() { Operator.Add, Operator.Subtract, Operator.Multiply, Operator.Divide } },
			{ OperatorCategory.All, new List<Operator>() { Operator.Add, Operator.Subtract, Operator.Multiply, Operator.Divide, Operator.Mod } },
		};

		/// <summary>
		/// Calculates value for a two term expression
		/// </summary>
		/// <param name="num1">Left term of equation</param>
		/// <param name="num2">Right term of equation</param>
		/// <param name="operation">Operator performed on terms</param>
		/// <returns>Resulting integer</returns>
		public static double CalculateValue(double num1, double num2, Operator operation)
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

		/// <summary>
		/// Creates an equation at the target difficulty level.
		/// </summary>
		/// <param name="difficulty">The difficulty of the equation to generate</param>
		/// <returns>A reference to the equation generated</returns>
		public static Equation CreateEquation(Difficulty difficulty)
		{
			switch (difficulty)
			{
				case Difficulty.Introduction:
					return new TwoTermEquation(new Range(2, 4), new Range(1, 3), operators[OperatorCategory.AddSub]);
				case Difficulty.Basic:
					return new TwoTermEquation(new Range(4, 6), new Range(2, 4), operators[OperatorCategory.AddSub]);
				case Difficulty.Novice:
					return new TwoTermEquation(new Range(4, 9), new Range(2, 8), operators[OperatorCategory.MultiDivid]);
				case Difficulty.Intermediate:
					return new TwoTermEquation(new Range(3, 15), new Range(3, 15), operators[OperatorCategory.Arithmetic]);
				case Difficulty.Hard:
					return new ThreeTermEquation(new Range(3, 15), new Range(3, 15), new Range(3, 15), operators[OperatorCategory.Arithmetic], operators[OperatorCategory.Arithmetic]);
				case Difficulty.Challenging:
				case Difficulty.Maximum:
					return new ThreeTermEquation(new Range(5, 30), new Range(2, 5), new Range(3, 15), operators[OperatorCategory.Mod], operators[OperatorCategory.Arithmetic]);
				default:
					return new TwoTermEquation(new Range(1, 1), new Range(1, 1), operators[OperatorCategory.Mod]);
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
	/// The different categories of operators to be generated
	/// </summary>
	public enum OperatorCategory
	{
		AddSub, MultiDivid, Mod, Arithmetic, All
	}
}
