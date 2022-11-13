using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yacht
{
    internal class ScoreIndicatingLabel
    {
        public ScoreIndicatingLabel(Color fC, Color hindFC, Color bgC, Color hoverC, Color disabledBG, Color disabledFG, bool enabled = true)
        {
            this.enabled = enabled;
            fontColor = fC;
            hintFontColor = hindFC;
            bgColor = bgC;
            hoverColor = hoverC;
            disabledBgColor = disabledBG; 
            disabledFgColor = disabledFG;
        }

        Label? label;
        bool initiated = false;
        bool isHovering = false;

        public bool enabled { get; private set; }
        public Color fontColor { get; private set; }
        public Color hintFontColor{ get; private set; }
        public Color bgColor{ get; private set; }
        public Color hoverColor { get; private set; }
        public Color disabledBgColor { get; private set; }
        public Color disabledFgColor { get; private set; }


        public void SetLabel(Label label, Func<int>? f=null)
        {
            this.label = label;
            initiated = true;
            label.MouseHover += (o, e) =>
            {
                isHovering = true;
                if (enabled && Yacht.Interactive)
                {
                    (o as Label).BackColor = hoverColor;
                }
            };


            label.MouseLeave += (o, e) =>
            {
                isHovering = false;
                if(enabled)
                    (o as Label).BackColor = bgColor;
            };

            if (f != null)
            {
                label.MouseDown += (o, e) => {
                    if (enabled && Yacht.Interactive)
                    {
                        int t = f();
                        SetText(t.ToString());
                        SetEnabled(false);
                    }
                };
            }
        }

        public void SetHint(string str)
        {
            if (!initiated)
            {
                throw new InvalidOperationException("Label is not initiated");
            }

            label!.Text = str;
            label!.ForeColor = hintFontColor;
        }

        public void SetEnabled(bool enabled)
        {
            if (initiated) {
                if (enabled)
                {
                    if (isHovering)
                    {
                        label!.BackColor = hoverColor;
                        label!.ForeColor = fontColor;
                    }
                }
                else
                {
                    label!.BackColor = disabledBgColor;
                    label!.ForeColor = disabledFgColor;
                }
            }
            this.enabled = enabled;
        }

        public void SetText(string str)
        {
            if (!initiated)
            {
                throw new InvalidOperationException("Label is not initiated");
            }
            Debug.WriteLine(fontColor);
            //MessageBox.Show(fontColor.ToString());
            label!.Text = str;
            label!.ForeColor = fontColor;
        }
        
    }
}
