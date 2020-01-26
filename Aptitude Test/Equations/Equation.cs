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
		public abstract double GetValue();

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
		public abstract double GetMysteryValue(int mysteryIndex);
	}

	/// <summary>
	/// Different types of operators that can be created.
	/// </summary>
	public enum Operator
	{
		Add, Subtract, Multiply, Divide, Mod
	}
}