using System;
using System.Collections.Generic;
using System.Drawing;
using UIKit;
using CoreGraphics;

namespace Iwt
{
    public class CardPanel : Panel
    {
        private Dictionary<object, UIView> viewsByKey = new Dictionary<object, UIView>();
        private UIView current;

        public CardPanel(params Style[] styles) : base(styles)
        {
        }

        public override void AddSubview(UIView view)
        {
            throw new InvalidOperationException("Cannot add a subview without specifying a key");
        }

        public void AddSubview(UIView view, object key)
        {
            base.AddSubview(view);
            viewsByKey[key] = view;
            if (current == null)
                current = view;
        }

        public void Show(object key)
        {
            if (current != null)
                current.Hidden = true;
            current = viewsByKey[key];
            current.Hidden = false;
            this.SetNeedsLayout();
        }

        protected override CGSize CalculatePreferredSize(CGSize availableSpace)
        {
            nfloat maxWidth = 0f;
            nfloat maxHeight = 0f;

            foreach (var subview in Subviews)
            {
                var size = subview.SizeThatFits(availableSpace);
                maxWidth = (nfloat)Math.Max(maxWidth, size.Width);
                maxHeight = (nfloat)Math.Max(maxHeight, size.Height);
            }

            return new CGSize(maxWidth, maxHeight);
        }

        protected override void LayoutPanel(CGRect clientFrame)
        {
            current.Frame = clientFrame;
        }
    }
}

