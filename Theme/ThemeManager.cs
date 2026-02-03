using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public static class ThemeManager
    {
        public static Dictionary<string, Theme> Themes = new()
        {
            ["Світла"] = new Theme
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                ButtonColor = Color.LightGray,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            },
            ["Темна"] = new Theme
            {
                BackColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.White,
                ButtonColor = Color.Gray,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            },
            ["Вишиванка"] = new Theme
            {
                BackColor = Color.White,
                ForeColor = Color.DarkRed,
                ButtonColor = Color.FromArgb(230, 230, 230),
                Font = new Font("Georgia", 10, FontStyle.Bold)
            },
            ["Калина"] = new Theme
            {
                BackColor = Color.FromArgb(255, 240, 240),
                ForeColor = Color.DarkGreen,
                ButtonColor = Color.FromArgb(255, 200, 200),
                Font = new Font("Verdana", 10, FontStyle.Italic)
            },
            ["Мрія"] = new Theme
            {
                BackColor = Color.LightBlue,
                ForeColor = Color.Navy,
                ButtonColor = Color.AliceBlue,
                Font = new Font("Segoe Print", 10, FontStyle.Regular)
            },
            ["Зоряна ніч"] = new Theme
            {
                BackColor = Color.Black,
                ForeColor = Color.Yellow,
                ButtonColor = Color.DarkSlateBlue,
                Font = new Font("Consolas", 10, FontStyle.Bold)
            },
            ["Сонце"] = new Theme
            {
                BackColor = Color.FromArgb(255, 250, 205),
                ForeColor = Color.OrangeRed,
                ButtonColor = Color.Gold,
                Font = new Font("Comic Sans MS", 10, FontStyle.Bold)
            }
        };

        public static void ApplyTheme(Control control, string themeName)
        {
            if (!Themes.TryGetValue(themeName, out var theme)) return;

            control.BackColor = theme.BackColor;
            control.ForeColor = theme.ForeColor;
            control.Font = theme.Font;

            foreach (Control c in control.Controls)
            {
                ApplyTheme(c, themeName);
                if (c is Button)
                    c.BackColor = theme.ButtonColor;
            }
        }
    }

    public class Theme
    {
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Color ButtonColor { get; set; }
        public Font Font { get; set; }
    }
}

