using CodedSelenium.HtmlControls;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace CodedSelenium.Test.ObjectMap
{
    public class MouseClickDiv : HtmlDiv
    {
        private HtmlControl _xControl;
        private HtmlControl _yControl;
        private HtmlControl _actionControl;
        private HtmlControl _buttonControl;
        private HtmlControl _altKeyControl;
        private HtmlControl _shiftKeyControl;
        private HtmlControl _ctrlKeyControl;

        public MouseClickDiv(UITestControl parent)
            : base(parent)
        {
        }

        public enum MouseAction
        {
            Click,
            MouseDown,
            MouseUp,
            DblClick
        }

        public MouseAction Action
        {
            get
            {
                if (_actionControl == null)
                {
                    _actionControl = new HtmlControl(this);
                    _actionControl.SearchProperties[HtmlControl.PropertyNames.Id] = "logAction";
                }

                string value = _actionControl.InnerText;

                switch (value)
                {
                    case "click":
                        return MouseAction.Click;

                    case "mouseup":
                        return MouseAction.MouseUp;

                    case "mousedown":
                        return MouseAction.MouseDown;

                    case "dblclick":
                        return MouseAction.DblClick;

                    default:
                        throw new NotImplementedException(string.Format("'{0}' mouse action type is not handled", value));
                }
            }
        }

        public MouseButtons MouseButton
        {
            get
            {
                if (_buttonControl == null)
                {
                    _buttonControl = new HtmlControl(this);
                    _buttonControl.SearchProperties[HtmlControl.PropertyNames.Id] = "button";
                }

                string value = _buttonControl.InnerText;

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
                if (_altKeyControl == null)
                {
                    _altKeyControl = new HtmlControl(this);
                    _altKeyControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "altKey");
                }

                if (_shiftKeyControl == null)
                {
                    _shiftKeyControl = new HtmlControl(this);
                    _shiftKeyControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "shiftKey");
                }

                if (_ctrlKeyControl == null)
                {
                    _ctrlKeyControl = new HtmlControl(this);
                    _ctrlKeyControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "ctrlKey");
                }

                foreach (HtmlControl item in new[] { _altKeyControl, _shiftKeyControl, _ctrlKeyControl })
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

        public string X
        {
            get
            {
                if (_xControl == null)
                {
                    _xControl = new HtmlControl(this);
                    _xControl.SearchProperties[HtmlControl.PropertyNames.Id] = "clientX";
                }

                return _xControl.InnerText;
            }
        }

        public string Y
        {
            get
            {
                if (_yControl == null)
                {
                    _yControl = new HtmlControl(this);
                    _yControl.SearchProperties[HtmlControl.PropertyNames.Id] = "clientY";
                }

                return _yControl.InnerText;
            }
        }
    }
}
