using System;
using UIKit;
using CoreGraphics;

namespace Iwt
{
    public class VerticalRule : UIView
    {
        private int size;

        public VerticalRule(int size = 1, UIColor color = null)
        {
            this.size = size;
            BackgroundColor = color ?? UIColor.Black;
        }

        public override CGSize SizeThatFits(CGSize size)
        {
            var result = new CGSize(this.size, size.Height);
            if (size.Height == float.MaxValue)
                size.Height = 0;
            return result;
        }
    }
}

