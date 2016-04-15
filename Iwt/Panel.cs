using System;
using System.Drawing;
using UIKit;
using CoreGraphics;

namespace Iwt
{
	public abstract class Panel : UIView, IStyleSubscriber
	{
		protected abstract void LayoutPanel(CGRect clientFrame);
		protected abstract CGSize CalculatePreferredSize(CGSize availableSpace);

		public Spacing Padding { get; set; }

		public Panel(Style[] styles)
		{
            Style.ApplyStyles(this, styles);
		}

        UIFont IStyleSubscriber.Font { set {} }

		public override CGSize SizeThatFits(CGSize size)
		{
            var width = size.Width - (Padding.Left + Padding.Right);
            var height = size.Height - (Padding.Top + Padding.Bottom);

            var preferredSize = CalculatePreferredSize(new CGSize(width, height));
			return new CGSize(
				Math.Min(size.Width, Padding.Left + Padding.Right + preferredSize.Width),
				Math.Min(size.Height, Padding.Top + Padding.Bottom + preferredSize.Height)
			);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			var left = Bounds.Left;
			var top = Bounds.Top;
			var width = Bounds.Width;
			var height = Bounds.Height;

			left += Padding.Left;
			top += Padding.Top;
			width -= Padding.Left + Padding.Right;
			height -= Padding.Top + Padding.Bottom;

			LayoutPanel(new CGRect(left, top, width, height));
		}
	}
}

