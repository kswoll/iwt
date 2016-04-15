using System;
using UIKit;

namespace Iwt
{
    public class StylableLabel : UILabel
    {
        private Style style;

        public StylableLabel()
        {
        }

        public Style Style 
        {
            get { return style; }
            set 
            {
                if (style != value) 
                {
                    if (style != null)
                        style.Detach(this);

                    style = value;

                    if (style != null)
                        style.Attach(this);
                }
            }
        }
    }
}

