using System;
using UIKit;
using System.Linq;

namespace Iwt
{
    public class Style
    {
        public UIFont Font { get; set; }
        public Spacing? Padding { get; set; }

        public static void ApplyStyles(IStylableControl control, Style[] styles)
        {
            foreach (var style in styles.Concat(DefaultStyles.Default).Reverse())
            {
                style.Apply(control);
            }
        }

        public void Apply(IStylableControl control)
        {
            if (Font != null)
                control.Font = Font;
            if (Padding != null)
                control.Padding = Padding.Value;
        }
    }
}

