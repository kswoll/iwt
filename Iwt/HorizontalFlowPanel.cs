using System;
using System.Drawing;
using CoreGraphics;

namespace Iwt
{
    public class HorizontalFlowPanel : Panel
    {
        public int Spacing { get; set; }

        public HorizontalFlowPanel(params Style[] styles) : base(styles)
        {
        }

        protected override CGSize CalculatePreferredSize(CGSize availableSpace)
        {
            nfloat height = 0;
            nfloat width = 0;
            foreach (var subview in Subviews) 
            {
                var preferredSize = subview.SizeThatFits(new CGSize(availableSpace.Width, nfloat.MaxValue));
                width += preferredSize.Width;
                height = (nfloat)Math.Max(height, preferredSize.Height);
            }
            return new CGSize(width, height);
        }
        
        protected override void LayoutPanel(CGRect clientFrame)
        {
            nfloat x = clientFrame.Left;
            nfloat spacing = 0;
            
            foreach (var subview in Subviews)
            {
                x += spacing;
                spacing = Spacing;
                
                var preferredSize = subview.SizeThatFits(new CGSize(float.MaxValue, clientFrame.Height));
                subview.Frame = new CGRect(x, clientFrame.Top, preferredSize.Width, clientFrame.Height);
                x += preferredSize.Width;
            }
        }
    }
}

