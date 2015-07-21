using System;

namespace Iwt
{
    public struct TableWidth
    {
        public TableWidthStyle Style { get; private set; }
        public int Value { get; private set; }

        public TableWidth()
        {
            Style = TableWidthStyle.Fixed;
            Value = 0;
        }

        private TableWidth(TableWidthStyle style, int value) : this()
        {
            Style = style;
            Value = value;
        }

        public static TableWidth Fixed(int value)
        {
            return new TableWidth(TableWidthStyle.Fixed, value);
        }

        public static TableWidth Weight(int value = 1)
        {
            return new TableWidth(TableWidthStyle.Weight, value);
        }

        public static TableWidth Percentage(int value)
        {
            return new TableWidth(TableWidthStyle.Percentage, value);
        }

        public static TableWidth SizeToFit()
        {
            return new TableWidth(TableWidthStyle.SizeToFit, 0);    
        }
    }
}

