using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HUlamp
{
    public partial class ColorPicker : Form
    {
        public Color Color = new Color();

        public ColorPicker()
        {
            InitializeComponent();
        }

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            colorWheel.Color = colorEditor.Color;
            Color = colorWheel.Color;
        }

        private void colorWheel_ColorChanged(object sender, EventArgs e)
        {
            colorEditor.Color = colorWheel.Color;
            Color = colorEditor.Color;
        }

        private void colorGrid_ColorChanged(object sender, EventArgs e)
        {
            colorEditor.Color = colorGrid.Color;
            colorWheel.Color = colorGrid.Color;
            Color = colorGrid.Color;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            colorGrid.Color = colorEditor.Color;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ColorPicker_Shown(object sender, EventArgs e)
        {
            colorEditor.Color = Color;
        }
    }
}
