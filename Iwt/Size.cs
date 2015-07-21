using System;

namespace Iwt
{
    public struct Size
    {
        public nfloat Width { get; private set; }
        public nfloat Height { get; private set; }

        public Size()
        {
            Width = 0;
            Height = 0;
        }

        public Size(nfloat width, nfloat height) : this()
        {
            Width = width;
            Height = height;
        }

        public Size(nfloat size) : this(size, size)
        {
            Width = size;
            Height = size;
        }

        public static implicit operator Size(nfloat size)
        {
            return new Size(size);
        }

        public static implicit operator Size(int size)
        {
            return new Size(size);
        }
    }
}

