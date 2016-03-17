using CodedSelenium.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace CodedSelenium.Test.ObjectMap
{
    public class MouseClickDiv : HtmlDiv
    {
        private HtmlControl xControl;
        private HtmlControl yControl;
        private MouseButtons mouseButton;
        private HtmlControl actionControl;
        private HtmlControl buttonControl;
        private HtmlControl altKeyControl;
        private HtmlControl shiftKeyControl;
        private HtmlControl ctrlKeyControl;

        public MouseClickDiv(UITestControl parent)
            : base(parent)
        {
        }

        public MouseAction Action
        {
            get
            {
                if (actionControl == null)
                {
                    actionControl = new HtmlControl(this);
                    actionControl.SearchProperties[HtmlControl.PropertyNames.Id] = "logAction";
                }

                string value = actionControl.InnerText;

                switch (value)
                {
                    case "click":
                        return MouseAction.Click;

                    case "mouseup":
                        return MouseAction.MouseUp;

                    case "mousedown":
                        return MouseAction.MouseDown;

                    default:
                        throw new NotImplementedException(string.Format("'{0}' mouse action type is not handled", value));
                }
            }
        }

        public MouseButtons MouseButton
        {
            get
            {
                if (buttonControl == null)
                {
                    buttonControl = new HtmlControl(this);
                    buttonControl.SearchProperties[HtmlControl.PropertyNames.Id] = "button";
                }

                string value = buttonControl.InnerText;

                switch (value)
                {
                    case "0":
                        return MouseButtons.Left;

                    case "1":
                        return MouseButtons.Middle;

                    case "2":
                        return MouseButtons.Right;

                    default:
                        throw new NotImplementedException(string.Format("'{0}' mouse action type is not handled", value));
                }
            }
        }

        public ModifierKeys ModifierKey
        {
            get
            {
                if (altKeyControl == null)
                {
                    altKeyControl = new HtmlControl(this);
                    altKeyControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "altKey");
                }

                if (shiftKeyControl == null)
                {
                    shiftKeyControl = new HtmlControl(this);
                    shiftKeyControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "shiftKey");
                }

                if (ctrlKeyControl == null)
                {
                    ctrlKeyControl = new HtmlControl(this);
                    ctrlKeyControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "ctrlKey");
                }

                foreach (HtmlControl item in new[] { altKeyControl, shiftKeyControl, ctrlKeyControl })
                {
                    string value = item.InnerText;

                    if (value == "true")
                    {
                        switch (item.Id)
                        {
                            case "altKey":
                                return ModifierKeys.Alt;

                            case "shiftKey":
                                return ModifierKeys.Shift;

                            case "ctrlKey":
                                return ModifierKeys.Control;
                        }
                    }
                }

                return ModifierKeys.None;
            }
        }

        public int X
        {
            get
            {
                if (xControl == null)
                {
                    xControl = new HtmlControl(this);
                    xControl.SearchProperties[HtmlControl.PropertyNames.Id] = "clientX";
                }

                string value = xControl.InnerText;
                return int.Parse(value);
            }
        }

        public int Y
        {
            get
            {
                if (yControl == null)
                {
                    yControl = new HtmlControl(this);
                    yControl.SearchProperties[HtmlControl.PropertyNames.Id] = "clientY";
                }

                string value = yControl.InnerText;
                return int.Parse(value);
            }
        }

        public enum MouseAction
        {
            Click,
            MouseDown,
            MouseUp
        }
    }
}
