using System;
using UIKit;
using CoreGraphics;

namespace Iwt
{
    public enum Alignment
    {
        TopLeft, Top, TopRight, Left, Center, Right, BottomLeft, Bottom, BottomRight,
        TopLeftFill, LeftFill, BottomLeftFill
    }

    /// <summary>
    /// A panel that aligns its *one* child in a particular orientation.  The basic directions are:
    /// 
    /// TopLeft
    /// Top
    /// TopRight
    /// Left
    /// Center
    /// Right
    /// BottomLeft
    /// Bottom
    /// BottomRight
    /// 
    /// The extended directions are:
    /// 
    /// TopLeftFill
    /// LeftFill
    /// BottomLeftFill
    /// 
    /// These are similar to TopLeft, Left, and Bottom respectively, but the width component is filled in by the available
    /// space.
    /// 
    /// </summary>
    public class AlignmentPanel : Panel
    {
        public Alignment Alignment { get; private set; }

        public AlignmentPanel(Alignment alignment, UIView view, params Style[] styles) : base(styles)
        {
            Alignment = alignment;
            AddSubview(view);
        }

        public static AlignmentPanel TopLeft(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.TopLeft, view, styles);
        }

        public static AlignmentPanel Top(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.Top, view, styles);
        }

        public static AlignmentPanel TopRight(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.TopRight, view, styles);
        }

        public static AlignmentPanel Left(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.Left, view, styles);
        }

        public static AlignmentPanel BottomLeft(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.BottomLeft, view, styles);
        }

        public static new AlignmentPanel Center(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.Center, view, styles);
        }

        public static AlignmentPanel Right(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.Center, view, styles);
        }

        public static AlignmentPanel Bottom(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.Bottom, view, styles);
        }

        public static AlignmentPanel BottomRight(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.BottomRight, view, styles);
        }

        public static AlignmentPanel TopLeftFill(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.TopLeftFill, view, styles);
        }

        public static AlignmentPanel LeftFill(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.LeftFill, view, styles);
        }

        public static AlignmentPanel BottomLeftFill(UIView view, params Style[] styles)
        {
            return new AlignmentPanel(Alignment.BottomLeftFill, view, styles);
        }

        protected override void LayoutPanel(CGRect clientFrame)
        {
            var size = Subviews[0].SizeThatFits(clientFrame.Size);
            size = new CGSize(Math.Min(size.Width, clientFrame.Width), Math.Min(size.Height, clientFrame.Height));

            nfloat leftOffset = 0;
            nfloat topOffset = 0;
                
            var clientWidth = clientFrame.Width;
            var widthDifference = clientWidth - size.Width;
            var clientHeight = clientFrame.Height;
            var heightDifference = clientHeight - size.Height;
            var width = size.Width;
            var height = size.Height;

            switch (Alignment)
            {
                case Alignment.TopLeft:
                case Alignment.Left:
                case Alignment.BottomLeft:
                    break;
                case Alignment.Top:
                case Alignment.Center:
                case Alignment.Bottom:
                    leftOffset = widthDifference / 2;
                    break;
                case Alignment.TopRight:
                case Alignment.Right:
                case Alignment.BottomRight:
                    leftOffset = widthDifference;
                    break;
            }
            switch (Alignment)
            {
                case Alignment.TopLeft:
                case Alignment.Top:
                case Alignment.TopRight:
                    break;
                case Alignment.Left:
                case Alignment.Center:
                case Alignment.Right:
                    topOffset = heightDifference / 2;
                    break;
                case Alignment.BottomLeft:
                case Alignment.Bottom:
                case Alignment.BottomRight:
                    topOffset = heightDifference;
                    break;
            }
            switch (Alignment)
            {
                case Alignment.TopLeftFill:
                case Alignment.LeftFill:
                case Alignment.BottomLeftFill:
                    width = clientFrame.Width;
                    break;
            }

            Subviews[0].Frame = new CGRect(clientFrame.Left + leftOffset, clientFrame.Top + topOffset, width, height);
        }

        protected override CGSize CalculatePreferredSize(CGSize availableSpace)
        {
            return Subviews[0].SizeThatFits(availableSpace);
        }
    }
}

