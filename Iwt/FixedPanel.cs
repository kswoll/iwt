using System;
using System.Drawing;
using System.Linq;
using UIKit;
using CoreGraphics;

namespace Iwt
{
	public class FixedPanel : Panel
	{
        private nint? fixedWidth;
        private nint? fixedHeight;

        public FixedPanel(CGSize fixedSize, params Style[] styles) : this((nint)fixedSize.Width, (nint)fixedSize.Height, styles)
        {
        }

        public FixedPanel(nint? width, nint? height, params Style[] styles) : base(styles)
        {
            this.fixedWidth = width;
            this.fixedHeight = height;
        }
        
        public FixedPanel(CGSize fixedSize, UIView view, params Style[] styles) : this((nint)fixedSize.Width, (nint)fixedSize.Height, view, styles)
		{
		}

        public FixedPanel(nint? width, nint? height, UIView view, params Style[] styles) : this(width, height, styles)
        {
            AddSubview(view);
        }

        public static FixedPanel FixedWidth(nint fixedWidth, UIView view, params Style[] styles)
        {
            return new FixedPanel(fixedWidth, null, view, styles);
        }

        public static FixedPanel FixedHeight(nint fixedHeight, UIView view, params Style[] styles)
        {
            return new FixedPanel(null, fixedHeight, view, styles);
        }

        protected override CGSize CalculatePreferredSize(CGSize availableSpace)
		{
            return new CGSize(fixedWidth ?? 0, fixedHeight ?? 0);
		}

		protected override void LayoutPanel(CGRect clientFrame)
		{
            if (Subviews.Any())
                Subviews[0].Frame = new CGRect(clientFrame.Location, new CGSize(fixedWidth ?? clientFrame.Width, fixedHeight ?? clientFrame.Height));
		}
	}
}

