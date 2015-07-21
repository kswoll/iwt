using System;
using UIKit;

namespace Iwt
{
    public interface IStylableControl
    {
        UIFont Font { set; }
        Spacing Padding { set; }
    }
}

