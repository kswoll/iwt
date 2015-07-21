using System;

namespace Iwt
{
    public struct Spacing
    {
        public nfloat Left { get; private set; }
        public nfloat Top { get; private set; }
        public nfloat Right { get; private set; }
        public nfloat Bottom { get; private set; }

        public Spacing(nfloat left, nfloat top, nfloat right, nfloat bottom) : this()
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public Spacing(nfloat size) : this(size, size, size, size)
        {
        }
        
        public static implicit operator Spacing(nfloat padding) 
        {
            return new Spacing { Left = padding, Top = padding, Right = padding, Bottom = padding };
        }

        public static implicit operator Spacing(int padding) 
        {
            return new Spacing { Left = padding, Top = padding, Right = padding, Bottom = padding };
        }

        public nfloat Width 
        {
            get { return Left + Right; }
        }

        public nfloat Height 
        {
            get { return Top + Bottom; }
        }
    }
}