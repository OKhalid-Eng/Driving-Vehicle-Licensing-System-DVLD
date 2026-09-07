
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Classes
{
    public class MyColorTable : ProfessionalColorTable
    {
        public override Color MenuStripGradientBegin => Color.FromArgb(74, 63, 146);
        public override Color MenuStripGradientEnd => Color.FromArgb(74, 63, 146);

        public override Color MenuItemSelected => Color.FromArgb(95, 83, 180);

        public override Color MenuItemPressedGradientBegin => Color.FromArgb(74, 63, 146);
        public override Color MenuItemPressedGradientMiddle => Color.FromArgb(74, 63, 146);
        public override Color MenuItemPressedGradientEnd => Color.FromArgb(74, 63, 146);

        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(95, 83, 180);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(95, 83, 180);

        public override Color ToolStripDropDownBackground => Color.White;

        public override Color ImageMarginGradientBegin => Color.White;
        public override Color ImageMarginGradientMiddle => Color.White;
        public override Color ImageMarginGradientEnd => Color.White;

        public override Color MenuBorder => Color.FromArgb(74, 63, 146);
        public override Color MenuItemBorder => Color.FromArgb(74, 63, 146);

        public override Color SeparatorDark => Color.LightGray;
        public override Color SeparatorLight => Color.White;
    }
}
