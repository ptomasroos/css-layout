using System;

namespace CSSLayout
{
	public class ArrayUtils
	{
		public static void Fill<T>(T[] array, T value) where T : struct
		{
			if (array == null) {
				throw new ArgumentNullException ("array");
			}

			for (int i = 0; i < array.Length; i++) {
				array [i] = value;
			}
		}
	}
}

