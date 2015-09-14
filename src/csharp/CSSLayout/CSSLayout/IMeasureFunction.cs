using System;

namespace CSSLayout
{
	public interface IMeasureFunction 
	{
		void Measure(CSSNode node, float width, MeasureOutput measureOutput);
	}
}

