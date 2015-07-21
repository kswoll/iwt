using System;
using System.Drawing;
using UIKit;
using CoreGraphics;

namespace Iwt
{
    public class HorizontalRule : UIView
    {
        private int size;

        public HorizontalRule(int size = 1, UIColor color = null)
        {
            this.size = size;
            BackgroundColor = color ?? UIColor.Black;
        }

        public override CGSize SizeThatFits(CGSize size)
        {
            var result = new CGSize(size.Width, this.size);
            if (size.Width == float.MaxValue)
                result.Width = 0;
            return result;
        }
    }
}

