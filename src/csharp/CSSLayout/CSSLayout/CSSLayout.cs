using System;

namespace CSSLayout
{
	public class CSSLayout
	{
		public static readonly int POSITION_LEFT = 0;
		public static readonly int POSITION_TOP = 1;
		public static readonly int POSITION_RIGHT = 2;
		public static readonly int POSITION_BOTTOM = 3;

		public static readonly int DIMENSION_WIDTH = 0;
		public static readonly int DIMENSION_HEIGHT = 1;

		public float[] position = new float[4];
		public float[] dimensions = new float[2];

		public CSSDirection direction = CSSDirection.LTR;

		public void ResetResult ()
		{
			ArrayUtils.Fill (position, 0f);
			ArrayUtils.Fill(dimensions, CSSConstants.UNDEFINED);
			direction = CSSDirection.LTR;
		}

		public void Copy (CSSLayout layout)
		{
			position [POSITION_LEFT] = layout.position [POSITION_LEFT];
			position [POSITION_TOP] = layout.position [POSITION_TOP];
			position [POSITION_RIGHT] = layout.position [POSITION_RIGHT];
			position [POSITION_BOTTOM] = layout.position [POSITION_BOTTOM];
			dimensions [DIMENSION_WIDTH] = layout.dimensions [DIMENSION_WIDTH];
			dimensions [DIMENSION_HEIGHT] = layout.dimensions [DIMENSION_HEIGHT];
			direction = layout.direction;
		}

		public override string ToString ()
		{
			return "layout: {" +
			"left: " + position [POSITION_LEFT] + ", " +
			"top: " + position [POSITION_TOP] + ", " +
			"width: " + dimensions [DIMENSION_WIDTH] + ", " +
			"height: " + dimensions [DIMENSION_HEIGHT] + ", " +
			"direction: " + direction +
			"}";
		}
	}
}