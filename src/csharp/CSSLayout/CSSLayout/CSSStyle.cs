using System;

namespace CSSLayout
{
	public class CSSStyle
	{
		public CSSDirection Direction = CSSDirection.INHERIT;
		public CSSFlexDirection FlexDirection = CSSFlexDirection.COLUMN;
		public CSSJustify JustifyContent = CSSJustify.FLEX_START;
		public CSSAlign AlignContent = CSSAlign.FLEX_START;
		public CSSAlign AlignItems = CSSAlign.STRETCH;
		public CSSAlign AlignSelf = CSSAlign.AUTO;
		public CSSPositionType PositionType = CSSPositionType.RELATIVE;
		public CSSWrap FlexWrap = CSSWrap.NOWRAP;
		public float Flex;

		public Spacing Margin = new Spacing ();
		public Spacing Padding = new Spacing ();
		public Spacing Border = new Spacing ();

		public float[] Position = {
			CSSConstants.UNDEFINED,
			CSSConstants.UNDEFINED,
			CSSConstants.UNDEFINED,
			CSSConstants.UNDEFINED,
		};

		public float[] Dimensions = {
			CSSConstants.UNDEFINED,
			CSSConstants.UNDEFINED,
		};

		public float MinWidth = CSSConstants.UNDEFINED;
		public float MinHeight = CSSConstants.UNDEFINED;

		public float MaxWidth = CSSConstants.UNDEFINED;
		public float MaxHeight = CSSConstants.UNDEFINED;
	}
}