using System;
using UIKit;
using CoreGraphics;

namespace Iwt
{
    public class AutoFreezePanel : Panel
    {
        private CGRect? frozenBounds;

        public AutoFreezePanel(UIView view, params Style[] styles) : base(styles)
        {
            AddSubview(view);
            ClipsToBounds = true;
        }

        protected override void LayoutPanel(CGRect clientFrame)
        {
            var view = Subviews[0];
            if (frozenBounds == null)
            {
                frozenBounds = clientFrame;
            }
            view.Frame = frozenBounds.Value;
        }

        protected override CGSize CalculatePreferredSize(CGSize availableSpace)
        {
            if (frozenBounds != null)
                return frozenBounds.Value.Size;

            var view = Subviews[0];
            return view.SizeThatFits(availableSpace);
        }
    }
}

