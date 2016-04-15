using System;
using UIKit;

namespace Iwt
{
    public interface IStyleSubscriber
    {
        UIFont Font { set; }
        Spacing Padding { set; }
    }
}

