using System;
using UIKit;
using System.Linq;
using System.Collections.Generic;

namespace Iwt
{
    public class Style : IStyleSubscriber
    {
        private Style parent;
        private UIFont font;
        private Spacing? padding;
        private List<IStyleSubscriber> subscribers = new List<IStyleSubscriber>();

        public Style()
        {
        }

        public Style(Style parent)
        {
            this.parent = parent;
        }

        public void Attach(IStyleSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void Detach(IStyleSubscriber subscriber) 
        {
            subscribers.Remove(subscriber);
        }

        public static void ApplyStyles(IStyleSubscriber control, Style[] styles)
        {
            foreach (var style in styles.Concat(DefaultStyles.Default).Reverse())
            {
                style.Apply(control);
            }
        }

        public void Apply(IStyleSubscriber control)
        {
            if (Font != null)
                control.Font = Font;
            if (Padding != null)
                control.Padding = Padding.Value;
        }

        public UIFont Font 
        { 
            get { return font ?? parent?.Font; }
            set 
            {
                font = value;
                if (font != null)
                {
                    foreach (var subscriber in subscribers)
                    {
                        subscriber.Font = value;
                    }
                }
            }
        }

        UIFont IStyleSubscriber.Font
        {
            get { return Font; }
            set
            {
                if (font == null)
                {
                    foreach (var subscriber in subscribers)
                    {
                        subscriber.Font = value;
                    }
                }
            }
        }

        public Spacing? Padding { get; set; }

    }
}

