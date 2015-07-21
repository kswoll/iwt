using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;

namespace Iwt
{
	public class VerticalFlowPanel : Panel
	{
		public int Spacing { get; set; }

		private Dictionary<UIView, int> granularSpacing = new Dictionary<UIView, int>();

        public VerticalFlowPanel(params Style[] styles) : base(styles)
        {
        }

		public override void AddSubview(UIView view)
		{
			AddSubview(view, 0);
        }

		public void AddSubview(UIView view, int spaceAbove)
		{
			base.AddSubview(view);
			granularSpacing[view] = spaceAbove;
		}

        protected override CGSize CalculatePreferredSize(CGSize availableSpace)
		{
			nfloat height = granularSpacing.Values.Sum();
			nfloat width = 0;
			foreach (var subview in Subviews) 
			{
                var preferredSize = subview.SizeThatFits(new CGSize(availableSpace.Width, float.MaxValue));
				height += preferredSize.Height;
				width = (nfloat)Math.Max(width, preferredSize.Width);
			}
            return new CGSize(width, height);
		}

		protected override void LayoutPanel(CGRect clientFrame)
		{
			nfloat y = clientFrame.Top;
			nfloat spacing = 0;

			foreach (var subview in Subviews)
			{
				y += spacing;
				spacing = Spacing;
				y += granularSpacing[subview];

				var preferredSize = subview.SizeThatFits(new CGSize(clientFrame.Width, float.MaxValue));
				subview.Frame = new CGRect(clientFrame.Left, y, clientFrame.Width, preferredSize.Height);
				y += preferredSize.Height;
			}
		}
	}
}

