using System;
using System.Collections.Generic;
using System.Drawing;
using UIKit;
using CoreGraphics;
using System.Linq;

namespace Iwt
{
	public enum BorderConstraint 
	{
		None, Top, Bottom, Right, Left, Center
	}
	
	public class BorderPanel : Panel
	{
		public Spacing Spacing { get; set; }
        public UIColor SeparatorColor { get; set; }

        private UIView top;
        private UIView bottom;
        private UIView right;
        private UIView left;
        private UIView center;

        private Lazy<UIView> leftSeparator;
        private Lazy<UIView> rightSeparator;
        private Lazy<UIView> topSeparator;
        private Lazy<UIView> bottomSeparator;

        public BorderPanel(params Style[] styles) : base(styles)
		{
            leftSeparator = new Lazy<UIView>(() => AddVerticalSeparator(true));
            rightSeparator = new Lazy<UIView>(() => AddVerticalSeparator(false));
            topSeparator = new Lazy<UIView>(() => AddHorizontalSeparator(true));
            bottomSeparator = new Lazy<UIView>(() => AddHorizontalSeparator(false));
		}

        private UIView Top
        {
            get 
            { 
                return top == null || top.Hidden ? null : top; 
            }
            set { top = value; }
        }

        private UIView Left
        {
            get
            {
                return left == null || left.Hidden ? null : left;
            }
            set { left = value; }
        }

        private UIView Right
        {
            get 
            {
                return right == null || right.Hidden ? null : right;
            }
            set { right = value; }
        }

        private UIView Bottom
        {
            get 
            {
                return bottom == null || bottom.Hidden ? null : bottom;
            }
            set 
            { 
                bottom = value; 
            }
        }

        private new UIView Center
        {
            get 
            {
                return center == null || center.Hidden ? null : center;
            }
            set { center = value; }
        }

        private UIView AddVerticalSeparator(bool left)
        {
            var view = new VerticalRule(left ? (int)Spacing.Left : (int)Spacing.Right, SeparatorColor);
            base.AddSubview(view);
            return view;
        }

        private UIView AddHorizontalSeparator(bool top)
        {
            var view = new HorizontalRule(top ? (int)Spacing.Top : (int)Spacing.Bottom, SeparatorColor);
            base.AddSubview(view);
            return view;
        }

        public BorderPanel(UIView center = null, UIView top = null, UIView bottom = null, UIView right = null, UIView left = null, params Style[] styles) : this(styles)
		{
			if (center != null)
				AddSubview(center, BorderConstraint.Center);
			if (top != null)
				AddSubview(top, BorderConstraint.Top);
			if (bottom != null)
				AddSubview(bottom, BorderConstraint.Bottom);
			if (right != null)
				AddSubview(right, BorderConstraint.Right);
			if (left != null)
				AddSubview(left, BorderConstraint.Left);
		}

		public override void AddSubview(UIView view)
		{
			AddSubview(view, BorderConstraint.Center);
		}

		public void AddSubview(UIView view, BorderConstraint constraint)
		{
			base.AddSubview(view);

			switch (constraint) 
			{
				case BorderConstraint.Top:
					Top = view;
					break;
				case BorderConstraint.Bottom:
					Bottom = view;
					break;
				case BorderConstraint.Right:
					Right = view;
					break;
				case BorderConstraint.Left:
					Left = view;
					break;
				default:
					Center = view;
					break;
			}
		}

        public override void WillRemoveSubview(UIView view)
        {
            if (view == top)
                Top = null;
            else if (view == left)
                Left = null;
            else if (view == right)
                Right = null;
            else if (view == bottom)
                Bottom = null;
            else if (view == center)
                Center = null;
        }

		protected override void LayoutPanel(CGRect clientFrame)
		{
            var sizes = CalculateSizes(new SizeF((float)clientFrame.Width, (float)clientFrame.Height));

			nfloat height = clientFrame.Height;
			nfloat width = clientFrame.Width;
			nfloat centerTop = clientFrame.Top;
			nfloat centerLeft = clientFrame.Left;

            var top = Top;
            var left = Left;
            var right = Right;
            var bottom = Bottom;
            var center = Center;

			if (top != null)
			{
				height -= sizes.NorthSize.Height;
				centerTop += sizes.NorthSize.Height;
				if (center != null)
				{
					centerTop += Spacing.Top;
					height -= Spacing.Top;
				}
				top.Frame = new CGRect(clientFrame.Left, clientFrame.Top, clientFrame.Width, sizes.NorthSize.Height);
                top.SetNeedsLayout();
			}
			if (bottom != null) 
			{
				height -= sizes.SouthSize.Height;
                bottom.Frame = new CGRect(clientFrame.Left, clientFrame.Top + clientFrame.Height - sizes.SouthSize.Height, clientFrame.Width, sizes.SouthSize.Height);
                bottom.SetNeedsLayout();
				if (center != null) 
				{
					height -= Spacing.Bottom;
				}
			}
			if (left != null)
			{
				width -= sizes.WestSize.Width;
				centerLeft += sizes.WestSize.Width;
                left.Frame = new CGRect(clientFrame.Left, centerTop, sizes.WestSize.Width, height);
                left.SetNeedsLayout();
				if (center != null)
				{
					centerLeft += Spacing.Left;
					width -= Spacing.Left;
				}
			}
			if (right != null)
			{
				width -= sizes.EastSize.Width;
                right.Frame = new CGRect(clientFrame.Left + clientFrame.Width - sizes.EastSize.Width, centerTop, sizes.EastSize.Width, height);
                right.SetNeedsLayout();
				if (center != null)
				{
					width -= Spacing.Right;
				}
			}
			if (center != null)
			{
                center.Frame = new CGRect(centerLeft, centerTop, width, height);
                center.SetNeedsLayout();
			}
            if (SeparatorColor != UIColor.Clear)
            {
                if (Spacing.Top > 0 && sizes.TopSeparatorSize > 0)
                {
                    topSeparator.Value.Frame = new CGRect(clientFrame.Left, clientFrame.Top + sizes.NorthSize.Height, clientFrame.Width, Spacing.Top);
                    topSeparator.Value.Hidden = false;
                }
                else if (topSeparator.IsValueCreated)
                {
                    topSeparator.Value.Hidden = true;
                }
                if (Spacing.Bottom > 0 && sizes.BottomSeparatorSize > 0)
                {
                    bottomSeparator.Value.Frame = new CGRect(clientFrame.Left, clientFrame.Top + clientFrame.Height - sizes.SouthSize.Height - Spacing.Bottom, clientFrame.Width, Spacing.Bottom);
                    bottomSeparator.Value.Hidden = false;
                }
                else if (bottomSeparator.IsValueCreated)
                {
                    bottomSeparator.Value.Hidden = true;
                }
                if (Spacing.Left > 0 && sizes.LeftSeparatorSize > 0)
                {
                    leftSeparator.Value.Frame = new CGRect(clientFrame.Left + centerLeft, centerTop, Spacing.Left, height);
                    leftSeparator.Value.Hidden = false;
                }
                else if (leftSeparator.IsValueCreated)
                {
                    leftSeparator.Value.Hidden = true;
                }
                if (Spacing.Right > 0 && sizes.RightSeparatorSize > 0)
                {
                    rightSeparator.Value.Frame = new CGRect(clientFrame.Left + clientFrame.Width - sizes.EastSize.Width - Spacing.Right, centerTop, Spacing.Right, height);
                    rightSeparator.Value.Hidden = false;
                }
                else if (rightSeparator.IsValueCreated)
                {
                    rightSeparator.Value.Hidden = true;
                }
            }
		}

        protected override CGSize CalculatePreferredSize(CGSize availableSpace)
        {
            var sizes = CalculateSizes(new SizeF((float)availableSpace.Width, (float)availableSpace.Height));

            return new CGSize(
                (nfloat)Max(sizes.NorthSize.Width, sizes.SouthSize.Width, sizes.WestSize.Width + sizes.CenterSize.Width + sizes.EastSize.Width + sizes.LeftSeparatorSize + sizes.RightSeparatorSize),
                (nfloat)Max(sizes.WestSize.Height, sizes.EastSize.Height, sizes.NorthSize.Height + sizes.CenterSize.Height + sizes.SouthSize.Height + sizes.TopSeparatorSize + sizes.BottomSeparatorSize)
			);
		}

		private nfloat Max(params nfloat[] values)
		{
			nfloat maxValue = nfloat.MinValue;
			foreach (var value in values)
			{
				if (value > maxValue)
					maxValue = value;
			}
			return maxValue;
		}

		private Sizes CalculateSizes(SizeF maxSize) 
		{
            var top = Top;
            var left = Left;
            var right = Right;
            var bottom = Bottom;
            var center = Center;

			return new Sizes {
				NorthSize = top != null ? top.SizeThatFits(maxSize) : new SizeF(0, 0),
				SouthSize = bottom != null ? bottom.SizeThatFits(maxSize) : new SizeF(0, 0),
				EastSize = right != null ? right.SizeThatFits(maxSize) : new SizeF(0, 0),
				WestSize = left != null ? left.SizeThatFits(maxSize) : new SizeF(0, 0),
				CenterSize = center != null ? center.SizeThatFits(maxSize) : new SizeF(0, 0),
                LeftSeparatorSize = left != null && (center != null || right != null) ? Spacing.Left : 0,
                RightSeparatorSize = center != null && right != null ? Spacing.Right : 0,
                TopSeparatorSize = top != null && (center != null || bottom != null) ? Spacing.Top : 0,
                BottomSeparatorSize = center != null && bottom != null ? Spacing.Bottom : 0
			};
		}

		class Sizes 
		{
			public CGSize NorthSize { get; set; }
            public CGSize SouthSize { get; set; }
            public CGSize EastSize { get; set; }
            public CGSize WestSize { get; set; }
            public CGSize CenterSize { get; set; }
            public nfloat LeftSeparatorSize { get; set; }
            public nfloat TopSeparatorSize { get; set; }
            public nfloat RightSeparatorSize { get; set; }
            public nfloat BottomSeparatorSize { get; set; }
		}
	}
}

