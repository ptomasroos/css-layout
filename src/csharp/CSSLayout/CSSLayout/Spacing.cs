using System;

namespace CSSLayout
{
	public class Spacing
	{
		public readonly int LEFT = 0;
		public readonly int TOP = 1;
		public readonly int RIGHT = 2;
		public readonly int BOTTOM = 3;
		public readonly int VERTICAL = 4;
		public readonly int HORIZONTAL = 5;
		public readonly int START = 6;
		public readonly int END = 7;
		public readonly int ALL = 8;
	
		private readonly float[] spacing = NewFullSpacingArray ();
		private float[] defaultSpacing = null;

		public bool Set (int spacingType, float value)
		{
			if (spacing [spacingType] != value) {
				spacing [spacingType] = value;
				return true;
			}
			return false;
		}

		public bool SetDefault (int spacingType, float value)
		{
			if (defaultSpacing == null) {
				defaultSpacing = NewSpacingResultArray ();
			}

			if (defaultSpacing [spacingType] != value) {
				defaultSpacing [spacingType] = value;
				return true;
			}

			return false;
		}

		public float Get (int spacingType)
		{
			int secondType = spacingType == TOP || spacingType == BOTTOM ? VERTICAL : HORIZONTAL;
			float defaultValue = spacingType == START || spacingType == END ? CSSConstants.UNDEFINED : 0;

			if (spacing [spacingType] != CSSConstants.UNDEFINED)
				return spacing [spacingType];

			if (spacing [secondType] != CSSConstants.UNDEFINED)
				return spacing [secondType];

			if (spacing [ALL] != CSSConstants.UNDEFINED)
				return spacing [ALL];

			if (defaultSpacing != null)
				return defaultSpacing [spacingType];

			return defaultValue;
		}

		public float GetRaw (int spacingType)
		{
			return spacing [spacingType];
		}

		private static float[] NewFullSpacingArray ()
		{
			return new float[] {
				CSSConstants.UNDEFINED,
				CSSConstants.UNDEFINED,
				CSSConstants.UNDEFINED,
				CSSConstants.UNDEFINED,
				CSSConstants.UNDEFINED,
				CSSConstants.UNDEFINED,
				CSSConstants.UNDEFINED,
				CSSConstants.UNDEFINED,
				CSSConstants.UNDEFINED
			};
		}

		private static float[] NewSpacingResultArray ()
		{
			return NewSpacingResultArray (0);
		}

		private static float[] NewSpacingResultArray (float defaultValue)
		{
			return new float[] {
				defaultValue,
				defaultValue,
				defaultValue,
				defaultValue,
				defaultValue,
				defaultValue,
				CSSConstants.UNDEFINED,
				CSSConstants.UNDEFINED,
				defaultValue
			};
		}

	}
}