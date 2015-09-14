using System;
using System.Text;
using System.Collections.Generic;

namespace CSSLayout
{
	public class CSSNode
	{
		enum LayoutState 
		{
			DIRTY,
			HAS_NEW_LAYOUT,
			UP_TO_DATE,
		}
			
		internal readonly CSSStyle style = new CSSStyle();
		internal readonly CSSLayout layout = new CSSLayout();
		internal readonly CachedCSSLayout lastLayout = new CachedCSSLayout();

		public int lineIndex = 0;

		private CSSNode nextAbsoluteChild;
		private CSSNode nextFlexChild;

		private List<CSSNode> children = new List<CSSNode> (4); // Capacity is 4 according to Java version
		public CSSNode parent = null;
		private IMeasureFunction measureFunction = null;
		private LayoutState layoutState = LayoutState.DIRTY;

		public int GetChildCount() {
			return children.Count;
		}

		public CSSNode GetChildAt(int i) {
			return children [i];
		}

		public void AddChildAt(CSSNode child, int i) {
			if (child.parent != null) {
				throw new InvalidOperationException ("Child already has a parent, it must be removed first.");
			}

			children.Insert(i, child);
			child.parent = this;
			Dirty();
		}

		public CSSNode RemoveChildAt(int i) {
			var removed = children [i];
			children.RemoveAt (i);
			removed.parent = null;
			Dirty ();
			return removed;		
		}

		public CSSNode GetParent() {
			return parent;
		}

		public int IndexOf(CSSNode child) {
			return children.IndexOf (child);
		}

		public void SetMeasureFunction(IMeasureFunction newMeasureFunction) {
			if (this.measureFunction != newMeasureFunction) {
				this.measureFunction = newMeasureFunction;
				Dirty ();
			}
		}

		public bool IsMeasureDefined() {
			return this.measureFunction != null;
		}

		public MeasureOutput Measure(MeasureOutput measureOutput, float width) {
			if (!IsMeasureDefined ()) {
				throw new NullReferenceException ("Missing IMeasureFunction");
			}

			measureOutput.Height = CSSConstants.UNDEFINED;
			measureOutput.Width = CSSConstants.UNDEFINED;

			measureFunction.Measure (this, width, measureOutput);
			return measureOutput;
		}

		public void CalculateLayout(CSSLayoutContext layoutContext) {
			layout.ResetResult ();
//			LayoutEngine.LayoutNode (layoutContext, this, CSSConstants.UNDEFINED, null); // WTF IS NULL? TODO
		}

		protected bool IsDirty() {
			return layoutState == LayoutState.DIRTY;
		}

		public bool HasNewLayout() {
			return layoutState == LayoutState.HAS_NEW_LAYOUT;
		}

		protected void Dirty() {
			if (layoutState == LayoutState.DIRTY) {
				return;
			} else if (layoutState == LayoutState.HAS_NEW_LAYOUT) {
				throw new InvalidOperationException ("Previous layout was ignored! MarkLayoutSeen() was never called.");
			}

			layoutState = LayoutState.DIRTY;

			if (parent != null) {
				parent.Dirty ();
			}
		}

		public void MarkHasNewLayout() {
			layoutState = LayoutState.HAS_NEW_LAYOUT;
		}

		public void MarkLayoutSeen() {
			if (!HasNewLayout ()) {
				throw new InvalidOperationException ("Expected node to have a new layout to be seen!");
			}

			layoutState = LayoutState.UP_TO_DATE;
		}

		public void ToStringWithIndentation(StringBuilder result, int level) {
			var indentation = new StringBuilder ();
			for (int i = 0; i < level; ++i) {
				indentation.Append("__");
			}

			result.Append(indentation.ToString());
			result.Append(layout.ToString());

			if (GetChildCount() == 0) {
				return;
			}

			result.Append(", children: [\n");
			for (int i = 0; i < GetChildCount(); i++) {
				GetChildAt(i).ToStringWithIndentation(result, level + 1);
				result.Append("\n");
			}
			result.Append(indentation + "]");
		}

		public override string ToString ()
		{
			var sb = new StringBuilder ();
			ToStringWithIndentation (sb, 0);
			return sb.ToString ();
		}


		protected bool ValuesEqual(float f1, float f2) {
			return f1 == f2;
		}

		public void SetDirection(CSSDirection direction)  {
			if (this.style.Direction != direction) {
				this.style.Direction = direction;
				Dirty ();
			}
		}

		public void SetFlexDirection(CSSFlexDirection flexDirection) {
			if (this.style.FlexDirection != flexDirection) {
				this.style.FlexDirection = flexDirection;
				Dirty ();
			}
		}

		public void SetJustifyContent(CSSJustify justifyContent) {
			if (this.style.JustifyContent != justifyContent) {
				this.style.JustifyContent = justifyContent;
				Dirty ();
			}

		}

		public void SetAlignItems(CSSAlign alignItems) {
			if (this.style.AlignItems != alignItems) {
				this.style.AlignItems = alignItems;
				Dirty ();
			}
		}

		public void SetAlignSelf(CSSAlign alignSelf) {
			if (this.style.AlignSelf != alignSelf) {
				this.style.AlignSelf = alignSelf;
				Dirty ();
			}
		}

		public void SetPositionType(CSSPositionType positionType) {
			if (this.style.PositionType != positionType) {
				this.style.PositionType = positionType;
				Dirty ();
			}
		}

		public void SetWrap(CSSWrap flexWrap) {
			if (this.style.FlexWrap != flexWrap) {
				this.style.FlexWrap = flexWrap;
				Dirty ();
			}
		}

		public void SetFlex(float flex) {
			if (this.style.Flex != flex) {
				this.style.Flex = flex;
				Dirty ();
			}
		}

		public void SetMargin(int spacingType, float margin) {
			if (style.Margin.Set (spacingType, margin)) {
				Dirty ();
			}
		}

		public void SetPadding(int spacingType, float padding) {
			if (style.Padding.Set (spacingType, padding)) {
				Dirty ();
			}
		}

		public void SetBorder(int spacingType, float border) {
			if (style.Border.Set(spacingType, border)) {
				Dirty ();
			}
		}

		public void SetPositionTop(float positionTop) {
			if (style.Position [CSSLayout.POSITION_TOP] != positionTop) {
				style.Position [CSSLayout.POSITION_TOP] = positionTop;
				Dirty ();
			}
		}

		public void SetPositionBottom(float positionBottom) {
			if (style.Position [CSSLayout.POSITION_BOTTOM] != positionBottom) {
				style.Position [CSSLayout.POSITION_BOTTOM] = positionBottom;
				Dirty ();
			}
		}

		public void SetPositionLeft(float positionLeft) {
			if (style.Position [CSSLayout.POSITION_LEFT] != positionLeft) {
				style.Position [CSSLayout.POSITION_LEFT] = positionLeft;
				Dirty ();
			}
		}

		public void SetPositionRight(float positionRight) {
			if (style.Position [CSSLayout.POSITION_RIGHT] != positionRight) {
				style.Position [CSSLayout.POSITION_RIGHT] = positionRight;
				Dirty ();
			}
		}

		public void SetStyleWidth(float width) {
			if (style.Dimensions [CSSLayout.DIMENSION_WIDTH] != width) {
				style.Dimensions [CSSLayout.DIMENSION_WIDTH] = width;
				Dirty ();
			}
		}

		public void SetStyleHeight(float height) {
			if (style.Dimensions [CSSLayout.DIMENSION_HEIGHT] != height) {
				style.Dimensions [CSSLayout.DIMENSION_HEIGHT] = height;
				Dirty ();
			}
		}

		public float GetLayoutX() {
			return layout.position [CSSLayout.POSITION_LEFT];
		}

		public float GetLayoutY() {
			return layout.position [CSSLayout.POSITION_TOP];
		}

		public float GetLayoutWidth() {
			return layout.dimensions [CSSLayout.DIMENSION_WIDTH];
		}

		public float GetLayoutHeight() {
			return layout.dimensions [CSSLayout.DIMENSION_HEIGHT];
		}

		public CSSDirection GetLayoutDirection() {
			return layout.direction;
		}

		public Spacing GetStylePadding() {
			return style.Padding;
		}

		public float GetStyleWidth() {
			return style.Dimensions [CSSLayout.DIMENSION_WIDTH];
		}

		public float GetStyleHeight() {
			return style.Dimensions [CSSLayout.DIMENSION_HEIGHT];
		}

		public CSSDirection GetStyleDirection() {
			return style.Direction;
		}

		public void SetDefaultPadding(int spacingType, float padding) {
			if (style.Padding.SetDefault (spacingType, padding)) {
				Dirty ();
			}
		}

	}
}
