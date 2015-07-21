using System;
using UIKit;
using CoreGraphics;

namespace Iwt
{
    public class ContentPanel : Panel
    {
        private Func<IUILayoutSupport> topLayoutGuide;
        private Func<IUILayoutSupport> bottomLayoutGuide;
        
        public ContentPanel(Func<IUILayoutSupport> topLayoutGuide, Func<IUILayoutSupport> bottomLayoutGuide, UIView content, params Style[] styles) : base(styles)
        {
            this.topLayoutGuide = topLayoutGuide;
            this.bottomLayoutGuide = bottomLayoutGuide;
            AddSubview(content);
        }

        public override void WillRemoveSubview(UIView uiview)
        {
            base.WillRemoveSubview(uiview);
        }

        protected override void LayoutPanel(CGRect clientFrame)
        {
            var content = Subviews[0];
            content.Frame = new CGRect(clientFrame.Left, clientFrame.Top + topLayoutGuide().Length, 
                clientFrame.Width, clientFrame.Height - topLayoutGuide().Length - bottomLayoutGuide().Length);
        }

        protected override CGSize CalculatePreferredSize(CGSize availableSpace)
        {
            return new CGSize(availableSpace.Width, availableSpace.Height - topLayoutGuide().Length - bottomLayoutGuide().Length);
        }
    }
}

