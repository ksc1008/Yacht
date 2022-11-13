using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yacht
{
    internal class DiceToggleButton
    {
        static string[] die = { "⚀", "⚁", "⚂", "⚃", "⚄", "⚅" };
        Button button;
        Color toggleColor;
        Color color;
        Color inactiveColor;
        bool toggled = false;

        public DiceToggleButton(Button button, Color toggleColor, Color color, Color inactiveColor)
        {
            this.button = button;
            this.toggleColor = toggleColor;
            this.color = color;
            this.inactiveColor = inactiveColor;
            SetActive(false);
            button.Click += (o, e) =>
            {
                if ((o as Button).Enabled)
                {
                    SetToggle(!toggled);
                }
            };
        }
        public void SetDice(int i)
        {
            button.Text = die[i];
        }

        public void SetToggle(bool toggle)
        {
            if (toggle)
                button.BackColor = toggleColor;
            else
                button.BackColor = color;
            toggled = toggle;
        }

        public void SetActive(bool active)
        {
            if (!active)
            {
                button.BackColor = inactiveColor;
            }
            else
            {
                button.BackColor = color;
            }
            button.Enabled = active;
        }

        public bool GetToggled() => toggled;

        public void ClearDice()
        {
            SetToggle(false);
            button.Text = "?";
        }
    }
}
