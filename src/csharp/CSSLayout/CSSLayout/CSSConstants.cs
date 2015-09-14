using System;

namespace CSSLayout
{
	public class CSSConstants
	{
		public static readonly float UNDEFINED = float.NaN; // TODO remove?

		public static bool IsUndefined(float value) {
			return float.Equals(value, UNDEFINED); // TODO use float.IsNan() instead? Seems like a java thingy
		}
	}
}

