using System;

namespace CSSLayout
{
	public class FloatUtils
	{
		private const float EPSILON = .00001f;

		public static bool FloatsEqual (float f1, float f2)
		{
			if (float.IsNaN (f1) || float.IsNaN (f2)) {
				return float.IsNaN (f1) && float.IsNaN (f2);
			}
			return Math.Abs (f2 - f1) < EPSILON;
		}
	}
}

