using System;
using CoreGraphics;
using UIKit;
using System.Threading.Tasks;

namespace Iwt
{
    public class AnimationPanel : Panel
    {
        private bool isAnimating;

        public AnimationPanel(UIView view, params Style[] styles) : base(styles)
        {
            AddSubview(view);
        }

        protected override CGSize CalculatePreferredSize(CGSize availableSpace)
        {
            var view = Subviews[0];
            return view.SizeThatFits(availableSpace);
        }

        protected override void LayoutPanel(CGRect clientFrame)
        {
            if (!isAnimating)
            {
                var view = Subviews[0];
                view.Frame = clientFrame;
            }
        }

        public void Replace(UIView newView)
        {
            isAnimating = true;
            var view = Subviews[0];
            var frame = view.Frame;
            AddSubview(newView);
            newView.Frame = new CGRect(frame.Width, frame.Top, frame.Width, frame.Height);
            UIView.Animate(
                .5, () => 
                {
                    view.Frame = new CGRect(frame.Left - frame.Width, frame.Top, frame.Width, frame.Height);
                    newView.Frame = frame;
                },
                () => 
                {
                    view.RemoveFromSuperview();
                    isAnimating = false;
                });
        }
    }
}

